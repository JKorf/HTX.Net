using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Position risk limit
    /// </summary>
    [SerializationModel]
    public record HTXPositionRiskLimitV5
    {
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>position_side</c>"] Position side
        /// </summary>
        [JsonPropertyName("position_side")]
        public FuturesPositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>max_lever</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("max_lever")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>maintenance_margin_rate</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintenance_margin_rate")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>max_volume</c>"] Max volume
        /// </summary>
        [JsonPropertyName("max_volume")]
        public decimal MaxVolume { get; set; }
        /// <summary>
        /// ["<c>min_volume</c>"] Min volume
        /// </summary>
        [JsonPropertyName("min_volume")]
        public decimal MinVolume { get; set; }
        /// <summary>
        /// ["<c>volume_unit</c>"] Volume unit
        /// </summary>
        [JsonPropertyName("volume_unit")]
        public string VolumeUnit { get; set; } = string.Empty;
    }
}
