using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Offset
    /// </summary>
    public enum Offset
    {
        /// <summary>
        /// Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Close
        /// </summary>
        [Map("close")]
        Close,
        /// <summary>
        /// Both
        /// </summary>
        [Map("both")]
        Both
    }
}
