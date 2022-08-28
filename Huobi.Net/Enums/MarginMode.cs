using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Margin mode
    /// </summary>
    public enum MarginMode
    {
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("cross")]
        Cross,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("isolated")]
        Isolated,
        /// <summary>
        /// All (filter)
        /// </summary>
        [Map("all")]
        All
    }
}
