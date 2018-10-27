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

            var type = okResult.Value.GetType();
            if (type.IsGenericType && type.GetInterfaces().Contains(typeof(IEnumerable)))
            {
                if (okResult.Value is IEnumerable) // generic list
                {
                    foreach (var item in okResult.Value as IEnumerable)
                    {
                        CheckPropertiesForMasking(item);
                    }
                }
            }
            else if (type.Name == "String") // plain text
            {
                var firstValue = okResult.Value as String;
                okResult.Value = firstValue.Substring(this.MaskLength).PadLeft(firstValue.Length, this.MaskChar);
            }
            else // complex type
            {
                if (type.IsGenericType) // paged list?
                {
                    var prop = okResult.Value.GetType().GetProperties().FirstOrDefault(q =>
                                                 q.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IEnumerable)));
                    if (prop == null)
                        return;

                    foreach (var item in (IEnumerable)prop.GetValue(okResult.Value, null))
                    {
                        CheckPropertiesForMasking(item);
                    }
                }
                else
                {
                    CheckPropertiesForMasking(okResult.Value);
                }
            }
        }

        private static void CheckPropertiesForMasking(object item)
        {
            var props = item.GetType().GetProperties();
            foreach (var prop in props)
            {
                var maskAttrData = prop.GetCustomAttributes(true).FirstOrDefault(q => q is MaskAttribute);
                if (maskAttrData == null)
                    continue;

                if (prop.PropertyType.Name != "String")
                {
                    throw new TypeAccessException("Only string type can be masked.");
                }

                var firstValue = (string)prop.GetValue(item, null);
                var lastValue = GetMaskedValue((MaskAttribute)maskAttrData, firstValue);
                prop.SetValue(item, lastValue, null);
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