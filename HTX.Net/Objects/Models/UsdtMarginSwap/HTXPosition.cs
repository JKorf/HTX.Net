using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin position info
    /// </summary>
    [SerializationModel]
    public record HTXPosition
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
        /// ["<c>volume</c>"] Quantity
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>frozen</c>"] Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>cost_open</c>"] Opening average price
        /// </summary>
        [JsonPropertyName("cost_open")]
        public decimal CostOpen { get; set; }
        /// <summary>
        /// ["<c>cost_hold</c>"] Average price of position
        /// </summary>
        [JsonPropertyName("cost_hold")]
        public decimal CostHold { get; set; }
        /// <summary>
        /// ["<c>profit_unreal</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>profit_rate</c>"] Profit rate
        /// </summary>
        [JsonPropertyName("profit_rate")]
        public decimal ProfitRate { get; set; }
        /// <summary>
        /// ["<c>lever_rate</c>"] Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public decimal LeverageRate { get; set; }
        /// <summary>
        /// ["<c>position_margin</c>"] Position margin
        /// </summary>
        [JsonPropertyName("position_margin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Order direction
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>profit</c>"] Profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }
        /// <summary>
        /// ["<c>last_price</c>"] Last price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>new_risk_rate</c>"] New risk rate
        /// </summary>
        [JsonPropertyName("new_risk_rate")]
        public decimal? NewRiskRate { get; set; }
        /// <summary>
        /// ["<c>margin_asset</c>"] Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]

        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>margin_account</c>"] Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>position_mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]

        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// ["<c>adl_risk_percent</c>"] ADL risk percentage
        /// </summary>
        [JsonPropertyName("adl_risk_percent")]
        public decimal? AdlRiskPercentage { get; set; }
        /// <summary>
        /// ["<c>trade_partition</c>"] Trade partition
        /// </summary>
        [JsonPropertyName("trade_partition")]
        public string? TradePartition { get; set; }
    }

    /// <summary>
    /// Cross margin position
    /// </summary>
    [SerializationModel]
    public record HTXCrossPosition : HTXPosition
    {
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
    }
}
