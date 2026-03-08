using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Take profit / stop loss order page
    /// </summary>
    [SerializationModel]
    public record HTXTpSlOrderPage
    {
        /// <summary>
        /// ["<c>orders</c>"] Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public HTXTpSlOrder[] Orders { get; set; } = Array.Empty<HTXTpSlOrder>();
        /// <summary>
        /// ["<c>total_page</c>"] Total page
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPage { get; set; }
        /// <summary>
        /// ["<c>current_page</c>"] Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>total_size</c>"] Total results
        /// </summary>
        [JsonPropertyName("total_size")]
        public int Total { get; set; }
    }

    /// <summary>
    /// Tp/Sl order
    /// </summary>
    [SerializationModel]
    public record HTXTpSlOrder
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
        /// ["<c>order_type</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public int OrderType { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide OrderSide { get; set; }
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
        /// ["<c>order_source</c>"] Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
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
        public OrderPriceType OrderPriceType { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public TpSlStatus? Status { get; set; }
        /// <summary>
        /// ["<c>tpsl_order_type</c>"] Tpsl order type
        /// </summary>
        [JsonPropertyName("tpsl_order_type")]
        public TpslOrderType? TpslOrderType { get; set; }
        /// <summary>
        /// ["<c>source_order_id</c>"] Source order id
        /// </summary>
        [JsonPropertyName("source_order_id")]
        public string? SourceOrderId { get; set; }
        /// <summary>
        /// ["<c>relation_tpsl_order_id</c>"] Relation tpsl order id
        /// </summary>
        [JsonPropertyName("relation_tpsl_order_id")]
        public string? RelationTpslOrderId { get; set; }
    }


}
