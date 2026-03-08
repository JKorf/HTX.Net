namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Funding rates page
    /// </summary>
    [SerializationModel]
    public record HTXFundingRatePage
    {
        /// <summary>
        /// ["<c>total_page</c>"] Total pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// ["<c>current_page</c>"] Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>total_size</c>"] Total results
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalResults { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Rates
        /// </summary>
        [JsonPropertyName("data")]
        public HTXHistoricalFundingRate[] Rates { get; set; } = Array.Empty<HTXHistoricalFundingRate>();
    }

    /// <summary>
    /// Historical funding rate
    /// </summary>
    [SerializationModel]
    public record HTXHistoricalFundingRate
    {
        /// <summary>
        /// ["<c>avg_premium_index</c>"] Average premium index
        /// </summary>
        [JsonPropertyName("avg_premium_index")]
        public decimal AveragePremiumIndex { get; set; }
        /// <summary>
        /// ["<c>funding_rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>realized_rate</c>"] Realized rate
        /// </summary>
        [JsonPropertyName("realized_rate")]
        public decimal? RealizedRate { get; set; }
        /// <summary>
        /// ["<c>funding_time</c>"] Funding time
        /// </summary>
        [JsonPropertyName("funding_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee_asset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
    }
}
