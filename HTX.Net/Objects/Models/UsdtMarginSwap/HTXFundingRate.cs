namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Funding rate
    /// </summary>
    [SerializationModel]
    public record HTXFundingRate
    {
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal? FundingRate { get; set; }
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
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("funding_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime FundingTime { get; set; }
    }
}
