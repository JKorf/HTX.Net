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
        /// ["<c>ts</c>"] Data timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>event</c>"] Event
        /// </summary>
        [JsonPropertyName("event")]
        public EventTrigger Event { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapIsolatedPositionUpdateData[] Data { get; set; } = Array.Empty<HTXUsdtMarginSwapIsolatedPositionUpdateData>();
        /// <summary>
        /// ["<c>uid</c>"] User id
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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
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
        /// ["<c>cost_open</c>"] Open price
        /// </summary>
        [JsonPropertyName("cost_open")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>cost_hold</c>"] Position price
        /// </summary>
        [JsonPropertyName("cost_hold")]
        public decimal PositionPrice { get; set; }
        /// <summary>
        /// ["<c>profit_unreal</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>profit_rate</c>"] Profit / loss ratio
        /// </summary>
        [JsonPropertyName("profit_rate")]
        public decimal ProfitRate { get; set; }
        /// <summary>
        /// ["<c>profit</c>"] Profit and loss
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Pnl { get; set; }
        /// <summary>
        /// ["<c>position_margin</c>"] Position margin
        /// </summary>
        [JsonPropertyName("position_margin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// ["<c>lever_rate</c>"] Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>last_price</c>"] Last price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal LastPrice { get; set; }
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
        /// ["<c>adl_risk_percent</c>"] Adl risk percent
        /// </summary>
        [JsonPropertyName("adl_risk_percent")]
        public decimal? AdlRiskPercent { get; set; }
    }


}
