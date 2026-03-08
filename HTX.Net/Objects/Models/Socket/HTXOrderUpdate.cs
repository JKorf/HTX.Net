using HTX.Net.Converters;
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order update
    /// </summary>
    [SerializationModel]
    public record HTXOrderUpdate
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
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverter(typeof(ClientIdConverter))]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>orderStatus</c>"] Order status
        /// </summary>
        [JsonPropertyName("orderStatus")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>lastActTime</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("lastActTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>totalTradeAmount</c>"] Total trade quantity
        /// </summary>
        [JsonPropertyName("totalTradeAmount")]
        public decimal? TotalTradeQuantity { get; set; }
    }
        
    /// <summary>
    /// Submitted order update
    /// </summary>
    [SerializationModel]
    public record HTXSubmittedOrderUpdate : HTXOrderUpdate
    {
        /// <summary>
        /// ["<c>accountId</c>"] Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>orderPrice</c>"] Price of the order
        /// </summary>
        [JsonPropertyName("orderPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>orderSize</c>"] Quantity of the order
        /// </summary>
        [JsonPropertyName("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>orderValue</c>"] Value of the order
        /// </summary>
        [JsonPropertyName("orderValue")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>type</c>"] The raw type string
        /// </summary>
        [JsonPropertyName("type")]
        public string RawType { get; set; } = string.Empty;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => EnumConverter.ParseString<OrderType>(RawType)!.Value;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => EnumConverter.ParseString<OrderSide>(RawType)!.Value;
        /// <summary>
        /// ["<c>orderCreateTime</c>"] Creation time
        /// </summary>
        [JsonPropertyName("orderCreateTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ["<c>orderSource</c>"] Order source
        /// </summary>
        [JsonPropertyName("orderSource")]
        public string OrderSource { get; set; } = string.Empty;
    }

    /// <summary>
    /// Matched order update
    /// </summary>
    [SerializationModel]
    public record HTXMatchedOrderUpdate : HTXOrderUpdate
    {
        /// <summary>
        /// ["<c>tradePrice</c>"] Trade price
        /// </summary>
        [JsonPropertyName("tradePrice")]
        public decimal TradePrice { get; set; }
        /// <summary>
        /// ["<c>tradeVolume</c>"] Trade volume
        /// </summary>
        [JsonPropertyName("tradeVolume")]
        public decimal TradeQuantity { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>type</c>"] The raw type string
        /// </summary>
        [JsonPropertyName("type")]
        public string RawType { get; set; } = string.Empty;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => EnumConverter.ParseString<OrderType>(RawType)!.Value;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => EnumConverter.ParseString<OrderSide>(RawType)!.Value;
        /// <summary>
        /// ["<c>tradeId</c>"] Trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public long TradeId { get; set; }
        /// <summary>
        /// ["<c>tradeTime</c>"] Timestamp of trade
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("tradeTime")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// ["<c>aggressor</c>"] Is the taker
        /// </summary>
        [JsonPropertyName("aggressor")]
        public bool IsTaker { get; set; }
        /// <summary>
        /// ["<c>remainAmt</c>"] Remaining quantity
        /// </summary>
        [JsonPropertyName("remainAmt")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>execAmt</c>"] Executed quantity
        /// </summary>
        [JsonPropertyName("execAmt")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>orderPrice</c>"] Price of the order
        /// </summary>
        [JsonPropertyName("orderPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>orderSize</c>"] Quantity of the order
        /// </summary>
        [JsonPropertyName("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>orderSource</c>"] Order source
        /// </summary>
        [JsonPropertyName("orderSource")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderValue</c>"] Value of the order
        /// </summary>
        [JsonPropertyName("orderValue")]
        public decimal? QuoteQuantity { get; set; }
    }

    /// <summary>
    /// Canceled order update
    /// </summary>
    [SerializationModel]
    public record HTXCanceledOrderUpdate : HTXOrderUpdate
    {
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }

        /// <summary>
        /// ["<c>type</c>"] The raw type string
        /// </summary>
        [JsonPropertyName("type")]
        public string RawType { get; set; } = string.Empty;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => EnumConverter.ParseString<OrderType>(RawType)!.Value;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => EnumConverter.ParseString<OrderSide>(RawType)!.Value;
        /// <summary>
        /// ["<c>remainAmt</c>"] Remaining quantity
        /// </summary>
        [JsonPropertyName("remainAmt")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>execAmt</c>"] Executed quantity
        /// </summary>
        [JsonPropertyName("execAmt")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>orderPrice</c>"] Price of the order
        /// </summary>
        [JsonPropertyName("orderPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>orderSize</c>"] Quantity of the order
        /// </summary>
        [JsonPropertyName("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>orderValue</c>"] Value of the order
        /// </summary>
        [JsonPropertyName("orderValue")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>orderSource</c>"] Order source
        /// </summary>
        [JsonPropertyName("orderSource")]
        public string OrderSource { get; set; } = string.Empty;
    }

    /// <summary>
    /// Info on a failed trigger for a conditional order
    /// </summary>
    [SerializationModel]
    public record HTXTriggerFailureOrderUpdate : HTXOrderUpdate
    {
        /// <summary>
        /// ["<c>orderSide</c>"] Side of the order
        /// </summary>
        [JsonPropertyName("orderSide")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>errCode</c>"] The error code
        /// </summary>
        [JsonPropertyName("errCode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// ["<c>errMessage</c>"] The error message
        /// </summary>
        [JsonPropertyName("errMessage")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
