using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Access key
        /// </summary>
        [JsonPropertyName("accessKey")]
        public string AccessKey { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public ApiKeyStatus Status { get; set; }
        /// <summary>
        /// Note
        /// </summary>
        [JsonPropertyName("note")]
        public string? Note { get; set; }
        /// <summary>
        /// Permissions
        /// </summary>
        [JsonPropertyName("permission")]
        public string Permissions { get; set; } = string.Empty;
        /// <summary>
        /// Ip addresses
        /// </summary>
        [JsonPropertyName("ipAddresses")]
        public string IpAddresses { get; set; } = string.Empty;
        /// <summary>
        /// Valid days
        /// </summary>
        [JsonPropertyName("validDays")]
        public int ValidDays { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }


}
