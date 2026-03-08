namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order book update
    /// </summary>
    [SerializationModel]
    public record HTXOrderBookUpdate
    {
        /// <summary>
        /// ["<c>mrid</c>"] Order id
        /// </summary>
        [JsonPropertyName("mrid")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Update id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>version</c>"] Version
        /// </summary>
        [JsonPropertyName("version")]
        public long? Version { get; set; }
        /// <summary>
        /// ["<c>bids</c>"] List of changed bids
        /// </summary>
        [JsonPropertyName("bids")]
        public HTXOrderBookEntry[] Bids { get; set; } = Array.Empty<HTXOrderBookEntry>();
        /// <summary>
        /// ["<c>asks</c>"] List of changed asks
        /// </summary>
        [JsonPropertyName("asks")]
        public HTXOrderBookEntry[] Asks { get; set; } = Array.Empty<HTXOrderBookEntry>();
    }
}
