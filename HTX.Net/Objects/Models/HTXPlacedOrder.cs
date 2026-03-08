namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Placed order
    /// </summary>
    [SerializationModel]
    public record HTXPlacedOrder
    {
        /// <summary>
        /// ["<c>id</c>"] The id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
