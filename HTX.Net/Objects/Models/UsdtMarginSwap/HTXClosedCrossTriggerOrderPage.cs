namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trigger order page
    /// </summary>
    [SerializationModel]
    public record HTXClosedCrossTriggerOrderPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public HTXClosedCrossTriggerOrder[] Orders { get; set; } = Array.Empty<HTXClosedCrossTriggerOrder>();
        /// <summary>
        /// Total page
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPage { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total results
        /// </summary>
        [JsonPropertyName("total_size")]
        public int Total { get; set; }
    }

    /// <summary>
    /// Trigger order
    /// </summary>
    [SerializationModel]
    public record HTXClosedCrossTriggerOrder : HTXCrossTriggerOrder
    {
        /// <summary>
        /// Cancel time
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// Trigger time
        /// </summary>
        [JsonPropertyName("triggered_at")]
        public DateTime? TriggerTime { get; set; }
        /// <summary>
        /// Order insert time
        /// </summary>
        [JsonPropertyName("order_insert_at")]
        public DateTime? OrderInsertTime { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Error code
        /// </summary>
        [JsonPropertyName("fail_code")]
        public int? FailCode { get; set; }
        /// <summary>
        /// Error reason
        /// </summary>
        [JsonPropertyName("fail_reason")]
        public string? FailReason { get; set; }
        /// <summary>
        /// Triggered price
        /// </summary>
        [JsonPropertyName("triggered_price")]
        public int? TriggeredPrice { get; set; }
        /// <summary>
        /// Relation order id
        /// </summary>
        [JsonPropertyName("relation_order_id")]
        public string? RelationOrderId { get; set; }
    }


}
