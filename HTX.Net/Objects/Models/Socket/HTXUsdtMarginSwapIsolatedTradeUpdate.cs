using HTX.Net.Objects.Sockets;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order trade update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapIsolatedTradeUpdate : HTXOpMessage
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
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
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatusFilter Status { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Order id string
        /// </summary>
        [JsonPropertyName("order_id_str")]
        public string OrderIdStr { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public long? ClientOrderId { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public MarginOrderType OrderType { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Trades
        /// </summary>
        [JsonPropertyName("trade")]
        public HTXUsdtMarginSwapIsolatedTradeUpdateTrade[] Trades { get; set; } = Array.Empty<HTXUsdtMarginSwapIsolatedTradeUpdateTrade>();
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public Offset Offset { get; set; }
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        public OrderPriceType OrderPriceType { get; set; }
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
        /// Is take profit / stop loss
        /// </summary>
        [JsonPropertyName("is_tpsl")]
        public bool IsTpsl { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapIsolatedTradeUpdateTrade
    {
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long TradeId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("trade_price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal Value { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Role
        /// </summary>
        [JsonPropertyName("role")]
        public OrderRole Role { get; set; }
    }


}
