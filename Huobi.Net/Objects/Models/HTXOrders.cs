namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Orders info
    /// </summary>
    public record HTXOrders
	{
        /// <summary>
        /// Timestamp for pagination
        /// </summary>
        [JsonPropertyName("nextTime")]
        public DateTime NextTime { get; set; }
        /// <summary>
        /// List of ticks for symbols
        /// </summary>
        [JsonPropertyName("orders")]
        public IEnumerable<HTXOrder> Orders { get; set; } = Array.Empty<HTXOrder>();
    }
}
