using System;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Socket
{
    /// <summary>
    /// Trade update
    /// </summary>
    public class HuobiTradeUpdate
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
        [JsonProperty("tradePrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// Volume of this trade
        /// </summary>
        [JsonProperty("tradeVolume")]
        public decimal Quantity { get; set; }
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
        /// Is the taker
        /// </summary>
        [JsonProperty("aggressor")]
        public bool IsTaker { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonProperty("tradeId")]
        public long Id { get; set; }
        /// <summary>
        /// Time of trade
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("tradeTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Transaction fee
        /// </summary>
        [JsonProperty("transactFee")]
        public decimal TransactionFee { get; set; }

        /// <summary>
        /// Asset of the fee
        /// </summary>
        [JsonProperty("feeCurrency")]
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
    }
}
