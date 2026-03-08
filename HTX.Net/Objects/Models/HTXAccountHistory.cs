using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Account history data
    /// </summary>
    [SerializationModel]
    public record HTXAccountHistory
    {
        /// <summary>
        /// ["<c>account-id</c>"] Account ID
        /// </summary>
        [JsonPropertyName("account-id")]
        public long AccountId { get; set; }

        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>transact-amt</c>"] Quantity change (positive value if income, negative value if outcome)	
        /// </summary>
        [JsonPropertyName("transact-amt")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>transact-type</c>"] Blance change types
        /// </summary>
        [JsonPropertyName("transact-type")]
        public TransactionType Type { get; set; }

        /// <summary>
        /// ["<c>avail-balance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("avail-balance")]
        public decimal Available { get; set; }
		
        /// <summary>
        /// ["<c>acct-balance</c>"] Account balance
        /// </summary>
        [JsonPropertyName("acct-balance")]
        public decimal Total { get; set; }
		
        /// <summary>
        /// Transaction time (database time)
        /// </summary>
        [JsonPropertyName("transact-time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>record-id</c>"] Unique record ID in the database
        /// </summary>
        [JsonPropertyName("record-id")]
        public long RecordId { get; set; }
    }
}
