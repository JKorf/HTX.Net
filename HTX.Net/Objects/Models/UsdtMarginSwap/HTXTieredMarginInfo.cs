using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Tiered margin info
    /// </summary>
    public record HTXTieredMarginInfo
    {
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// List
        /// </summary>
        [JsonPropertyName("list")]
        public IEnumerable<HTXTieredMarginRate> List { get; set; } = Array.Empty<HTXTieredMarginRate>();
    }

    /// <summary>
    /// Tiered cross margin info
    /// </summary>
    public record HTXTieredCrossMarginInfo: HTXTieredMarginInfo
    {
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get;set; }
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

    /// <summary>
    /// Margin rate
    /// </summary>
    public record HTXTieredMarginRate
    {
        /// <summary>
        /// Lever rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public decimal LeverageRate { get; set; }
        /// <summary>
        /// Ladders
        /// </summary>
        [JsonPropertyName("ladders")]
        public IEnumerable<HTXTieredMarginLadder> Ladders { get; set; } = Array.Empty<HTXTieredMarginLadder>();
    }

    /// <summary>
    /// Ladder info
    /// </summary>
    public record HTXTieredMarginLadder
    {
        /// <summary>
        /// Min marging balance
        /// </summary>
        [JsonPropertyName("min_margin_balance")]
        public decimal? MinMarginBalance { get; set; }
        /// <summary>
        /// Max margin balance
        /// </summary>
        [JsonPropertyName("max_margin_balance")]
        public decimal? MaxMarginBalance { get; set; }
        /// <summary>
        /// Min margin available
        /// </summary>
        [JsonPropertyName("min_margin_available")]
        public decimal? MinMarginAvailable { get; set; }
        /// <summary>
        /// Max margin available
        /// </summary>
        [JsonPropertyName("max_margin_available")]
        public decimal? MaxMarginAvailable { get; set; }
    }
}
