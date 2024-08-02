using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Margin order info
    /// </summary>
    public record HTXMarginOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("account-id")]
        public long AccountId { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user-id")]
        public long UserId { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("created-at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Accrue time
        /// </summary>
        [JsonPropertyName("accrued-at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? AccrueTime { get; set; }
        /// <summary>
        /// Loan quantity
        /// </summary>
        [JsonPropertyName("loan-amount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// Loan balance left
        /// </summary>
        [JsonPropertyName("loan-balance")]
        public decimal LoanBalance { get; set; }
        /// <summary>
        /// Interst rate
        /// </summary>
        [JsonPropertyName("interest-rate")]
        public decimal? InterestRate { get; set; }
        /// <summary>
        /// Interest quantity
        /// </summary>
        [JsonPropertyName("interest-amount")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// Interest left
        /// </summary>
        [JsonPropertyName("interest-balance")]
        public decimal InterestBalance { get; set; }
        /// <summary>
        /// State
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("state")]
        public MarginOrderStatus Status { get; set; }
        /// <summary>
        /// Paid HTX points
        /// </summary>
        [JsonPropertyName("paid-point")]
        public decimal PaidPoints { get; set; }
        /// <summary>
        /// Paid asset
        /// </summary>
        [JsonPropertyName("paid-coin")]
        public decimal PaidAsset { get; set; }
        /// <summary>
        /// Filled HTX points
        /// </summary>
        [JsonPropertyName("filled-points")]
        public decimal FilledPoints { get; set; }
        /// <summary>
        /// HT deduction amount
        /// </summary>
        [JsonPropertyName("filled-ht")]
        public decimal FilledHt { get; set; }
        /// <summary>
        /// Deduct rate
        /// </summary>
        [JsonPropertyName("deduct-rate")]
        public decimal? DeductRate { get; set; }
        /// <summary>
        /// Deduct asset
        /// </summary>
        [JsonPropertyName("deduct-currency")]
        public string? DeductAsset { get; set; }
        /// <summary>
        /// Deduct quantity
        /// </summary>
        [JsonPropertyName("deduct-amount")]
        public decimal? DeductQuantity { get; set; }
        /// <summary>
        /// Last updated
        /// </summary>
        [JsonPropertyName("updated-at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Hourly interest rate
        /// </summary>
        [JsonPropertyName("hour-interest-rate")]
        public decimal? HourInterestRate { get; set; }
        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonPropertyName("day-interest-rate")]
        public decimal? DayInterestRate { get; set; }
    }
}
