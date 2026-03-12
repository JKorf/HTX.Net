using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Status of a margin order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginOrderStatus>))]
    public enum MarginOrderStatus
    {
        /// <summary>
        /// ["<c>created</c>"] Created
        /// </summary>
        [Map("created")]
        Created,
        /// <summary>
        /// ["<c>accrual</c>"] Loaned
        /// </summary>
        [Map("accrual")]
        Accural,
        /// <summary>
        /// ["<c>cleared</c>"] Paid
        /// </summary>
        [Map("cleared")]
        Cleared,
        /// <summary>
        /// ["<c>invalid</c>"] Invalid
        /// </summary>
        [Map("invalid")]
        Invalid,
        /// <summary>
        /// ["<c>failed</c>"] Failed
        /// </summary>
        [Map("failed")]
        Failed
    }
}
