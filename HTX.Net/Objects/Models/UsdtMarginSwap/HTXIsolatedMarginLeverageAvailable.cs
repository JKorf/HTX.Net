using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Available leverage info
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedMarginLeverageAvailable
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

        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Available rates
        /// </summary>
        [JsonPropertyName("available_level_rate")]
        public string AvailableLevelRate { get; set; } = string.Empty;
    }
}
