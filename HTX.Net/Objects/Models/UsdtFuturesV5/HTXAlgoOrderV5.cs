using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Algo order
    /// </summary>
    [SerializationModel]
    public record HTXAlgoOrderV5
    {
        /// <summary>
        /// ["<c>id</c>"] Query id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>algo_id</c>"] Algo order id
        /// </summary>
        [JsonPropertyName("algo_id")]
        public string AlgoId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>algo_client_order_id</c>"] Algo client order id
        /// </summary>
        [JsonPropertyName("algo_client_order_id")]
        public string? AlgoClientOrderId { get; set; }
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>volume</c>"] Quantity
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Algo type
        /// </summary>
        [JsonPropertyName("type")]
        public AlgoOrderType Type { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public AlgoOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>position_side</c>"] Position side
        /// </summary>
        [JsonPropertyName("position_side")]
        public FuturesPositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
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
        /// ["<c>tp_type</c>"] Take profit order type
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
        /// ["<c>sl_type</c>"] Stop loss order type
        /// </summary>
        [JsonPropertyName("sl_type")]
        public OrderTypeV5? StopLossType { get; set; }
        /// <summary>
        /// ["<c>sl_trigger_price_type</c>"] Stop loss trigger price type
        /// </summary>
        [JsonPropertyName("sl_trigger_price_type")]
        public TriggerPriceType? StopLossTriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Trigger order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>price_match</c>"] Price match
        /// </summary>
        [JsonPropertyName("price_match")]
        public PriceMatch? PriceMatch { get; set; }
        /// <summary>
        /// ["<c>trigger_price</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("trigger_price")]
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>trigger_price_type</c>"] Trigger price type
        /// </summary>
        [JsonPropertyName("trigger_price_type")]
        public TriggerPriceType? TriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>active_price</c>"] Active price
        /// </summary>
        [JsonPropertyName("active_price")]
        public decimal? ActivePrice { get; set; }
        /// <summary>
        /// ["<c>order_price_type</c>"] Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        public PriceMatch? OrderPriceType { get; set; }
        /// <summary>
        /// ["<c>callback_rate</c>"] Callback rate
        /// </summary>
        [JsonPropertyName("callback_rate")]
        public decimal? CallbackRate { get; set; }
        /// <summary>
        /// ["<c>reduce_only</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool? ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>actual_volume</c>"] Actual quantity
        /// </summary>
        [JsonPropertyName("actual_volume")]
        public decimal? ActualQuantity { get; set; }
        /// <summary>
        /// ["<c>actual_price</c>"] Actual price
        /// </summary>
        [JsonPropertyName("actual_price")]
        public decimal? ActualPrice { get; set; }
        /// <summary>
        /// ["<c>actual_time</c>"] Actual time
        /// </summary>
        [JsonPropertyName("actual_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? ActualTime { get; set; }
        /// <summary>
        /// ["<c>relation_order_id</c>"] Relation order id
        /// </summary>
        [JsonPropertyName("relation_order_id")]
        public string? RelationOrderId { get; set; }
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
        /// ["<c>order_source</c>"] Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
    }
}
