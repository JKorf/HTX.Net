namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account request
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountRequest
    {
        /// <summary>
        /// ["<c>userName</c>"] User name
        /// </summary>
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>note</c>"] Note
        /// </summary>
        [JsonPropertyName("note")]
        public string? Note { get; set; }
    }
}
