using System;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Order update
    /// </summary>
    public class HuobiOrderUpdate
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("order-id")]
        public long Id { get; set; }

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
        [JsonProperty("order-amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonProperty("order-price")]
        public decimal Price { get; set; }

        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonProperty("created-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CreateTime { get; set; }
        
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonProperty("order-type"), JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }


        /// <summary>
        /// The source of the order
        /// </summary>
        [JsonProperty("order-source"), JsonOptionalProperty]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// The state of the order
        /// </summary>
        [JsonProperty("order-state"), JsonConverter(typeof(OrderStateConverter))]
        public OrderState State { get; set; }

        /// <summary>
        /// The role of the order
        /// </summary>
        [JsonProperty("role"), JsonConverter(typeof(OrderRoleConverter))]
        public OrderRole Role { get; set; }

        /// <summary>
        /// The quantity of the order that is filled
        /// </summary>
        [JsonProperty("filled-amount"), JsonOptionalProperty]
        public decimal QuantityFilled { get; set; }

        /// <summary>
        /// Unfilled quantity
        /// </summary>
        [JsonProperty("unfilled-amount"), JsonOptionalProperty]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Filled cash quantity
        /// </summary>
        [JsonProperty("filled-cash-amount"), JsonOptionalProperty]
        public decimal QuoteQuantityFilled { get; set; }

        /// <summary>
        /// The quantity of fees paid for the filled quantity
        /// </summary>
        [JsonProperty("filled-fees"), JsonOptionalProperty]
        public decimal Fee { get; set; }
    }
}
