using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Repayment info
    /// </summary>
    [SerializationModel]
    public record HTXRepayment
    {
        /// <summary>
        /// Repayment id
        /// </summary>
        [JsonPropertyName("repayId")]
        public long RepayId { get; set; }
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// Repay time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("repayTime")]
        public DateTime RepayTime { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Repay quantity
        /// </summary>
        [JsonPropertyName("repaidAmount")]
        public decimal RepaidQuantity { get; set; }
        /// <summary>
        /// Transactions
        /// </summary>
        [JsonPropertyName("transactIds")]
        public HTXRepayTransaction Transactions { get; set; } = null!;
    }

    /// <summary>
    /// Repayment transaction
    /// </summary>
    [SerializationModel]
    public record HTXRepayTransaction
    {
        /// <summary>
        /// Transact id
        /// </summary>
        [JsonPropertyName("transactId")]
        public long TransactId { get; set; }
        /// <summary>
        /// Principal repaid
        /// </summary>
        [JsonPropertyName("repaidprincipal")]
        public decimal RepaidPrincipal { get; set; }
        /// <summary>
        /// Interest repaid
        /// </summary>
        [JsonPropertyName("repaidInterest")]
        public decimal RepaidInterest { get; set; }
        /// <summary>
        /// HT paid
        /// </summary>
        [JsonPropertyName("paidHt")]
        public decimal PaidHt { get; set; }
        /// <summary>
        /// Points paid
        /// </summary>
        [JsonPropertyName("paidPoint")]
        public decimal PaidPoint { get; set; }
    }
}
