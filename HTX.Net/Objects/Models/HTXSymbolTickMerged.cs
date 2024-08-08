using CryptoExchange.Net.Converters;


namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Symbol tick info
    /// </summary>
    public record HTXSymbolTickMerged: HTXSymbolData
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The id of the tick
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// The current best bid for the symbol
        /// </summary>
        [JsonPropertyName("bid")]
        public HTXOrderBookEntry? BestBid { get; set; }

        /// <summary>
        /// The current best ask for the symbol
        /// </summary>
        [JsonPropertyName("ask")]
        public HTXOrderBookEntry? BestAsk { get; set; }
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public record HTXOrderBookEntry: ISymbolOrderBookEntry
    {
        /// <summary>
        /// The price for this entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity for this entry
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }

    }
}
