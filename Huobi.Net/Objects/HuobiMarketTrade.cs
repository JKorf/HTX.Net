using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects
{
    public class HuobiMarketTrade
    {
        public long Id { get; set; }
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        public List<HuobiMarketTradeDetails> Data { get; set; }
    }

    public class HuobiMarketTradeDetails
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        [JsonProperty("direction"), JsonConverter(typeof(OrderSideConverter))]
        public HuobiOrderSide Side { get; set; }
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
