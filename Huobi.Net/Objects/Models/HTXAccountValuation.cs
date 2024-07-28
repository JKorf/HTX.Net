namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Account valuation
    /// </summary>
    public record HTXAccountValuation
    {
        /// <summary>
        /// The balance
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
