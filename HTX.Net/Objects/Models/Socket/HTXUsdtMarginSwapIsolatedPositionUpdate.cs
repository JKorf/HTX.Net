using HTX.Net.Enums;
using HTX.Net.Objects.Sockets;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Position data update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapIsolatedPositionUpdate : HTXOpMessage
    {
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Event
        /// </summary>
        [JsonPropertyName("event")]
        public EventTrigger Event { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapIsolatedPositionUpdateData[] Data { get; set; } = Array.Empty<HTXUsdtMarginSwapIsolatedPositionUpdateData>();
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
    }

    /// <summary>
    /// Position data
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapIsolatedPositionUpdateData
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("cost_open")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Position price
        /// </summary>
        [JsonPropertyName("cost_hold")]
        public decimal PositionPrice { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Profit / loss ratio
        /// </summary>
        [JsonPropertyName("profit_rate")]
        public decimal ProfitRate { get; set; }
        /// <summary>
        /// Profit and loss
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Pnl { get; set; }
        /// <summary>
        /// Position margin
        /// </summary>
        [JsonPropertyName("position_margin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal LastPrice { get; set; }
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
        /// Adl risk percent
        /// </summary>
        [JsonPropertyName("adl_risk_percent")]
        public decimal? AdlRiskPercent { get; set; }
    }


}
