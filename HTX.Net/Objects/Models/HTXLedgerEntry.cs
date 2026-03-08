using HTX.Net.Enums;


namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Ledger entry
    /// </summary>
    [SerializationModel]
    public record HTXLedgerEntry
    {
        /// <summary>
        /// ["<c>accountId</c>"] Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }

        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transactAmt</c>"] Quantity of the transaction
        /// </summary>
        [JsonPropertyName("transactAmt")]
        public decimal TransactionQuantity { get; set; }
        /// <summary>
        /// ["<c>transactType</c>"] Type of transaction
        /// </summary>
        [JsonPropertyName("transactType")]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// ["<c>transferType</c>"] Type of transfer
        /// </summary>
        [JsonPropertyName("transferType")]
        public string TransferType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transactId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("transactId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>transactTime</c>"] Transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("transactTime")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// ["<c>transferer</c>"] Transferer
        /// </summary>
        [JsonPropertyName("transferer")]
        public long Transferer { get; set; }
        /// <summary>
        /// ["<c>transferee</c>"] Transferee
        /// </summary>
        [JsonPropertyName("transferee")]
        public long Transferee { get; set; }
    }
}
