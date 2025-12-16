using HTX.Net.Converters;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Conditional order info
    /// </summary>
    [SerializationModel]
    public record HTXConditionalOrder
    {
        /// <summary>
        /// Acount id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// Source
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string? OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverter(typeof(ClientIdConverter))]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
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

        public OrderSide Side { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>

        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Type
        /// </summary>

        [JsonPropertyName("orderType")]
        public ConditionalOrderType Type { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// Trailing rate
        /// </summary>
        [JsonPropertyName("trailingRate")]
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
