using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Status of a margin order
    /// </summary>
    public enum MarginOrderStatus
    {
        /// <summary>
        /// Created
        /// </summary>
        [Map("created")]
        Created,
        /// <summary>
        /// Loaned
        /// </summary>
        [Map("accrual")]
        Accural,
        /// <summary>
        /// Paid
        /// </summary>
        [Map("cleared")]
        Cleared,
        /// <summary>
        /// Invalid
        /// </summary>
        [Map("invalid")]
        Invalid,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("failed")]
        Failed
    }
}
