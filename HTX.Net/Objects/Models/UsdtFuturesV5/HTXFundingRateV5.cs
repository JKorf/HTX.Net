namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Funding rate
    /// </summary>
    [SerializationModel]
    public record HTXFundingRateV5
    {
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>funding_rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>funding_time</c>"] Funding time
        /// </summary>
        [JsonPropertyName("funding_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// ["<c>next_funding_time</c>"] Next funding time
        /// </summary>
        [JsonPropertyName("next_funding_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// ["<c>min_funding_rate</c>"] Minimum funding rate
        /// </summary>
        [JsonPropertyName("min_funding_rate")]
        public decimal MinFundingRate { get; set; }
        /// <summary>
        /// ["<c>max_funding_rate</c>"] Maximum funding rate
        /// </summary>
        [JsonPropertyName("max_funding_rate")]
        public decimal MaxFundingRate { get; set; }
    }
}
