namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Funding rates page
    /// </summary>
    public record HTXFundingRatePage
    {
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total results
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalResults { get; set; }
        /// <summary>
        /// Rates
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<HTXHistoricalFundingRate> Rates { get; set; } = Array.Empty<HTXHistoricalFundingRate>();
    }

    /// <summary>
    /// Historical funding rate
    /// </summary>
    public record HTXHistoricalFundingRate
    {
        /// <summary>
        /// Average premium index
        /// </summary>
        [JsonPropertyName("avg_premium_index")]
        public decimal AveragePremiumIndex { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Realized rate
        /// </summary>
        [JsonPropertyName("realized_rate")]
        public decimal? RealizedRate { get; set; }
        /// <summary>
        /// Funding time
        /// </summary>
        [JsonPropertyName("funding_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
    }
}
