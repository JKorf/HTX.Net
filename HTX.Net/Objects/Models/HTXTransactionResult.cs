namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Transaction result
    /// </summary>
    [SerializationModel]
    public record HTXTransactionResult
    {
        /// <summary>
        /// ["<c>transact-id</c>"] Id
        /// </summary>
        [JsonPropertyName("transact-id")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>transact-time</c>"] Time
        /// </summary>
        [JsonPropertyName("transact-time")]
        public long TransactionTime { get; set; }
    }
}
