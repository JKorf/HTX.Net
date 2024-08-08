using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Account history data
    /// </summary>
    public record HTXAccountHistory
    {
        /// <summary>
        /// Account ID
        /// </summary>
        [JsonPropertyName("account-id")]
        public long AccountId { get; set; }

        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Quantity change (positive value if income, negative value if outcome)	
        /// </summary>
        [JsonPropertyName("transact-amt")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Blance change types
        /// </summary>
        [JsonPropertyName("transact-type"), JsonConverter(typeof(EnumConverter))]
        public TransactionType Type { get; set; }

        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("avail-balance")]
        public decimal Available { get; set; }
		
        /// <summary>
        /// Account balance
        /// </summary>
        [JsonPropertyName("acct-balance")]
        public decimal Total { get; set; }
		
        /// <summary>
        /// Transaction time (database time)
        /// </summary>
        [JsonPropertyName("transact-time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Unique record ID in the database
        /// </summary>
        [JsonPropertyName("record-id")]
        public long RecordId { get; set; }
    }
}