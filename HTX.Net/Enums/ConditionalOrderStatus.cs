using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Status of a conditional order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ConditionalOrderStatus>))]
    public enum ConditionalOrderStatus
    {
        /// <summary>
        /// ["<c>created</c>"] Created and active
        /// </summary>
        [Map("created")]
        Created,
        /// <summary>
        /// ["<c>canceled</c>"] Canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// ["<c>rejected</c>"] Rejected
        /// </summary>
        [Map("rejected")]
        Rejected,
        /// <summary>
        /// ["<c>triggered</c>"] Triggered
        /// </summary>
        [Map("triggered")]
        Triggered
    }
}
