using System;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order update
    /// </summary>
    public class HuobiOrderUpdate
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
        /// Client order id
        /// </summary>
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonProperty("orderStatus"), JsonConverter(typeof(OrderStateConverter))]
        public OrderState Status { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("lastActTime")]
        public DateTime? UpdateTime { get; set; }
    }
        
    /// <summary>
    /// Submitted order update
    /// </summary>
    public class HuobiSubmittedOrderUpdate : HuobiOrderUpdate
    {
        /// <summary>
        /// Account id
        /// </summary>
        public long AccountId { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Price of the order
        /// </summary>
        [JsonProperty("orderPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the order
        /// </summary>
        [JsonProperty("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Value of the order
        /// </summary>
        [JsonProperty("orderValue")]
        public decimal? QuoteQuantity { get; set; }
        [JsonProperty("type")]
        internal string TypeInternal { get; set; } = string.Empty;
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => new OrderTypeConverter(false).ReadString(TypeInternal);

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => new OrderSideConverter(false).ReadString(TypeInternal);
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("orderCreateTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Order source
        /// </summary>
        public string OrderSource { get; set; } = string.Empty;
    }

    /// <summary>
    /// Matched order update
    /// </summary>
    public class HuobiMatchedOrderUpdate : HuobiOrderUpdate
    {
        /// <summary>
        /// Trade price
        /// </summary>
        public decimal TradePrice { get; set; }
        /// <summary>
        /// Trade volume
        /// </summary>
        [JsonProperty("tradeVolume")]
        public decimal TradeQuantity { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        [JsonProperty("type")]
        internal string TypeInternal { get; set; } = string.Empty;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => new OrderTypeConverter(false).ReadString(TypeInternal);

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => new OrderSideConverter(false).ReadString(TypeInternal);
        /// <summary>
        /// Trade id
        /// </summary>
        public long TradeId { get; set; }
        /// <summary>
        /// Timestamp of trade
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Is the taker
        /// </summary>
        [JsonProperty("aggressor")]
        public bool IsTaker { get; set; }
        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonProperty("remainAmt")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Executed quantity
        /// </summary>
        [JsonProperty("execAmt")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Price of the order
        /// </summary>
        [JsonProperty("orderPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the order
        /// </summary>
        [JsonProperty("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Order source
        /// </summary>
        public string OrderSource { get; set; } = string.Empty;
    }

    /// <summary>
    /// Canceled order update
    /// </summary>
    public class HuobiCanceledOrderUpdate : HuobiOrderUpdate
    {
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        [JsonProperty("type")]
        internal string TypeInternal { get; set; } = string.Empty;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => new OrderTypeConverter(false).ReadString(TypeInternal);

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => new OrderSideConverter(false).ReadString(TypeInternal);
        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonProperty("remainAmt")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Executed quantity
        /// </summary>
        [JsonProperty("execAmt")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Price of the order
        /// </summary>
        [JsonProperty("orderPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the order
        /// </summary>
        [JsonProperty("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Order source
        /// </summary>
        public string OrderSource { get; set; } = string.Empty;
    }

    /// <summary>
    /// Info on a failed trigger for a conditional order
    /// </summary>
    public class HuobiTriggerFailureOrderUpdate : HuobiOrderUpdate
    {
        /// <summary>
        /// Side of the order
        /// </summary>
        [JsonProperty("orderSide"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The error code
        /// </summary>
        [JsonProperty("errCode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        [JsonProperty("errMessage")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
