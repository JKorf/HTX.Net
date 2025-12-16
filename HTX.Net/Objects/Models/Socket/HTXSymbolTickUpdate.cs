namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Symbol update
    /// </summary>
    [SerializationModel]
    public record HTXSymbolTickUpdate: HTXSymbolData
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
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
        public decimal TradeTurnover { get; set; }
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
