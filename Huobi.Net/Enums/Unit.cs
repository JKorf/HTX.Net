using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Unit type
    /// </summary>
    public enum Unit
    {
        /// <summary>
        /// Cont
        /// </summary>
        [Map("1")]
        Cont,
        /// <summary>
        /// Crypto currency
        /// </summary>
        [Map("2")]
        CryptoCurrency
    }
}
