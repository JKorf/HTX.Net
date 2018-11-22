using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects
{
    public class HuobiOrder
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        public string Symbol { get; set; }
        [JsonProperty("account-id")]
        public long AccountId { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("created-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("canceled-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CanceledAt { get; set; }
        [JsonProperty("finished-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime FinishedAt { get; set; }

        [JsonProperty("order-type"), JsonConverter(typeof(OrderTypeConverter)), JsonOptionalProperty]
        public HuobiOrderType Type { get; set; }
        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter)), JsonOptionalProperty]
        private HuobiOrderType TypeField { set => Type = value; }


        [JsonProperty("order-source"), JsonOptionalProperty]
        public string Source { get; set; }
        [JsonProperty("source"), JsonOptionalProperty]
        private string SourceAlt { set => Source = value; }
        [JsonProperty("order-state"), JsonConverter(typeof(OrderStateConverter)), JsonOptionalProperty]
        public HuobiOrderState State { get; set; }
        [JsonProperty("state"), JsonConverter(typeof(OrderStateConverter)), JsonOptionalProperty]
        private HuobiOrderState StateField { set => State = value; }

        [JsonProperty("filled-amount"), JsonOptionalProperty]
        public decimal FilledAmount { get; set; }
        [JsonProperty("field-amount"), JsonOptionalProperty]
        private decimal FieldAmount { set => FilledAmount = value; }

        [JsonProperty("filled-cash-amount"), JsonOptionalProperty]
        public decimal FilledCashAmount { get; set; }
        [JsonProperty("field-cash-amount"), JsonOptionalProperty]
        private decimal FieldCashAmount { set => FilledCashAmount = value; }
        [JsonProperty("filled-fees"), JsonOptionalProperty]
        public decimal FilledFees { get; set; }
        [JsonProperty("field-fees"), JsonOptionalProperty]
        private decimal FieldFees { set => FilledFees = value; }

    }
}
