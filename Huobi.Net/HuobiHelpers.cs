using System;
using System.Text.RegularExpressions;

namespace Huobi.Net
{
    public static class HuobiHelpers
    {
        /// <summary>
        /// Validate the string is a valid Huobi symbol.
        /// </summary>
        /// <param name="symbolString">string to validate</param>
        public static string ValidateHuobiSymbol(this string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
                throw new ArgumentException("Symbol is not provided");
            symbolString = symbolString.ToLower();
            if (!Regex.IsMatch(symbolString, "^(([a-z]|[0-9]){4,9})$"))
                throw new ArgumentException($"{symbolString} is not a valid Huobi symbol. Should be [QuoteCurrency][BaseCurrency], e.g. ETHBTC");
            return symbolString;
        }
    }
}
