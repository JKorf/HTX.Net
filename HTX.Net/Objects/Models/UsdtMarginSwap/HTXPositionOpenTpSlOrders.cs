using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Position open and tp/sl info
    /// </summary>
    [SerializationModel]
    public record HTXPositionOpenTpSlOrders
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
        /// ["<c>direction</c>"] Side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>offset</c>"] Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public Offset Offset { get; set; }
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
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>created_at</c>"] Creation time
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>trade_volume</c>"] Filled quantity
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Value filled
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal ValueFilled { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>trade_avg_price</c>"] Average trade price
        /// </summary>
        [JsonPropertyName("trade_avg_price")]
        public decimal AveragePrice { get; set; }
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
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatusFilter Status { get; set; }
        /// <summary>
        /// ["<c>order_type</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public MarginOrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>order_source</c>"] Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee_asset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>canceled_at</c>"] Cancelation time
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// ["<c>tpsl_order_info</c>"] Tpsl order info
        /// </summary>
        [JsonPropertyName("tpsl_order_info")]
        public HTXPositionOpenTpSlOrdersSUB[] TpslOrderInfo { get; set; } = Array.Empty<HTXPositionOpenTpSlOrdersSUB>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record HTXPositionOpenTpSlOrdersSUB
    {
        /// <summary>
        /// ["<c>volume</c>"] Quantity
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>tpsl_order_type</c>"] Tpsl order type
        /// </summary>
        [JsonPropertyName("tpsl_order_type")]
        public TpslOrderType TpslOrderType { get; set; }
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
        /// ["<c>trigger_type</c>"] Trigger type
        /// </summary>
        [JsonPropertyName("trigger_type")]
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// ["<c>trigger_price</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("trigger_price")]
        public decimal TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>order_price</c>"] Order price
        /// </summary>
        [JsonPropertyName("order_price")]
        public decimal? OrderPrice { get; set; }
        /// <summary>
        /// ["<c>created_at</c>"] Creation time
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>order_price_type</c>"] Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        public OrderPriceType? OrderPriceType { get; set; }
        /// <summary>
        /// ["<c>relation_tpsl_order_id</c>"] Relation tpsl order id
        /// </summary>
        [JsonPropertyName("relation_tpsl_order_id")]
        public string? RelationTpslOrderId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public TpSlStatus Status { get; set; }
        /// <summary>
        /// ["<c>canceled_at</c>"] Cancelation time
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// ["<c>fail_code</c>"] Fail code
        /// </summary>
        [JsonPropertyName("fail_code")]
        public int? FailCode { get; set; }
        /// <summary>
        /// ["<c>fail_reason</c>"] Fail reason
        /// </summary>
        [JsonPropertyName("fail_reason")]
        public string? FailReason { get; set; }
        /// <summary>
        /// ["<c>triggered_price</c>"] Triggered price
        /// </summary>
        [JsonPropertyName("triggered_price")]
        public decimal? TriggeredPrice { get; set; }
        /// <summary>
        /// ["<c>relation_order_id</c>"] Relation order id
        /// </summary>
        [JsonPropertyName("relation_order_id")]
        public string? RelationOrderId { get; set; }
    }


}
