namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Placed order
    /// </summary>
    [SerializationModel]
    public record HTXPlacedOrder
    {
        /// <summary>
        /// The id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
