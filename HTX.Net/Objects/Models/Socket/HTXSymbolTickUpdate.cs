namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Symbol update
    /// </summary>
    [SerializationModel]
    public record HTXSymbolTickUpdate: HTXSymbolData
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>mrid</c>"] Order id
        /// </summary>
        [JsonPropertyName("mrid")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal TradeTurnover { get; set; }
        /// <summary>
        /// ["<c>bid</c>"] Best bid
        /// </summary>
        [JsonPropertyName("bid")]
        public HTXOrderBookEntry BestBid { get; set; } = null!;
        /// <summary>
        /// ["<c>ask</c>"] Best ask
        /// </summary>
        [JsonPropertyName("ask")]
        public HTXOrderBookEntry BestAsk { get; set; } = null!;
    }
}
