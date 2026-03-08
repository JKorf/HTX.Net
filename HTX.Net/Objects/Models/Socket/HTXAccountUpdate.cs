using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Account update
    /// </summary>
    [SerializationModel]
    public record HTXAccountUpdate
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>accountId</c>"] Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// ["<c>balance</c>"] Total balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal? Balance { get; set; }
        /// <summary>
        /// ["<c>available</c>"] Available balance
        /// </summary>
        [JsonPropertyName("available")]
        public decimal? Available { get; set; }
        /// <summary>
        /// ["<c>changeType</c>"] Type of change
        /// </summary>

        [JsonPropertyName("changeType")]
        public AccountEventType? ChangeType { get; set; }
        /// <summary>
        /// ["<c>accountType</c>"] Account type
        /// </summary>

        [JsonPropertyName("accountType")]
        public BalanceType AccountType { get; set; }
        /// <summary>
        /// ["<c>changeTime</c>"] Change time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("changeTime")]
        public DateTime? ChangeTime { get; set; }
        /// <summary>
        /// ["<c>seqNum</c>"] Update sequence number
        /// </summary>
        [JsonPropertyName("seqNum")]
        public long SequenceNumber { get; set; }
    }
}
