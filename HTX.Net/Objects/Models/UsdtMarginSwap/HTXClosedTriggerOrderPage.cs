namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trigger order page
    /// </summary>
    [SerializationModel]
    public record HTXClosedTriggerOrderPage
    {
        /// <summary>
        /// ["<c>orders</c>"] Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public HTXClosedTriggerOrder[] Orders { get; set; } = Array.Empty<HTXClosedTriggerOrder>();
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
    /// Trigger order
    /// </summary>
    [SerializationModel]
    public record HTXClosedTriggerOrder : HTXTriggerOrder
    {
        /// <summary>
        /// ["<c>canceled_at</c>"] Cancel time
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// ["<c>triggered_at</c>"] Trigger time
        /// </summary>
        [JsonPropertyName("triggered_at")]
        public DateTime? TriggerTime { get; set; }
        /// <summary>
        /// ["<c>order_insert_at</c>"] Order insert time
        /// </summary>
        [JsonPropertyName("order_insert_at")]
        public DateTime? OrderInsertTime { get; set; }
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
