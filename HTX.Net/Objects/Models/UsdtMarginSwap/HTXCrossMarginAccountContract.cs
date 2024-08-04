using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Cross margin account contract details
    /// </summary>
    public record HTXCrossMarginAccountContract
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
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
        public decimal MarginAvailable { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
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
        /// Cross max available
        /// </summary>
        [JsonPropertyName("cross_max_available")]
        public decimal? CrossMaxAvaialble { get; set; }
        /// <summary>
        /// Adjust factor
        /// </summary>
        [JsonPropertyName("adjust_factor")]
        public decimal AdjustFactor { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Trade partition
        /// </summary>
        [JsonPropertyName("trade_partition")]
        public string TradePartition { get; set; } = string.Empty;
    }
}
