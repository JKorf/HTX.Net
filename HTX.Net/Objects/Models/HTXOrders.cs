namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Orders info
    /// </summary>
    [SerializationModel]
    public record HTXOrders
	{
        /// <summary>
        /// ["<c>nextTime</c>"] Timestamp for pagination
        /// </summary>
        [JsonPropertyName("nextTime")]
        public DateTime NextTime { get; set; }
        /// <summary>
        /// ["<c>orders</c>"] List of ticks for symbols
        /// </summary>
        [JsonPropertyName("orders")]
        public HTXOrder[] Orders { get; set; } = Array.Empty<HTXOrder>();
    }
}
