namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Order id
    /// </summary>
    public record HTXOrderId
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order-id")]
        public long OrderId { get; set; }
    }
}
