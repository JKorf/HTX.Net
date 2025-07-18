using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }

        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity of the transaction
        /// </summary>
        [JsonPropertyName("transactAmt")]
        public decimal TransactionQuantity { get; set; }
        /// <summary>
        /// Type of transaction
        /// </summary>
        [JsonPropertyName("transactType")]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Type of transfer
        /// </summary>
        [JsonPropertyName("transferType")]
        public string TransferType { get; set; } = string.Empty;
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("transactId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("transactTime")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// Transferer
        /// </summary>
        [JsonPropertyName("transferer")]
        public long Transferer { get; set; }
        /// <summary>
        /// Transferee
        /// </summary>
        [JsonPropertyName("transferee")]
        public long Transferee { get; set; }
    }
}
