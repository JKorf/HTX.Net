namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Edit result
    /// </summary>
    [SerializationModel]
    public record HTXSubApiKeyEdit
    {
        /// <summary>
        /// ["<c>note</c>"] Note
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>permission</c>"] Permission, comma seperated
        /// </summary>
        [JsonPropertyName("permission")]
        public string Permission { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ipAddresses</c>"] Ip addresses, comma seperated
        /// </summary>
        [JsonPropertyName("ipAddresses")]
        public string IpAddresses { get; set; } = string.Empty;
    }


}
