using System;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Socket
{
    /// <summary>
    /// Cancelation details
    /// </summary>
    public class HuobiOrderCancelationUpdate
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
        /// Order side
        /// </summary>
        [JsonProperty("orderSide"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonProperty("orderType"), JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }
        /// <summary>
        /// Account id
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Order source
        /// </summary>
        [JsonProperty("source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// Order price
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonProperty("orderSize")]
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
        [JsonConverter(typeof(OrderStateConverter))]
        public OrderState OrderStatus { get; set; }

        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonProperty("remainAmt")]
        public decimal QuantityRemaining { get; set; }
    }
}
