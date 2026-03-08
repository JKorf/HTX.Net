using HTX.Net.Enums;
using HTX.Net.Objects.Sockets;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapOrderUpdate : HTXOpMessage
    {
        /// <summary>
        /// ["<c>ts</c>"] Update timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>uid</c>"] User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
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
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>order_price_type</c>"] Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        public OrderPriceType OrderPriceType { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Order side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>offset</c>"] Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public Offset Offset { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatusFilter OrderStatus { get; set; }
        /// <summary>
        /// ["<c>lever_rate</c>"] Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>order_id_str</c>"] Order id string
        /// </summary>
        [JsonPropertyName("order_id_str")]
        public string OrderIdStr { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public long? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>order_source</c>"] Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_type</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public int OrderType { get; set; }
        /// <summary>
        /// ["<c>created_at</c>"] Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// ["<c>trade_volume</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Value filled in quote asset
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal ValueFilled { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>liquidation_type</c>"] Liquidation type
        /// </summary>
        [JsonPropertyName("liquidation_type")]
        public LiquidationType? LiquidationType { get; set; }
        /// <summary>
        /// ["<c>trade_avg_price</c>"] Average trade price
        /// </summary>
        [JsonPropertyName("trade_avg_price")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// ["<c>margin_asset</c>"] Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_frozen</c>"] Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// ["<c>profit</c>"] Profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }
        /// <summary>
        /// ["<c>canceled_at</c>"] Canceled at
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public DateTime? CanceledAt { get; set; }
        /// <summary>
        /// ["<c>fee_asset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
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
        /// ["<c>is_tpsl</c>"] Is take profit / stop loss
        /// </summary>
        [JsonPropertyName("is_tpsl")]
        public bool IsTpsl { get; set; }
        /// <summary>
        /// ["<c>real_profit</c>"] Profit and loss
        /// </summary>
        [JsonPropertyName("real_profit")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>reduce_only</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// Canceled source
        /// </summary>
        [JsonPropertyName("canceled_source"), JsonConverter(typeof(NumberStringConverter))]
        public string? CanceledSource { get; set; }
        /// <summary>
        /// ["<c>trade</c>"] Trade info
        /// </summary>
        [JsonPropertyName("trade")]
        public HTXUsdtMarginSwapOrderUpdateTrade[] Trade { get; set; } = Array.Empty<HTXUsdtMarginSwapOrderUpdateTrade>();
    }

    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapOrderUpdateTrade
    {
        /// <summary>
        /// ["<c>trade_id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long TradeId { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>trade_volume</c>"] Trade quantity
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>trade_price</c>"] Trade price
        /// </summary>
        [JsonPropertyName("trade_price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>trade_fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("trade_fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>fee_asset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Deduction asset price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? DeductionAssetPrice { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Trade value
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal Value { get; set; }
        /// <summary>
        /// ["<c>created_at</c>"] Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// ["<c>role</c>"] Role
        /// </summary>
        [JsonPropertyName("role")]
        public OrderRole Role { get; set; }
        /// <summary>
        /// ["<c>profit</c>"] Profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }
        /// <summary>
        /// ["<c>real_profit</c>"] Real profit
        /// </summary>
        [JsonPropertyName("real_profit")]
        public decimal RealProfit { get; set; }
    }


}
