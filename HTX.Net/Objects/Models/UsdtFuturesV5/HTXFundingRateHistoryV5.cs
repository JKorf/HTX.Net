namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Funding rate history
    /// </summary>
    [SerializationModel]
    public record HTXFundingRateHistoryV5
    {
        /// <summary>
        /// ["<c>id</c>"] Query id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
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
    }
}
