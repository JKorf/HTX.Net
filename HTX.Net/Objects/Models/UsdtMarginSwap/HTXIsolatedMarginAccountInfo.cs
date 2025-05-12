using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Asset info
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Margin position
        /// </summary>
        [JsonPropertyName("margin_position")]
        public decimal MarginPosition { get; set; }
        /// <summary>
        /// Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// Margin available
        /// </summary>
        [JsonPropertyName("margin_available")]
        public decimal? MarginAvailable { get; set; }
        /// <summary>
        /// Profit real
        /// </summary>
        [JsonPropertyName("profit_real")]
        public decimal RealizedProfit { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonPropertyName("new_risk_rate")]
        public decimal? NewRiskRate { get; set; }
        /// <summary>
        /// Withdraw available
        /// </summary>
        [JsonPropertyName("withdraw_available")]
        public decimal WithdrawAvailable { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public decimal LeverageRate { get; set; }
        /// <summary>
        /// Adjust factor
        /// </summary>
        [JsonPropertyName("adjust_factor")]
        public decimal AdjustFactor { get; set; }
        /// <summary>
        /// Margin static
        /// </summary>
        [JsonPropertyName("margin_static")]
        public decimal MarginStatic { get; set; }
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>

        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]

        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// Trade partition
        /// </summary>
        [JsonPropertyName("trade_partition")]
        public string TradePartition { get; set; } = string.Empty;
        /// <summary>
        /// ADL risk percentage
        /// </summary>
        [JsonPropertyName("adl_risk_percent")]
        public decimal? AdlRiskPercentage { get; set; }
    }
}
