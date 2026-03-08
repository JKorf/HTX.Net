namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trailing order page
    /// </summary>
    [SerializationModel]
    public record HTXTrailingClosedOrderPage
    {
        /// <summary>
        /// ["<c>orders</c>"] Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public HTXTrailingClosedOrder[] Orders { get; set; } = Array.Empty<HTXTrailingClosedOrder>();
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
    /// Trailing order info
    /// </summary>
    [SerializationModel]
    public record HTXTrailingClosedOrder : HTXTrailingOrder
    {
        /// <summary>
        /// ["<c>triggered_price</c>"] Triggered price
        /// </summary>
        [JsonPropertyName("triggered_price")]
        public decimal? TriggeredPrice { get; set; }
        /// <summary>
        /// ["<c>update_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>canceled_at</c>"] Cancel time
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public DateTime? CancelTime { get; set; }
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
        /// ["<c>relation_order_id</c>"] Relation order id
        /// </summary>
        [JsonPropertyName("relation_order_id")]
        public string? RelationOrderId { get; set; }
        /// <summary>
        /// ["<c>market_limit_price</c>"] Lowest/highest market price (use the lowest price when buy. use the highest when sell)
        /// </summary>
        [JsonPropertyName("market_limit_price")]
        public decimal? MarketLimitPrice { get; set; }
        /// <summary>
        /// ["<c>real_volume</c>"] Real volume
        /// </summary>
        [JsonPropertyName("real_volume")]
        public decimal? RealVolume { get; set; }
        /// <summary>
        /// ["<c>formula_price</c>"] Formula price(the lowest (highest) market price* (1 ± callback rate))
        /// </summary>
        [JsonPropertyName("formula_price")]
        public decimal? FormulaPrice { get; set; }
    }


}
