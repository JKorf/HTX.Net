using HTX.Net.Converters;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Trade update
    /// </summary>
    [SerializationModel]
    public record HTXTradeUpdate
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
        /// ["<c>tradePrice</c>"] Price of this trade
        /// </summary>
        [JsonPropertyName("tradePrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>tradeVolume</c>"] Volume of this trade
        /// </summary>
        [JsonPropertyName("tradeVolume")]
        public decimal Quantity { get; set; }
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
        /// ["<c>aggressor</c>"] Is the taker
        /// </summary>
        [JsonPropertyName("aggressor")]
        public bool IsTaker { get; set; }
        /// <summary>
        /// ["<c>tradeId</c>"] Trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>tradeTime</c>"] Time of trade
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("tradeTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>transactFee</c>"] Transaction fee
        /// </summary>
        [JsonPropertyName("transactFee")]
        public decimal TransactionFee { get; set; }

        /// <summary>
        /// ["<c>feeCurrency</c>"] Asset of the fee
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>feeDeduct</c>"] Fee deduction quantity
        /// </summary>
        [JsonPropertyName("feeDeduct")]
        public decimal FeeDeduct { get; set; }

        /// <summary>
        /// ["<c>feeDeductType</c>"] Fee deduction type
        /// </summary>
        [JsonPropertyName("feeDeductType")]
        public string FeeDeductType { get; set; } = string.Empty;
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
    }
}
