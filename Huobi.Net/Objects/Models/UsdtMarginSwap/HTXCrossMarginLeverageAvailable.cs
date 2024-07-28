using CryptoExchange.Net.Converters;
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Available leverage info
    /// </summary>
    public record HTXCrossMarginLeverageAvailable: HTXIsolatedMarginLeverageAvailable
    {
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
    }
}
