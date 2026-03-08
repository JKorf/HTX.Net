namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trading status
    /// </summary>
    [SerializationModel]
    public record HTXTradingStatus
    {
        /// <summary>
        /// ["<c>is_disable</c>"] Is disable
        /// </summary>
        [JsonPropertyName("is_disable")]
        public bool IsDisable { get; set; }
        /// <summary>
        /// ["<c>order_price_types</c>"] Order price types
        /// </summary>
        [JsonPropertyName("order_price_types")]
        public string OrderPriceTypes { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>disable_reason</c>"] Disable reason
        /// </summary>
        [JsonPropertyName("disable_reason")]
        public string DisableReason { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>disable_interval</c>"] Disable interval
        /// </summary>
        [JsonPropertyName("disable_interval")]
        public long DisableInterval { get; set; }
        /// <summary>
        /// ["<c>recovery_time</c>"] Recovery time
        /// </summary>
        [JsonPropertyName("recovery_time")]
        public long RecoveryTime { get; set; }
        /// <summary>
        /// ["<c>COR</c>"] Cancel order ratio info
        /// </summary>
        [JsonPropertyName("COR")]
        public HTXTradingStatusCor CancelOrderRatio { get; set; } = null!;
        /// <summary>
        /// ["<c>TDN</c>"] Total disables info
        /// </summary>
        [JsonPropertyName("TDN")]
        public HTXTradingStatusTdn TotalDisables { get; set; } = null!;
    }

    /// <summary>
    /// Cancel order ratio
    /// </summary>
    [SerializationModel]
    public record HTXTradingStatusCor
    {
        /// <summary>
        /// ["<c>orders_threshold</c>"] Orders threshold
        /// </summary>
        [JsonPropertyName("orders_threshold")]
        public int OrdersThreshold { get; set; }
        /// <summary>
        /// ["<c>orders</c>"] Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public int Orders { get; set; }
        /// <summary>
        /// ["<c>invalid_cancel_orders</c>"] Invalid cancel orders
        /// </summary>
        [JsonPropertyName("invalid_cancel_orders")]
        public int InvalidCancelOrders { get; set; }
        /// <summary>
        /// ["<c>cancel_ratio_threshold</c>"] Cancel ratio threshold
        /// </summary>
        [JsonPropertyName("cancel_ratio_threshold")]
        public decimal CancelRatioThreshold { get; set; }
        /// <summary>
        /// ["<c>cancel_ratio</c>"] Cancel ratio
        /// </summary>
        [JsonPropertyName("cancel_ratio")]
        public decimal CancelRatio { get; set; }
        /// <summary>
        /// ["<c>is_trigger</c>"] Is trigger
        /// </summary>
        [JsonPropertyName("is_trigger")]
        public bool IsTrigger { get; set; }
        /// <summary>
        /// ["<c>is_active</c>"] Is active
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Total disables
    /// </summary>
    [SerializationModel]
    public record HTXTradingStatusTdn
    {
        /// <summary>
        /// ["<c>disables_threshold</c>"] Disables threshold
        /// </summary>
        [JsonPropertyName("disables_threshold")]
        public int DisablesThreshold { get; set; }
        /// <summary>
        /// ["<c>disables</c>"] Disables
        /// </summary>
        [JsonPropertyName("disables")]
        public int Disables { get; set; }
        /// <summary>
        /// ["<c>is_trigger</c>"] Is trigger
        /// </summary>
        [JsonPropertyName("is_trigger")]
        public bool IsTrigger { get; set; }
        /// <summary>
        /// ["<c>is_active</c>"] Is active
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }


}
