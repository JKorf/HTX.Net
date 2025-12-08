namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account API key
    /// </summary>
    [SerializationModel]
    public record HTXSubApiKey
    {
        /// <summary>
        /// Access key
        /// </summary>
        [JsonPropertyName("accessKey")]
        public string AccessKey { get; set; } = string.Empty;
        /// <summary>
        /// Secret key
        /// </summary>
        [JsonPropertyName("secretKey")]
        public string SecretKey { get; set; } = string.Empty;
        /// <summary>
        /// Note
        /// </summary>
        [JsonPropertyName("note")]
        public string? Note { get; set; }
        /// <summary>
        /// Permission, comma seperated
        /// </summary>
        [JsonPropertyName("permission")]
        public string Permission { get; set; } = string.Empty;
        /// <summary>
        /// Ip addresses, comma seperated
        /// </summary>
        [JsonPropertyName("ipAddresses")]
        public string IpAddresses { get; set; } = string.Empty;
    }


}
