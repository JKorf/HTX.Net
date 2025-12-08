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
        /// Pre-submitted
        /// </summary>
        [Map("pre-submitted")]
        PreSubmitted,
        /// <summary>
        /// Submitted, nothing filled yet
        /// </summary>
        [Map("submitted")]
        Submitted,
        /// <summary>
        /// Partially filled
        /// </summary>
        [Map("partial-filled")]
        PartiallyFilled,
        /// <summary>
        /// Partially filled, then canceled
        /// </summary>
        [Map("partial-canceled")]
        PartiallyCanceled,
        /// <summary>
        /// Filled completely
        /// </summary>
        [Map("filled")]
        Filled,
        /// <summary>
        /// Canceled without fill
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// Created
        /// </summary>
        [Map("created")]
        Created,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("rejected")]
        Rejected
    }
}
