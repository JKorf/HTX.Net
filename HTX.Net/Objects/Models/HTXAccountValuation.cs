namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Account valuation
    /// </summary>
    [SerializationModel]
    public record HTXAccountValuation
    {
        /// <summary>
        /// ["<c>balance</c>"] The balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp of the data
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
