using System;
using CryptoExchange.Net.Converters;

using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order update
    /// </summary>
    public record HTXOrderUpdate
    {
        /// <summary>
        /// Event type
        /// </summary>
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = string.Empty;

        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("orderStatus"), JsonConverter(typeof(EnumConverter))]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("lastActTime")]
        public DateTime? UpdateTime { get; set; }
    }
        
    /// <summary>
    /// Submitted order update
    /// </summary>
    public record HTXSubmittedOrderUpdate : HTXOrderUpdate
    {
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Price of the order
        /// </summary>
        [JsonPropertyName("orderPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the order
        /// </summary>
        [JsonPropertyName("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Value of the order
        /// </summary>
        [JsonPropertyName("orderValue")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// The raw type string
        /// </summary>
        [JsonPropertyName("type")]
        public string RawType { get; set; } = string.Empty;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => EnumConverter.ParseString<OrderType>(RawType);

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => EnumConverter.ParseString<OrderSide>(RawType);
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("orderCreateTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Order source
        /// </summary>
        [JsonPropertyName("orderSource")]
        public string OrderSource { get; set; } = string.Empty;
    }

    /// <summary>
    /// Matched order update
    /// </summary>
    public record HTXMatchedOrderUpdate : HTXOrderUpdate
    {
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("tradePrice")]
        public decimal TradePrice { get; set; }
        /// <summary>
        /// Trade volume
        /// </summary>
        [JsonPropertyName("tradeVolume")]
        public decimal TradeQuantity { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// The raw type string
        /// </summary>
        [JsonPropertyName("type")]
        public string RawType { get; set; } = string.Empty;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => EnumConverter.ParseString<OrderType>(RawType);

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => EnumConverter.ParseString<OrderSide>(RawType);
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public long TradeId { get; set; }
        /// <summary>
        /// Timestamp of trade
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("tradeTime")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Is the taker
        /// </summary>
        [JsonPropertyName("aggressor")]
        public bool IsTaker { get; set; }
        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonPropertyName("remainAmt")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Executed quantity
        /// </summary>
        [JsonPropertyName("execAmt")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Price of the order
        /// </summary>
        [JsonPropertyName("orderPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the order
        /// </summary>
        [JsonPropertyName("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Order source
        /// </summary>
        [JsonPropertyName("orderSource")]
        public string OrderSource { get; set; } = string.Empty;
    }

    /// <summary>
    /// Canceled order update
    /// </summary>
    public record HTXCanceledOrderUpdate : HTXOrderUpdate
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }

        /// <summary>
        /// The raw type string
        /// </summary>
        [JsonPropertyName("type")]
        public string RawType { get; set; } = string.Empty;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => EnumConverter.ParseString<OrderType>(RawType);

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => EnumConverter.ParseString<OrderSide>(RawType);
        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonPropertyName("remainAmt")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Executed quantity
        /// </summary>
        [JsonPropertyName("execAmt")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Price of the order
        /// </summary>
        [JsonPropertyName("orderPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the order
        /// </summary>
        [JsonPropertyName("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Order source
        /// </summary>
        [JsonPropertyName("orderSource")]
        public string OrderSource { get; set; } = string.Empty;
    }

    /// <summary>
    /// Info on a failed trigger for a conditional order
    /// </summary>
    public record HTXTriggerFailureOrderUpdate : HTXOrderUpdate
    {
        /// <summary>
        /// Side of the order
        /// </summary>
        [JsonPropertyName("orderSide"), JsonConverter(typeof(EnumConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The error code
        /// </summary>
        [JsonPropertyName("errCode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        [JsonPropertyName("errMessage")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
