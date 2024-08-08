namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Order book
    /// </summary>
    public record HTXOrderBook
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        [JsonPropertyName("version")]
        public long Version { get; set; }
        [JsonInclude, JsonPropertyName("seqNum")]
        internal long SequenceNumber
        {
            get => Version;
            set => Version = value;
        }

        /// <summary>
        /// List of bids
        /// </summary>
        [JsonPropertyName("bids")]
        public IEnumerable<HTXOrderBookEntry> Bids { get; set; } = Array.Empty<HTXOrderBookEntry>();
        /// <summary>
        /// List of asks
        /// </summary>
        [JsonPropertyName("asks")]
        public IEnumerable<HTXOrderBookEntry> Asks { get; set; } = Array.Empty<HTXOrderBookEntry>();
    }

    /// <summary>
    /// Incremental order book update
    /// </summary>
    public record HTXIncementalOrderBook
    {
        /// <summary>
        /// Sequence number
        /// </summary>
        [JsonPropertyName("seqNum")]
        public long SequenceNumber { get; set; }
        /// <summary>
        /// Previous sequence number
        /// </summary>
        [JsonPropertyName("prevSeqNum")]
        public long? PreviousSequenceNumber { get; set; }
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
