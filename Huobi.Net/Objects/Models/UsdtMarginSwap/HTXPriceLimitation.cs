using CryptoExchange.Net.Converters;
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Price limitation
    /// </summary>
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
        [JsonConverter(typeof(EnumConverter))]
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
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
    }
}
