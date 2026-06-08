using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// V5 order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatusV5>))]
    public enum OrderStatusV5
    {
        /// <summary>
        /// ["<c>new</c>"] New
        /// </summary>
        [Map("new")]
        New,
        /// <summary>
        /// ["<c>partially_filled</c>"] Partially filled
        /// </summary>
        [Map("partially_filled")]
        PartiallyFilled,
        /// <summary>
        /// ["<c>filled</c>"] Filled
        /// </summary>
        [Map("filled")]
        Filled,
        /// <summary>
        /// ["<c>partially_canceled</c>"] Partially canceled
        /// </summary>
        [Map("partially_canceled")]
        PartiallyCanceled,
        /// <summary>
        /// ["<c>canceled</c>"] Canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// ["<c>rejected</c>"] Rejected
        /// </summary>
        [Map("rejected")]
        Rejected
    }
}
