namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account API key
    /// </summary>
    [SerializationModel]
    public record HTXSubApiKey
    {
        /// <summary>
        /// ["<c>accessKey</c>"] Access key
        /// </summary>
        [JsonPropertyName("accessKey")]
        public string AccessKey { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>secretKey</c>"] Secret key
        /// </summary>
        [JsonPropertyName("secretKey")]
        public string SecretKey { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>note</c>"] Note
        /// </summary>
        [JsonPropertyName("note")]
        public string? Note { get; set; }
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
