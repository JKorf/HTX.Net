namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Best offer update
    /// </summary>
    [SerializationModel]
    public record HTXBestOfferUpdate
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
        /// ["<c>bid</c>"] Best bid
        /// </summary>
        [JsonPropertyName("bid")]
        public HTXOrderBookEntry Bid { get; set; } = null!;
        /// <summary>
        /// ["<c>ask</c>"] Best ask
        /// </summary>
        [JsonPropertyName("ask")]
        public HTXOrderBookEntry Ask { get; set; } = null!;
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>version</c>"] Version
        /// </summary>
        [JsonPropertyName("version")]
        public long Version { get; set; }
    }
}
