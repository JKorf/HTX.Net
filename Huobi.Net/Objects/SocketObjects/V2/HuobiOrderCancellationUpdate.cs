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
    /// Cancellation details
    /// </summary>
    public class HuobiOrderCancellationUpdate
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

        /// <summary>
        /// Remaining amount
        /// </summary>
        [JsonProperty("remainAmt")]
        public decimal RemainingAmount { get; set; }
    }
}
