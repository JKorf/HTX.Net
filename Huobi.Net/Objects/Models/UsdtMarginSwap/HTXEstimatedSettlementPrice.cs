using CryptoExchange.Net.Converters;
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Estimated settlement price
    /// </summary>
    public record HTXEstimatedSettlementPrice
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Estimated settlement price
        /// </summary>
        [JsonPropertyName("estimated_settlement_price")]
        public decimal? EstimatedSettlementPrice { get; set; }
        /// <summary>
        /// Settlement type
        /// </summary>
        [JsonPropertyName("settlement_type")]
        [JsonConverter(typeof(EnumConverter))]
        public SettlementType SettlementType { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
    }
}
