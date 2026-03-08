using HTX.Net.Enums;
using HTX.Net.Objects.Sockets;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Trigger order update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapIsolatedTriggerOrderUpdate : HTXOpMessage
    {
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// ["<c>event</c>"] Event
        /// </summary>
        [JsonPropertyName("event")]
        public EventOrderTrigger EventOrderTrigger { get; set; }
        /// <summary>
        /// ["<c>uid</c>"] User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapIsolatedTriggerOrderUpdateData[] Data { get; set; } = Array.Empty<HTXUsdtMarginSwapIsolatedTriggerOrderUpdateData>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapIsolatedTriggerOrderUpdateData
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
        /// ["<c>trigger_type</c>"] Trigger type
        /// </summary>
        [JsonPropertyName("trigger_type")]
        public TriggerType? TriggerType { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Quantity
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>order_type</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public int? OrderType { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide OrderSide { get; set; }
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
        /// ["<c>relation_order_id</c>"] Relation order id
        /// </summary>
        [JsonPropertyName("relation_order_id")]
        public string RelationOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_price_type</c>"] Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        public OrderPriceType? OrderPriceType { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatusFilter? OrderStatus { get; set; }
        /// <summary>
        /// ["<c>order_source</c>"] Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>trigger_price</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("trigger_price")]
        public decimal TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>triggered_price</c>"] Triggered price
        /// </summary>
        [JsonPropertyName("triggered_price")]
        public decimal? TriggeredPrice { get; set; }
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
        /// ["<c>triggered_at</c>"] Triggered time
        /// </summary>
        [JsonPropertyName("triggered_at")]
        public DateTime? TriggerTime { get; set; }
        /// <summary>
        /// ["<c>order_insert_at</c>"] Order insert time
        /// </summary>
        [JsonPropertyName("order_insert_at")]
        public DateTime? OrderInsertTime { get; set; }
        /// <summary>
        /// ["<c>canceled_at</c>"] Cancel time
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public DateTime? CancelTime { get; set; }
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
        /// ["<c>fail_code</c>"] Fail code
        /// </summary>
        [JsonPropertyName("fail_code")]
        public string? FailCode { get; set; }
        /// <summary>
        /// ["<c>fail_reason</c>"] Fail reason
        /// </summary>
        [JsonPropertyName("fail_reason")]
        public string? FailReason { get; set; }
        /// <summary>
        /// ["<c>reduce_only</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
    }


}
