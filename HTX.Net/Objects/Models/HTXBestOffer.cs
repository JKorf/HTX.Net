namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Info on a symbol's best offer
    /// </summary>
    [SerializationModel]
    public record HTXBestOffer
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteTime</c>"] Time of the data
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("quoteTime")]
        public DateTime QuoteTime { get; set; }
        /// <summary>
        /// ["<c>bid</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bidSize</c>"] Quantity of the best bid
        /// </summary>
        [JsonPropertyName("bidSize")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>ask</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>askSize</c>"] Quantity of the best ask
        /// </summary>
        [JsonPropertyName("askSize")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>seqId</c>"] Sequence number
        /// </summary>
        [JsonPropertyName("seqId")]
        public long Sequence { get; set; }
    }
}
