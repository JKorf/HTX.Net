using HTX.Net.Converters;
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Cancelation details
    /// </summary>
    [SerializationModel]
    public record HTXOrderCancelationUpdate
    {
        /// <summary>
        /// ["<c>eventType</c>"] Event type
        /// </summary>
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>orderSide</c>"] Order side
        /// </summary>
        [JsonPropertyName("orderSide")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>orderType</c>"] Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType Type { get; set; }
        /// <summary>
        /// ["<c>accountId</c>"] Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }

        /// <summary>
        /// ["<c>source</c>"] Order source
        /// </summary>
        [JsonPropertyName("source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderPrice</c>"] Order price
        /// </summary>
        [JsonPropertyName("orderPrice")]
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// ["<c>orderSize</c>"] Order quantity
        /// </summary>
        [JsonPropertyName("orderSize")]
        public decimal OrderQuantity { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverter(typeof(ClientIdConverter))]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>stopPrice</c>"] Stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// ["<c>operator</c>"] Operator
        /// </summary>
        [JsonPropertyName("operator")]
        public string? Operator { get; set; }
        /// <summary>
        /// ["<c>orderCreateTime</c>"] Order creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("orderCreateTime")]
        public DateTime OrderCreateTime { get; set; }
        /// <summary>
        /// ["<c>orderStatus</c>"] Order status
        /// </summary>

        [JsonPropertyName("orderStatus")]
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// ["<c>remainAmt</c>"] Remaining quantity
        /// </summary>
        [JsonPropertyName("remainAmt")]
        public decimal QuantityRemaining { get; set; }
    }
}
