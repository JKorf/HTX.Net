namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order book update
    /// </summary>
    public record HTXOrderBookUpdate
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("mrid")]
        public long OrderId { get; set; }
        /// <summary>
        /// Update id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        [JsonPropertyName("version")]
        public long? Version { get; set; }
        /// <summary>
        /// List of changed bids
        /// </summary>
        [JsonPropertyName("bids")]
        public IEnumerable<HTXOrderBookEntry> Bids { get; set; } = Array.Empty<HTXOrderBookEntry>();
        /// <summary>
        /// List of changed asks
        /// </summary>
        [JsonPropertyName("asks")]
        public IEnumerable<HTXOrderBookEntry> Asks { get; set; } = Array.Empty<HTXOrderBookEntry>();
    }
}
