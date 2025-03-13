using CryptoExchange.Net.Converters.SystemTextJson;


namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Best offer update
    /// </summary>
    [SerializationModel]
    public record HTXBestOfferUpdate
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
        /// Best bid
        /// </summary>
        [JsonPropertyName("bid")]
        public HTXOrderBookEntry Bid { get; set; } = null!;
        /// <summary>
        /// Best ask
        /// </summary>
        [JsonPropertyName("ask")]
        public HTXOrderBookEntry Ask { get; set; } = null!;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        [JsonPropertyName("version")]
        public long Version { get; set; }
    }
}
