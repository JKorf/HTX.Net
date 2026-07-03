namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Funding rate
    /// </summary>
    [SerializationModel]
    public record HTXFundingRate
    {
        /// <summary>
        /// ["<c>funding_rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal? FundingRate { get; set; }
        /// <summary>
        /// ["<c>estimated_rate</c>"] Estimated rate
        /// </summary>
        [JsonPropertyName("estimated_rate")]
        public decimal? EstimatedRate { get; set; }
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
        /// <summary>
        /// ["<c>funding_time</c>"] Funding time
        /// </summary>
        [JsonPropertyName("funding_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? FundingTime { get; set; }
        /// <summary>
        /// ["<c>next_funding_time</c>"] Next funding time
        /// </summary>
        [JsonPropertyName("next_funding_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? NextFundingTime { get; set; }
        /// <summary>
        /// ["<c>trade_partition</c>"] Trade partition
        /// </summary>
        [JsonPropertyName("trade_partition")]
        public string TradePartition { get; set; } = string.Empty;
    }
}
