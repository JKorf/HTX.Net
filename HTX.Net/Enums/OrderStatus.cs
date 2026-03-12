using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// ["<c>pre-submitted</c>"] Pre-submitted
        /// </summary>
        [Map("pre-submitted")]
        PreSubmitted,
        /// <summary>
        /// ["<c>submitted</c>"] Submitted, nothing filled yet
        /// </summary>
        [Map("submitted")]
        Submitted,
        /// <summary>
        /// ["<c>partial-filled</c>"] Partially filled
        /// </summary>
        [Map("partial-filled")]
        PartiallyFilled,
        /// <summary>
        /// ["<c>partial-canceled</c>"] Partially filled, then canceled
        /// </summary>
        [Map("partial-canceled")]
        PartiallyCanceled,
        /// <summary>
        /// ["<c>filled</c>"] Filled completely
        /// </summary>
        [Map("filled")]
        Filled,
        /// <summary>
        /// ["<c>canceled</c>"] Canceled without fill
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// ["<c>created</c>"] Created
        /// </summary>
        [Map("created")]
        Created,
        /// <summary>
        /// ["<c>rejected</c>"] Rejected
        /// </summary>
        [Map("rejected")]
        Rejected
    }
}
