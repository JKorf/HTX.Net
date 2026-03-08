namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Take profit / stop loss order page
    /// </summary>
    [SerializationModel]
    public record HTXTpSlClosedOrderPage
    {
        /// <summary>
        /// ["<c>orders</c>"] Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public HTXTpSlClosedOrder[] Orders { get; set; } = Array.Empty<HTXTpSlClosedOrder>();
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
    public record HTXTpSlClosedOrder : HTXTpSlOrder
    {
        /// <summary>
        /// ["<c>canceled_at</c>"] Cancel time
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// ["<c>update_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>fail_code</c>"] Error code
        /// </summary>
        [JsonPropertyName("fail_code")]
        public int? FailCode { get; set; }
        /// <summary>
        /// ["<c>fail_reason</c>"] Error reason
        /// </summary>
        [JsonPropertyName("fail_reason")]
        public string? FailReason { get; set; }
        /// <summary>
        /// ["<c>triggered_price</c>"] Triggered price
        /// </summary>
        [JsonPropertyName("triggered_price")]
        public int? TriggeredPrice { get; set; }
        /// <summary>
        /// ["<c>relation_order_id</c>"] Relation order id
        /// </summary>
        [JsonPropertyName("relation_order_id")]
        public string? RelationOrderId { get; set; }
    }


}
