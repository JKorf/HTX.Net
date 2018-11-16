using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    public class HuobiMarketData
    {
        public long Id { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal Amount { get; set; }
        [JsonProperty("vol")]
        public decimal Volume { get; set; }
        [JsonProperty("count")]
        public int TradeCount { get; set; }
    }
}
