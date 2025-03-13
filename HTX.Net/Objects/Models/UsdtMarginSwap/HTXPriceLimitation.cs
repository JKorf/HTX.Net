using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// The asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// High limit
        /// </summary>
        [JsonPropertyName("high_limit")]
        public decimal HighLimit { get; set; }
        /// <summary>
        /// Low limit
        /// </summary>
        [JsonPropertyName("low_limit")]
        public decimal LowLimit { get; set; }
        /// <summary>
        /// Business type
        /// </summary>

        [JsonPropertyName("business_type")]
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
        public ContractType ContractType { get; set; }
    }
}
