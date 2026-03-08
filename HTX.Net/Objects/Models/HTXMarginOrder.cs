using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Margin order info
    /// </summary>
    [SerializationModel]
    public record HTXMarginOrder
    {
        /// <summary>
        /// ["<c>id</c>"] Order id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>account-id</c>"] Account id
        /// </summary>
        [JsonPropertyName("account-id")]
        public long AccountId { get; set; }
        /// <summary>
        /// ["<c>user-id</c>"] User id
        /// </summary>
        [JsonPropertyName("user-id")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>created-at</c>"] Creation time
        /// </summary>
        [JsonPropertyName("created-at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>accrued-at</c>"] Accrue time
        /// </summary>
        [JsonPropertyName("accrued-at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? AccrueTime { get; set; }
        /// <summary>
        /// ["<c>loan-amount</c>"] Loan quantity
        /// </summary>
        [JsonPropertyName("loan-amount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// ["<c>loan-balance</c>"] Loan balance left
        /// </summary>
        [JsonPropertyName("loan-balance")]
        public decimal LoanBalance { get; set; }
        /// <summary>
        /// ["<c>interest-rate</c>"] Interst rate
        /// </summary>
        [JsonPropertyName("interest-rate")]
        public decimal? InterestRate { get; set; }
        /// <summary>
        /// ["<c>interest-amount</c>"] Interest quantity
        /// </summary>
        [JsonPropertyName("interest-amount")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// ["<c>interest-balance</c>"] Interest left
        /// </summary>
        [JsonPropertyName("interest-balance")]
        public decimal InterestBalance { get; set; }
        /// <summary>
        /// ["<c>state</c>"] State
        /// </summary>

        [JsonPropertyName("state")]
        public MarginOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>paid-point</c>"] Paid HTX points
        /// </summary>
        [JsonPropertyName("paid-point")]
        public decimal PaidPoints { get; set; }
        /// <summary>
        /// ["<c>paid-coin</c>"] Paid asset
        /// </summary>
        [JsonPropertyName("paid-coin")]
        public decimal PaidAsset { get; set; }
        /// <summary>
        /// ["<c>filled-points</c>"] Filled HTX points
        /// </summary>
        [JsonPropertyName("filled-points")]
        public decimal FilledPoints { get; set; }
        /// <summary>
        /// ["<c>filled-ht</c>"] HT deduction amount
        /// </summary>
        [JsonPropertyName("filled-ht")]
        public decimal FilledHt { get; set; }
        /// <summary>
        /// ["<c>deduct-rate</c>"] Deduct rate
        /// </summary>
        [JsonPropertyName("deduct-rate")]
        public decimal? DeductRate { get; set; }
        /// <summary>
        /// ["<c>deduct-currency</c>"] Deduct asset
        /// </summary>
        [JsonPropertyName("deduct-currency")]
        public string? DeductAsset { get; set; }
        /// <summary>
        /// ["<c>deduct-amount</c>"] Deduct quantity
        /// </summary>
        [JsonPropertyName("deduct-amount")]
        public decimal? DeductQuantity { get; set; }
        /// <summary>
        /// ["<c>updated-at</c>"] Last updated
        /// </summary>
        [JsonPropertyName("updated-at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>hour-interest-rate</c>"] Hourly interest rate
        /// </summary>
        [JsonPropertyName("hour-interest-rate")]
        public decimal? HourInterestRate { get; set; }
        /// <summary>
        /// ["<c>day-interest-rate</c>"] Daily interest rate
        /// </summary>
        [JsonPropertyName("day-interest-rate")]
        public decimal? DayInterestRate { get; set; }
    }
}
