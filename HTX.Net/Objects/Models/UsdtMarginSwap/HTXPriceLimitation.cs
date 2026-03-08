using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Price limitation
    /// </summary>
    [SerializationModel]
    public record HTXPriceLimitation
    {
        /// <summary>
        /// ["<c>symbol</c>"] The asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
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
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>

        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>

        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
    }
}
