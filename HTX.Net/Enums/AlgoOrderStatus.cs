using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Algo order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AlgoOrderStatus>))]
    public enum AlgoOrderStatus
    {
        /// <summary>
        /// ["<c>active</c>"] Active
        /// </summary>
        [Map("active")]
        Active,
        /// <summary>
        /// ["<c>effective</c>"] Effective
        /// </summary>
        [Map("effective")]
        Effective,
        /// <summary>
        /// ["<c>canceled</c>"] Canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// ["<c>failed</c>"] Failed
        /// </summary>
        [Map("failed")]
        Failed
    }
}
