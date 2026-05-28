namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Price limit
    /// </summary>
    [SerializationModel]
    public record HTXPriceLimitV5
    {
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>high_limit</c>"] High limit
        /// </summary>
        [JsonPropertyName("high_limit")]
        public decimal HighLimit { get; set; }
        /// <summary>
        /// ["<c>low_limit</c>"] Low limit
        /// </summary>
        [JsonPropertyName("low_limit")]
        public decimal LowLimit { get; set; }
    }
}
