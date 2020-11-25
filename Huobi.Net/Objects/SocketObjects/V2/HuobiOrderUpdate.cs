using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.SocketObjects.V2
{
    /// <summary>
    /// Order update
    /// </summary>
    public class HuobiOrderUpdate
    {
        /// <summary>
        /// Event type
        /// </summary>
        public string EventType { get; set; } = "";

        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// Client order id
        /// </summary>
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonProperty("orderStatus"), JsonConverter(typeof(OrderStateConverter))]
        public HuobiOrderState Status { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        [JsonOptionalProperty]
        public DateTime? LastActTime { get; set; }
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
        /// Size of the order
        /// </summary>
        [JsonProperty("orderSize")]
        public decimal? Size { get; set; }
        /// <summary>
        /// Value of the order
        /// </summary>
        [JsonProperty("orderValue")]
        [JsonOptionalProperty]
        public decimal? Value { get; set; }
        /// <summary>
        /// Type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public HuobiOrderType Type { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("orderCreateTime")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Order source
        /// </summary>
        public string OrderSource { get; set; } = "";
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
        public decimal TradeVolume { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public HuobiOrderType Type { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        public long TradeId { get; set; }
        /// <summary>
        /// Timestamp of trade
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Is the taker
        /// </summary>
        [JsonProperty("aggressor")]
        public bool IsTaker { get; set; }
        /// <summary>
        /// Remaining amount
        /// </summary>
        [JsonProperty("remainAmt")]
        public decimal RemainingAmount { get; set; }
        /// <summary>
        /// Executed amount
        /// </summary>
        [JsonProperty("execAmt")]
        public decimal ExecutedAmount { get; set; }
        /// <summary>
        /// Price of the order
        /// </summary>
        [JsonProperty("orderPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// Size of the order
        /// </summary>
        [JsonProperty("orderSize")]
        public decimal? Size { get; set; }
        /// <summary>
        /// Order source
        /// </summary>
        public string OrderSource { get; set; } = "";
    }

    /// <summary>
    /// Cancelled order update
    /// </summary>
    public class HuobiCancelledOrderUpdate : HuobiOrderUpdate
    {
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public HuobiOrderType Type { get; set; }
        /// <summary>
        /// Remaining amount
        /// </summary>
        [JsonProperty("remainAmt")]
        public decimal RemainingAmount { get; set; }
        /// <summary>
        /// Executed amount
        /// </summary>
        [JsonProperty("execAmt")]
        public decimal ExecutedAmount { get; set; }
        /// <summary>
        /// Price of the order
        /// </summary>
        [JsonProperty("orderPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// Size of the order
        /// </summary>
        [JsonProperty("orderSize")]
        public decimal? Size { get; set; }
        /// <summary>
        /// Order source
        /// </summary>
        public string OrderSource { get; set; } = "";
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
        public HuobiOrderSide Side { get; set; }
        /// <summary>
        /// The error code
        /// </summary>
        [JsonProperty("errCode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        [JsonProperty("errMessage")]
        public string ErrorMessage { get; set; } = "";
    }
}
