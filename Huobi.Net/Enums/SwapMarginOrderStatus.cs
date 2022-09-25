using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    public enum SwapMarginOrderStatus
    {
        /// <summary>
        /// Ready to submit
        /// </summary>
        [Map("1")]
        ReadyToSubmit,
        /// <summary>
        /// Submitting
        /// </summary>
        [Map("2")]
        Submitting,
        /// <summary>
        /// Submitted / active
        /// </summary>
        [Map("3")]
        Submitted,
        /// <summary>
        /// Partially filled
        /// </summary>
        [Map("4")]
        PartiallyFilled,
        /// <summary>
        /// Partially filled, cancelled
        /// </summary>
        [Map("5")]
        PartiallyCancelled,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("6")]
        Filled,
        /// <summary>
        /// Cancelled
        /// </summary>
        [Map("7")]
        Cancelled,
        /// <summary>
        /// Cancelling
        /// </summary>
        [Map("11")]
        Cancelling
    }
}
