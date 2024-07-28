

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Symbol update
    /// </summary>
    public record HTXSymbolTickUpdate: HTXSymbolData
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("mrid")]
        public long OrderId { get; set; }
        /// <summary>
        /// Turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public long TradeTurnover { get; set; }
        /// <summary>
        /// Best bid
        /// </summary>
        [JsonPropertyName("bid")]
        public HTXOrderBookEntry BestBid { get; set; } = null!;
        /// <summary>
        /// Best ask
        /// </summary>
        [JsonPropertyName("ask")]
        public HTXOrderBookEntry BestAsk { get; set; } = null!;
    }
}
