using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Estimated settlement price
    /// </summary>
    [SerializationModel]
    public record HTXEstimatedSettlementPrice
    {
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>estimated_settlement_price</c>"] Estimated settlement price
        /// </summary>
        [JsonPropertyName("estimated_settlement_price")]
        public decimal? EstimatedSettlementPrice { get; set; }
        /// <summary>
        /// ["<c>settlement_type</c>"] Settlement type
        /// </summary>
        [JsonPropertyName("settlement_type")]

        public SettlementType SettlementType { get; set; }
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
