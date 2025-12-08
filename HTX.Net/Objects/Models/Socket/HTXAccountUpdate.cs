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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal? Balance { get; set; }
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("available")]
        public decimal? Available { get; set; }
        /// <summary>
        /// Type of change
        /// </summary>

        [JsonPropertyName("changeType")]
        public AccountEventType? ChangeType { get; set; }
        /// <summary>
        /// Account type
        /// </summary>

        [JsonPropertyName("accountType")]
        public BalanceType AccountType { get; set; }
        /// <summary>
        /// Change time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("changeTime")]
        public DateTime? ChangeTime { get; set; }
        /// <summary>
        /// Update sequence number
        /// </summary>
        [JsonPropertyName("seqNum")]
        public long SequenceNumber { get; set; }
    }
}
