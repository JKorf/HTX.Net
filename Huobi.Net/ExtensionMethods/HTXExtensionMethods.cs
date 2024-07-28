using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HTX.Net.ExtensionMethods
{
    /// <summary>
    /// Extension methods specific to using the HTX API
    /// </summary>
    public static class HTXExtensionMethods
    {
        /// <summary>
        /// Validate the string is a valid HTX symbol.
        /// </summary>
        /// <param name="symbolString">string to validate</param>
        public static string ValidateHTXSymbol(this string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
                throw new ArgumentException("Symbol is not provided");
            symbolString = symbolString.ToLower(CultureInfo.InvariantCulture);
            return symbolString;
        }
    }
}
