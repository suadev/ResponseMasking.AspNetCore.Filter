using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ResponseMasking.Filter
{
    public class MaskResponseFilterAttribute : Attribute, IActionFilter
    {
        public int MaskLength { get; }
        public char MaskChar { get; }

        /// <summary>
        /// This constructor is only for plain text response. 
        /// </summary>
        /// <param name="maskLength">Sets the char count which will be masked from zero index of string.</param>
        /// <param name="maskChar">The char which is used for masking</param>
        public MaskResponseFilterAttribute(int maskLength, char maskChar = '*')
        {
            this.MaskChar = maskChar;
            this.MaskLength = maskLength;
        }

        public MaskResponseFilterAttribute()
        {

        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            var okResult = context.Result as OkObjectResult;
            if (okResult == null)
                return;

            var response = okResult.Value;
            var typeName = response.GetType().Name;

            if (typeName.Contains("List") || typeName.Contains("EnumerableQuery")) // generic list
            {
                foreach (var item in response as IEnumerable)
                {
                    CheckPropertiesForMasking(item);
                }
            }
            else if (response.GetType().Name == "String") // plain text
            {
                var firstValue = response as String;
                okResult.Value = firstValue.Substring(this.MaskLength).PadLeft(firstValue.Length, this.MaskChar);
            }
            else // complex type
            {
                CheckPropertiesForMasking(response);
            }
        }

        private static void CheckPropertiesForMasking(object response)
        {
            var props = response.GetType().GetProperties();
            foreach (var prop in props)
            {
                var maskAttrData = prop.GetCustomAttributes(true).FirstOrDefault(q => q is MaskAttribute);
                if (maskAttrData == null)
                    continue;

                if (prop.PropertyType.Name != "String")
                {
                    throw new TypeAccessException("Only string type can be masked.");
                }

                var firstValue = prop.GetValue(response).ToString();
                var lastValue = GetMaskedValue((MaskAttribute)maskAttrData, firstValue);
                prop.SetValue(response, lastValue);
            }
        }

        private static string GetMaskedValue(MaskAttribute maskAttr, string firstValue)
        {
            var maskLength = maskAttr.MaskLength;
            var lastValue = string.Empty;

            if (maskAttr.MaskAllWords)
            {
                var words = firstValue.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Substring(maskLength).PadLeft(words[i].Length, maskAttr.MaskChar);
                }
                lastValue = string.Join(' ', words);
            }
            else
            {
                lastValue = firstValue.Substring(maskLength).PadLeft(firstValue.Length, maskAttr.MaskChar);
            }
            return lastValue;
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}