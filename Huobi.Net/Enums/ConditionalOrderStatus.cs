using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Status of a conditional order
    /// </summary>
    public enum ConditionalOrderStatus
    {
        /// <summary>
        /// Created and active
        /// </summary>
        [Map("created")]
        Created,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("rejected")]
        Rejected,
        /// <summary>
        /// Triggered
        /// </summary>
        [Map("triggered")]
        Triggered
    }
}
