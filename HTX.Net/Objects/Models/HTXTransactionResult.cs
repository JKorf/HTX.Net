

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Transaction result
    /// </summary>
    public record HTXTransactionResult
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("transact-id")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [JsonPropertyName("transact-time")]
        public long TransactionTime { get; set; }
    }
}
