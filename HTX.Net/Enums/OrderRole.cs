using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Order role
    /// </summary>
    public enum OrderRole
    {
        /// <summary>
        /// Maker of an order book entry
        /// </summary>
        [Map("maker")]
        Maker,
        /// <summary>
        /// Taker of an order book entry
        /// </summary>
        [Map("taker")]
        Taker
    }
}
