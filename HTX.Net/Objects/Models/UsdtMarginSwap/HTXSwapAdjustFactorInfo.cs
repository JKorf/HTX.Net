using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Adjust factor info
    /// </summary>
    [SerializationModel]
    public record HTXSwapAdjustFactorInfo
    {
        /// <summary>
        /// ["<c>symbol</c>"] Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
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
        /// ["<c>list</c>"] list
        /// </summary>
        [JsonPropertyName("list")]
        public HTXFactorInfo[] List { get; set; } = Array.Empty<HTXFactorInfo>();
    }

    /// <summary>
    /// Cross margin adjust factor info
    /// </summary>
    [SerializationModel]
    public record HTXCrossSwapAdjustFactorInfo: HTXSwapAdjustFactorInfo
    {
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

    /// <summary>
    /// Factor info
    /// </summary>
    [SerializationModel]
    public record HTXFactorInfo
    {
        /// <summary>
        /// ["<c>lever_rate</c>"] Lever rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public decimal LeverageRate { get; set; }
        /// <summary>
        /// ["<c>ladders</c>"] Ladders
        /// </summary>
        [JsonPropertyName("ladders")]
        public HTXFactorEntry[] Ladders { get; set; } = Array.Empty<HTXFactorEntry>();
    }

    /// <summary>
    /// Factor info
    /// </summary>
    [SerializationModel]
    public record HTXFactorEntry
    {
        /// <summary>
        /// ["<c>ladder</c>"] Ladder
        /// </summary>
        [JsonPropertyName("ladder")]
        public int Ladder { get; set; }
        /// <summary>
        /// ["<c>min_size</c>"] Min size
        /// </summary>
        [JsonPropertyName("min_size")]
        public int? MinSize { get; set; }
        /// <summary>
        /// ["<c>max_size</c>"] Max size
        /// </summary>
        [JsonPropertyName("max_size")]
        public int? MaxSize { get; set; }
        /// <summary>
        /// ["<c>adjust_factor</c>"] Adjust factor
        /// </summary>
        [JsonPropertyName("adjust_factor")]
        public decimal AdjustFactor { get; set; }
    }
}
