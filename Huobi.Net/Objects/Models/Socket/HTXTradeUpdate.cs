using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Trade update
    /// </summary>
    public record HTXTradeUpdate
    {
        /// <summary>
        /// Event type
        /// </summary>
        public string EventType { get; set; } = string.Empty;

        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Price of this trade
        /// </summary>
        [JsonPropertyName("tradePrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// Volume of this trade
        /// </summary>
        [JsonPropertyName("tradeVolume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("orderSide"), JsonConverter(typeof(EnumConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("orderType"), JsonConverter(typeof(EnumConverter))]
        public OrderType Type { get; set; }
        /// <summary>
        /// Is the taker
        /// </summary>
        [JsonPropertyName("aggressor")]
        public bool IsTaker { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public long Id { get; set; }
        /// <summary>
        /// Time of trade
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("tradeTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Transaction fee
        /// </summary>
        [JsonPropertyName("transactFee")]
        public decimal TransactionFee { get; set; }

        /// <summary>
        /// Asset of the fee
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Fee deduction quantity
        /// </summary>
        public decimal FeeDeduct { get; set; }

        /// <summary>
        /// Fee deduction type
        /// </summary>
        public string FeeDeductType { get; set; } = string.Empty;
        /// <summary>
        /// Account id
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Order source
        /// </summary>
        [JsonPropertyName("source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// Order price
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("orderSize")]
        public decimal OrderQuantity { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Operator
        /// </summary>
        public string? Operator { get; set; }
        /// <summary>
        /// Order creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OrderCreateTime { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public OrderState OrderStatus { get; set; }
    }
}
