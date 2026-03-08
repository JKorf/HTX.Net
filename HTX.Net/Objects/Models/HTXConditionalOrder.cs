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
        /// ["<c>accountId</c>"] Acount id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// ["<c>source</c>"] Source
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string? OrderId { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverter(typeof(ClientIdConverter))]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderPrice</c>"] Price
        /// </summary>
        [JsonPropertyName("orderPrice")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>orderSize</c>"] Quantity
        /// </summary>
        [JsonPropertyName("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>orderValue</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("orderValue")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>orderSide</c>"] Side
        /// </summary>
        [JsonPropertyName("orderSide")]

        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>

        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>orderType</c>"] Type
        /// </summary>

        [JsonPropertyName("orderType")]
        public ConditionalOrderType Type { get; set; }
        /// <summary>
        /// ["<c>stopPrice</c>"] Stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// ["<c>trailingRate</c>"] Trailing rate
        /// </summary>
        [JsonPropertyName("trailingRate")]
        public decimal? TrailingRate { get; set; }
        /// <summary>
        /// ["<c>orderOrigTime</c>"] Creation time
        /// </summary>
        [JsonPropertyName("orderOrigTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>lastActTime</c>"] Last update time
        /// </summary>
        [JsonPropertyName("lastActTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>orderStatus</c>"] Status
        /// </summary>

        [JsonPropertyName("orderStatus")]
        public ConditionalOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>errCode</c>"] Error code if the conditional order is rejected
        /// </summary>
        [JsonPropertyName("errCode")]
        public int? ErrorCode { get; set; }
        /// <summary>
        /// ["<c>errMessage</c>"] Error message if conditional order is rejected
        /// </summary>
        [JsonPropertyName("errMessage")]
        public string? ErrorMessage { get; set; }
    }
}
