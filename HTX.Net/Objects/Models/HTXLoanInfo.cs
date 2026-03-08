namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Loan info
    /// </summary>
    [SerializationModel]
    public record HTXLoanInfo
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currencies</c>"] Currencies
        /// </summary>
        [JsonPropertyName("currencies")]
        public HTXLoanInfoAsset[] Assets { get; set; } = Array.Empty<HTXLoanInfoAsset>();
    }

    /// <summary>
    /// Asset info
    /// </summary>
    [SerializationModel]
    public record HTXLoanInfoAsset
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>interest-rate</c>"] Interest rate
        /// </summary>
        [JsonPropertyName("interest-rate")]
        public decimal InterestRate { get; set; }
        /// <summary>
        /// ["<c>min-loan-amt</c>"] Minimal loan quantity
        /// </summary>
        [JsonPropertyName("min-loan-amt")]
        public decimal MinLoanQuantity { get; set; }
        /// <summary>
        /// ["<c>max-loan-amt</c>"] Maximal loan quantity
        /// </summary>
        [JsonPropertyName("max-loan-amt")]
        public decimal MaxLoanQuantity { get; set; }
        /// <summary>
        /// ["<c>loanable-amt</c>"] Remaining loanable quantity
        /// </summary>
        [JsonPropertyName("loanable-amt")]
        public decimal LoanableQuantity { get; set; }
        /// <summary>
        /// ["<c>actual-rate</c>"] Actual interest rate
        /// </summary>
        [JsonPropertyName("actual-rate")]
        public decimal ActualRate { get; set; }
    }
}
