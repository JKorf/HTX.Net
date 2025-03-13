using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Leverage rate
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedMarginLeverageRate
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]

        public MarginMode Mode { get; set; }
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
    }

    /// <summary>
    /// Leverage rate
    /// </summary>
    [SerializationModel]
    public record HTXCrossMarginLeverageRate: HTXIsolatedMarginLeverageRate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]

        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]

        public ContractType ContractType { get; set; }
    }
}
