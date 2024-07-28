using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Conditional order info
    /// </summary>
    public record HTXConditionalOrder
    {
        /// <summary>
        /// Acount id
        /// </summary>
        public long AccountId { get; set; }
        /// <summary>
        /// Source
        /// </summary>
        public string Source { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        public string? OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("orderPrice")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonPropertyName("orderValue")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("orderSide")]
        [JsonConverter(typeof(EnumConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("orderType")]
        public ConditionalOrderType Type { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        public decimal StopPrice { get; set; }
        /// <summary>
        /// Trailing rate
        /// </summary>
        public decimal? TrailingRate { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("orderOrigTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonPropertyName("lastActTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("orderStatus")]
        public ConditionalOrderStatus Status { get; set; }
        /// <summary>
        /// Error code if the conditional order is rejected
        /// </summary>
        [JsonPropertyName("errCode")]
        public int? ErrorCode { get; set; }
        /// <summary>
        /// Error message if conditional order is rejected
        /// </summary>
        [JsonPropertyName("errMessage")]
        public string? ErrorMessage { get; set; }
    }
}
