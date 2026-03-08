using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// API key info
    /// </summary>
    [SerializationModel]
    public record HTXApiKeyInfo
    {
        /// <summary>
        /// ["<c>accessKey</c>"] Access key
        /// </summary>
        [JsonPropertyName("accessKey")]
        public string AccessKey { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public ApiKeyStatus Status { get; set; }
        /// <summary>
        /// ["<c>note</c>"] Note
        /// </summary>
        [JsonPropertyName("note")]
        public string? Note { get; set; }
        /// <summary>
        /// ["<c>permission</c>"] Permissions
        /// </summary>
        [JsonPropertyName("permission")]
        public string Permissions { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ipAddresses</c>"] Ip addresses
        /// </summary>
        [JsonPropertyName("ipAddresses")]
        public string IpAddresses { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>validDays</c>"] Valid days
        /// </summary>
        [JsonPropertyName("validDays")]
        public int ValidDays { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }


}
