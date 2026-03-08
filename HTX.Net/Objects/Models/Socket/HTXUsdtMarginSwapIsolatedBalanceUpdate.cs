using HTX.Net.Objects.Sockets;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Isolated balance update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapIsolatedBalanceUpdate: HTXOpMessage
    {
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>event</c>"] Event trigger
        /// </summary>
        [JsonPropertyName("event")]
        public EventTrigger Event { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapIsolatedBalanceUpdateData[] Data { get; set; } = Array.Empty<HTXUsdtMarginSwapIsolatedBalanceUpdateData>();
        /// <summary>
        /// ["<c>uid</c>"] User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
    }

    /// <summary>
    /// Isolated margin balance update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapIsolatedBalanceUpdateData
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
        /// ["<c>margin_balance</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// ["<c>margin_static</c>"] Static equity
        /// </summary>
        [JsonPropertyName("margin_static")]
        public decimal MarginStatic { get; set; }
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
        public decimal MarginAvailable { get; set; }
        /// <summary>
        /// ["<c>profit_real</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("profit_real")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>profit_unreal</c>"] Unrealized proft and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>withdraw_available</c>"] Withdraw available
        /// </summary>
        [JsonPropertyName("withdraw_available")]
        public decimal WithdrawAvailable { get; set; }
        /// <summary>
        /// ["<c>risk_rate</c>"] Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
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
    }


}
