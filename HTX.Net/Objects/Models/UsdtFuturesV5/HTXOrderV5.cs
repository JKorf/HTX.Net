using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Order
    /// </summary>
    [SerializationModel]
    public record HTXOrderV5
    {
        /// <summary>
        /// ["<c>id</c>"] Query id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>position_side</c>"] Position side
        /// </summary>
        [JsonPropertyName("position_side")]
        public FuturesPositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderTypeV5 Type { get; set; }
        /// <summary>
        /// ["<c>price_match</c>"] Price match
        /// </summary>
        [JsonPropertyName("price_match")]
        public PriceMatch? PriceMatch { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
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
        /// ["<c>lever_rate</c>"] Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Order status
        /// </summary>
        [JsonPropertyName("state")]
        public OrderStatusV5 Status { get; set; }
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
        /// ["<c>tp_trigger_price</c>"] Take profit trigger price
        /// </summary>
        [JsonPropertyName("tp_trigger_price")]
        public decimal? TakeProfitTriggerPrice { get; set; }
        /// <summary>
        /// ["<c>tp_order_price</c>"] Take profit order price
        /// </summary>
        [JsonPropertyName("tp_order_price")]
        public decimal? TakeProfitOrderPrice { get; set; }
        /// <summary>
        /// ["<c>tp_type</c>"] Take profit type
        /// </summary>
        [JsonPropertyName("tp_type")]
        public OrderTypeV5? TakeProfitType { get; set; }
        /// <summary>
        /// ["<c>tp_trigger_price_type</c>"] Take profit trigger price type
        /// </summary>
        [JsonPropertyName("tp_trigger_price_type")]
        public TriggerPriceType? TakeProfitTriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>sl_trigger_price</c>"] Stop loss trigger price
        /// </summary>
        [JsonPropertyName("sl_trigger_price")]
        public decimal? StopLossTriggerPrice { get; set; }
        /// <summary>
        /// ["<c>sl_order_price</c>"] Stop loss order price
        /// </summary>
        [JsonPropertyName("sl_order_price")]
        public decimal? StopLossOrderPrice { get; set; }
        /// <summary>
        /// ["<c>sl_type</c>"] Stop loss type
        /// </summary>
        [JsonPropertyName("sl_type")]
        public OrderTypeV5? StopLossType { get; set; }
        /// <summary>
        /// ["<c>sl_trigger_price_type</c>"] Stop loss trigger price type
        /// </summary>
        [JsonPropertyName("sl_trigger_price_type")]
        public TriggerPriceType? StopLossTriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>trade_avg_price</c>"] Average trade price
        /// </summary>
        [JsonPropertyName("trade_avg_price")]
        public decimal? AverageTradePrice { get; set; }
        /// <summary>
        /// ["<c>trade_volume</c>"] Trade quantity
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal TradeQuantity { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Trade turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal TradeTurnover { get; set; }
        /// <summary>
        /// ["<c>fee_currency</c>"] Fee currency
        /// </summary>
        [JsonPropertyName("fee_currency")]
        public string FeeCurrency { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>price_protect</c>"] Price protect
        /// </summary>
        [JsonPropertyName("price_protect")]
        public bool? PriceProtect { get; set; }
        /// <summary>
        /// ["<c>profit</c>"] Profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal? Profit { get; set; }
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>cancel_reason</c>"] Cancel reason
        /// </summary>
        [JsonPropertyName("cancel_reason")]
        public string CancelReason { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>created_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("created_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updated_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("updated_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>self_match_prevent</c>"] Self match prevention
        /// </summary>
        [JsonPropertyName("self_match_prevent")]
        public SelfMatchPrevent? SelfMatchPrevent { get; set; }
    }
}
