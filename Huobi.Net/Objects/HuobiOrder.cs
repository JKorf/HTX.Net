using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects
{
    public class HuobiOrder
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }
        /// <summary>
        /// The symbol of the order
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The id of the account that placed the order
        /// </summary>
        [JsonProperty("account-id")]
        public long AccountId { get; set; }
        /// <summary>
        /// The amount of the order
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonProperty("created-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The time the order created
        /// </summary>
        [JsonProperty("canceled-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CanceledAt { get; set; }
        /// <summary>
        /// The time the order was finished
        /// </summary>
        [JsonProperty("finished-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime FinishedAt { get; set; }

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonProperty("order-type"), JsonConverter(typeof(OrderTypeConverter)), JsonOptionalProperty]
        public HuobiOrderType Type { get; set; }
        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter)), JsonOptionalProperty]
        private HuobiOrderType TypeField { set => Type = value; }

        /// <summary>
        /// The source of the order
        /// </summary>

        [JsonProperty("order-source"), JsonOptionalProperty]
        public string Source { get; set; }
        [JsonProperty("source"), JsonOptionalProperty]
        private string SourceAlt { set => Source = value; }
        /// <summary>
        /// The state of the order
        /// </summary>
        [JsonProperty("order-state"), JsonConverter(typeof(OrderStateConverter)), JsonOptionalProperty]
        public HuobiOrderState State { get; set; }
        [JsonProperty("state"), JsonConverter(typeof(OrderStateConverter)), JsonOptionalProperty]
        private HuobiOrderState StateField { set => State = value; }

        /// <summary>
        /// The amount of the order that is filled
        /// </summary>
        [JsonProperty("filled-amount"), JsonOptionalProperty]
        public decimal FilledAmount { get; set; }
        [JsonProperty("field-amount"), JsonOptionalProperty]
        private decimal FieldAmount { set => FilledAmount = value; }

        [JsonProperty("filled-cash-amount"), JsonOptionalProperty]
        public decimal FilledCashAmount { get; set; }
        [JsonProperty("field-cash-amount"), JsonOptionalProperty]
        private decimal FieldCashAmount { set => FilledCashAmount = value; }

        /// <summary>
        /// The amount of fees paid for the filled amount
        /// </summary>
        [JsonProperty("filled-fees"), JsonOptionalProperty]
        public decimal FilledFees { get; set; }
        [JsonProperty("field-fees"), JsonOptionalProperty]
        private decimal FieldFees { set => FilledFees = value; }

    }
}
