using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedMarginAccountInfo
    {
        /// <summary>
        /// ["<c>symbol</c>"] Asset info
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_balance</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// ["<c>margin_position</c>"] Margin position
        /// </summary>
        [JsonPropertyName("margin_position")]
        public decimal MarginPosition { get; set; }
        /// <summary>
        /// ["<c>margin_frozen</c>"] Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// ["<c>margin_available</c>"] Margin available
        /// </summary>
        [JsonPropertyName("margin_available")]
        public decimal? MarginAvailable { get; set; }
        /// <summary>
        /// ["<c>profit_real</c>"] Profit real
        /// </summary>
        [JsonPropertyName("profit_real")]
        public decimal RealizedProfit { get; set; }
        /// <summary>
        /// ["<c>profit_unreal</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>risk_rate</c>"] Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// ["<c>new_risk_rate</c>"] Risk rate
        /// </summary>
        [JsonPropertyName("new_risk_rate")]
        public decimal? NewRiskRate { get; set; }
        /// <summary>
        /// ["<c>withdraw_available</c>"] Withdraw available
        /// </summary>
        [JsonPropertyName("withdraw_available")]
        public decimal WithdrawAvailable { get; set; }
        /// <summary>
        /// ["<c>liquidation_price</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>lever_rate</c>"] Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public decimal LeverageRate { get; set; }
        /// <summary>
        /// ["<c>adjust_factor</c>"] Adjust factor
        /// </summary>
        [JsonPropertyName("adjust_factor")]
        public decimal AdjustFactor { get; set; }
        /// <summary>
        /// ["<c>margin_static</c>"] Margin static
        /// </summary>
        [JsonPropertyName("margin_static")]
        public decimal MarginStatic { get; set; }
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
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
        /// ["<c>trade_partition</c>"] Trade partition
        /// </summary>
        [JsonPropertyName("trade_partition")]
        public string TradePartition { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>adl_risk_percent</c>"] ADL risk percentage
        /// </summary>
        [JsonPropertyName("adl_risk_percent")]
        public decimal? AdlRiskPercentage { get; set; }
    }
}
