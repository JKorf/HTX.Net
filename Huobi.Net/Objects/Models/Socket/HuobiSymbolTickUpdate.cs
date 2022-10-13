using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Socket
{
    /// <summary>
    /// Symbol update
    /// </summary>
    public class HuobiSymbolTickUpdate: HuobiSymbolData
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("mrid")]
        public long OrderId { get; set; }
        /// <summary>
        /// Turnover
        /// </summary>
        [JsonProperty("trade_turnover")]
        public long TradeTurnover { get; set; }
        /// <summary>
        /// Best bid
        /// </summary>
        [JsonProperty("bid")]
        public HuobiOrderBookEntry BestBid { get; set; } = null!;
        /// <summary>
        /// Best ask
        /// </summary>
        [JsonProperty("ask")]
        public HuobiOrderBookEntry BestAsk { get; set; } = null!;
    }
}
