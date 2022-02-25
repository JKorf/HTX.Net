using System;
using System.Globalization;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    public class HuobiOrder
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// The order id as specified by the client
        /// </summary>
        [JsonProperty("client-order-id")]
        public string ClientOrderId { get; set; } = string.Empty;

        /// <summary>
        /// The symbol of the order
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The id of the account that placed the order
        /// </summary>
        [JsonProperty("account-id")]
        public long AccountId { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        
        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonProperty("created-at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// The time the order was canceled
        /// </summary>
        [JsonProperty("canceled-at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CancelTime { get; set; }
        /// <summary>
        /// The time the order was finished
        /// </summary>
        [JsonProperty("finished-at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CompleteTime { get; set; }

        [JsonProperty("type")]
        internal string TypeInternal { get; set; } = string.Empty;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => new OrderTypeConverter(false).ReadString(TypeInternal);

        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => new OrderSideConverter(false).ReadString(TypeInternal);

        /// <summary>
        /// The source of the order
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// The state of the order
        /// </summary>
        [JsonProperty("state"), JsonConverter(typeof(OrderStateConverter))]
        public OrderState State { get; set; }

        /// <summary>
        /// The quantity of the order that is filled
        /// </summary>
        [JsonProperty("field-amount")]
        public decimal QuantityFilled { get; set; }

        /// <summary>
        /// Filled cash quantity
        /// </summary>
        [JsonProperty("field-cash-amount")]
        public decimal QuoteQuantityFilled { get; set; }

        /// <summary>
        /// The quantity of fees paid for the filled quantity
        /// </summary>
        [JsonProperty("field-fees")]
        public decimal Fee { get; set; }
    }
}
