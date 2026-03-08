using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Tiered margin info
    /// </summary>
    [SerializationModel]
    public record HTXTieredMarginInfo
    {
        /// <summary>
        /// ["<c>margin_account</c>"] Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
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
        /// ["<c>list</c>"] List
        /// </summary>
        [JsonPropertyName("list")]
        public HTXTieredMarginRate[] List { get; set; } = Array.Empty<HTXTieredMarginRate>();
    }

    /// <summary>
    /// Tiered cross margin info
    /// </summary>
    [SerializationModel]
    public record HTXTieredCrossMarginInfo: HTXTieredMarginInfo
    {
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]

        public BusinessType BusinessType { get;set; }
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
    /// Margin rate
    /// </summary>
    [SerializationModel]
    public record HTXTieredMarginRate
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
        public HTXTieredMarginLadder[] Ladders { get; set; } = Array.Empty<HTXTieredMarginLadder>();
    }

    /// <summary>
    /// Ladder info
    /// </summary>
    [SerializationModel]
    public record HTXTieredMarginLadder
    {
        /// <summary>
        /// ["<c>min_margin_balance</c>"] Min marging balance
        /// </summary>
        [JsonPropertyName("min_margin_balance")]
        public decimal? MinMarginBalance { get; set; }
        /// <summary>
        /// ["<c>max_margin_balance</c>"] Max margin balance
        /// </summary>
        [JsonPropertyName("max_margin_balance")]
        public decimal? MaxMarginBalance { get; set; }
        /// <summary>
        /// ["<c>min_margin_available</c>"] Min margin available
        /// </summary>
        [JsonPropertyName("min_margin_available")]
        public decimal? MinMarginAvailable { get; set; }
        /// <summary>
        /// ["<c>max_margin_available</c>"] Max margin available
        /// </summary>
        [JsonPropertyName("max_margin_available")]
        public decimal? MaxMarginAvailable { get; set; }
    }
}
