using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Match order update
    /// </summary>
    [SerializationModel]
    public record HTXMatchOrderUpdateV5
    {
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderTypeV5 Type { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Quantity
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public OrderStatusV5 Status { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>position_side</c>"] Position side
        /// </summary>
        [JsonPropertyName("position_side")]
        public FuturesPositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>price_match</c>"] Price match
        /// </summary>
        [JsonPropertyName("price_match")]
        public PriceMatch? PriceMatch { get; set; }
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>lever_rate</c>"] Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public string LeverageRate { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_source</c>"] Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>reduce_only</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>time_in_force</c>"] Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>cancel_reason</c>"] Cancel reason
        /// </summary>
        [JsonPropertyName("cancel_reason")]
        public string? CancelReason { get; set; }
        /// <summary>
        /// ["<c>trade_id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>trade_volume</c>"] Trade quantity
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal TradeQuantity { get; set; }
        /// <summary>
        /// ["<c>total_trade_volume</c>"] Total trade quantity
        /// </summary>
        [JsonPropertyName("total_trade_volume")]
        public decimal TotalTradeQuantity { get; set; }
        /// <summary>
        /// ["<c>trade_price</c>"] Trade price
        /// </summary>
        [JsonPropertyName("trade_price")]
        public decimal TradePrice { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Trade turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal TradeTurnover { get; set; }
        /// <summary>
        /// ["<c>role</c>"] Role
        /// </summary>
        [JsonPropertyName("role")]
        public OrderRole Role { get; set; }
        /// <summary>
        /// ["<c>created_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("created_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>match_time</c>"] Match time
        /// </summary>
        [JsonPropertyName("match_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime MatchTime { get; set; }
        /// <summary>
        /// ["<c>self_match_prevent</c>"] Self match prevention
        /// </summary>
        [JsonPropertyName("self_match_prevent")]
        public SelfMatchPrevent? SelfMatchPrevent { get; set; }
        /// <summary>
        /// ["<c>in_time</c>"] In time
        /// </summary>
        [JsonPropertyName("in_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? InTime { get; set; }
    }
}
