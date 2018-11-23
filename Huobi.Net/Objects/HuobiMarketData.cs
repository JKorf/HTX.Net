using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    public class HuobiMarketData
    {
        public long Id { get; set; }
        /// <summary>
        /// The highest price
        /// </summary>
        public decimal High { get; set; }
        /// <summary>
        /// The lowest price
        /// </summary>
        public decimal Low { get; set; }
        /// <summary>
        /// The price at the opening
        /// </summary>
        public decimal Open { get; set; }
        /// <summary>
        /// The last price
        /// </summary>
        public decimal Close { get; set; }
        /// <summary>
        /// The amount of the symbol trades
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The volume of the symbol trades (amount * price)
        /// </summary>
        [JsonProperty("vol")]
        public decimal Volume { get; set; }
        /// <summary>
        /// The number of trades
        /// </summary>
        [JsonProperty("count")]
        public int TradeCount { get; set; }
    }
}
