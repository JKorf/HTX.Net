

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Best offer update
    /// </summary>
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
        public long Id { get; set; }
        /// <summary>
        /// Best bid
        /// </summary>
        public HTXOrderBookEntry Bid { get; set; } = null!;
        /// <summary>
        /// Best ask
        /// </summary>
        public HTXOrderBookEntry Ask { get; set; } = null!;
    }
}
