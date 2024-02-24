using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Huobi.Net.ExtensionMethods
{
    /// <summary>
    /// Extension methods specific to using the Huobi API
    /// </summary>
    public static class HuobiExtensionMethods
    {
        /// <summary>
        /// Validate the string is a valid Huobi symbol.
        /// </summary>
        /// <param name="symbolString">string to validate</param>
        public static string ValidateHuobiSymbol(this string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
                throw new ArgumentException("Symbol is not provided");
            symbolString = symbolString.ToLower(CultureInfo.InvariantCulture);
            if (!Regex.IsMatch(symbolString, "^(([a-z]|[0-9]){4,})$"))
                throw new ArgumentException($"{symbolString} is not a valid Huobi symbol. Should be [QuoteAsset][BaseAsset], e.g. ETHBTC");
            return symbolString;
        }
    }
}
