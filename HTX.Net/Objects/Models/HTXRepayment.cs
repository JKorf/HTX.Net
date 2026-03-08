namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Repayment info
    /// </summary>
    [SerializationModel]
    public record HTXRepayment
    {
        /// <summary>
        /// ["<c>repayId</c>"] Repayment id
        /// </summary>
        [JsonPropertyName("repayId")]
        public long RepayId { get; set; }
        /// <summary>
        /// ["<c>accountId</c>"] Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// ["<c>repayTime</c>"] Repay time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("repayTime")]
        public DateTime RepayTime { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>repaidAmount</c>"] Repay quantity
        /// </summary>
        [JsonPropertyName("repaidAmount")]
        public decimal RepaidQuantity { get; set; }
        /// <summary>
        /// ["<c>transactIds</c>"] Transactions
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
        /// ["<c>transactId</c>"] Transact id
        /// </summary>
        [JsonPropertyName("transactId")]
        public long TransactId { get; set; }
        /// <summary>
        /// ["<c>repaidprincipal</c>"] Principal repaid
        /// </summary>
        [JsonPropertyName("repaidprincipal")]
        public decimal RepaidPrincipal { get; set; }
        /// <summary>
        /// ["<c>repaidInterest</c>"] Interest repaid
        /// </summary>
        [JsonPropertyName("repaidInterest")]
        public decimal RepaidInterest { get; set; }
        /// <summary>
        /// ["<c>paidHt</c>"] HT paid
        /// </summary>
        [JsonPropertyName("paidHt")]
        public decimal PaidHt { get; set; }
        /// <summary>
        /// ["<c>paidPoint</c>"] Points paid
        /// </summary>
        [JsonPropertyName("paidPoint")]
        public decimal PaidPoint { get; set; }
    }
}
