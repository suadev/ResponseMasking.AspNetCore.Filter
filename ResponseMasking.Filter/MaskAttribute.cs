using System;

namespace ResponseMasking.Filter
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Property)]
    public class MaskAttribute : Attribute
    {
        public bool MaskAllWords;
        public int MaskLength;
        public char MaskChar;

        /// <summary>
        /// Masking attribute for string type properties
        /// </summary>
        /// <param name="maskLength">Sets the char count which will be masked from zero index of string.</param>
        /// <param name="maskAllWords">Used for strings that contain more than one word. It true, masking will be applied for each word from zero index.</param>
        /// <param name="maskChar">The char which is used for masking</param>
        public MaskAttribute(int maskLength = 3, bool maskAllWords = false, char maskChar = '*')
        {
            this.MaskAllWords = maskAllWords;
            this.MaskLength = maskLength;
            this.MaskChar = maskChar;
        }
    }
}