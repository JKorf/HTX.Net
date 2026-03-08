using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Swap risk info
    /// </summary>
    [SerializationModel]
    public record HTXSwapRiskInfo
    {
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>insurance_fund</c>"] Insurance fund
        /// </summary>
        [JsonPropertyName("insurance_fund")]
        public decimal InsuranceFund { get; set; }
        /// <summary>
        /// ["<c>estimated_clawback</c>"] Estimated clawback
        /// </summary>
        [JsonPropertyName("estimated_clawback")]
        public decimal EstimatedClawback { get; set; }
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinuessType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
