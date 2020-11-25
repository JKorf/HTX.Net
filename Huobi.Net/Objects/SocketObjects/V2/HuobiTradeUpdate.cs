using System;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.SocketObjects.V2
{
    /// <summary>
    /// Trade update
    /// </summary>
    public class HuobiTradeUpdate
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
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Price of this trade
        /// </summary>
        public decimal TradePrice { get; set; }
        /// <summary>
        /// Volume of this trade
        /// </summary>
        public decimal TradeVolume { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonProperty("orderSide"), JsonConverter(typeof(OrderSideConverter))]
        public HuobiOrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonProperty("orderType"), JsonConverter(typeof(OrderTypeConverter))]
        public HuobiOrderType Type { get; set; }
        /// <summary>
        /// Is the taker
        /// </summary>
        [JsonProperty("aggressor")]
        public bool IsTaker { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        public long TradeId { get; set; }
        /// <summary>
        /// Time of trade
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Transaction fee
        /// </summary>
        [JsonProperty("TransactFee")]
        public decimal TransactionFee { get; set; }

        /// <summary>
        /// Currency of the fee
        /// </summary>
        public string FeeCurrency { get; set; } = "";
        /// <summary>
        /// Fee deduction amount
        /// </summary>
        public decimal FeeDeduct { get; set; }

        /// <summary>
        /// Fee deduction type
        /// </summary>
        public string FeeDeductType { get; set; } = "";
        /// <summary>
        /// Account id
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Order source
        /// </summary>
        [JsonProperty("source")]
        public string OrderSource { get; set; } = "";
        /// <summary>
        /// Order price
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// Order size
        /// </summary>
        public decimal OrderSize { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonOptionalProperty]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonOptionalProperty]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Operator
        /// </summary>
        [JsonOptionalProperty]
        public string? Operator { get; set; }
        /// <summary>
        /// Order creation time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime OrderCreateTime { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(OrderStateConverter))]
        public HuobiOrderState OrderStatus { get; set; }
    }
}
