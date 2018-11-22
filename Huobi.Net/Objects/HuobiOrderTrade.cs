using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects
{
    public class HuobiOrderTrade
    {
        [JsonProperty("created-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("filled-amount")]
        public decimal FilledAmount { get; set; }
        [JsonProperty("filled-fees")]
        public decimal FilledFees { get; set; }
        public long Id { get; set; }
        [JsonProperty("match-id")]
        public long MatchId { get; set; }
        [JsonProperty("order-id")]
        public long OrderId { get; set; }
        public decimal Price { get; set; }
        public string Source { get; set; }
        public string Symbol { get; set; }
        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter))]
        public HuobiOrderType OrderType { get; set; }
    }
}
