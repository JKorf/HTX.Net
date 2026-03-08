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
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]

        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>available_level_rate</c>"] Available rates
        /// </summary>
        [JsonPropertyName("available_level_rate")]
        public string AvailableLevelRate { get; set; } = string.Empty;
    }
}
