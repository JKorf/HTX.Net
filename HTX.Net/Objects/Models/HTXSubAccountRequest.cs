using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account request
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountRequest
    {
        /// <summary>
        /// User name
        /// </summary>
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Note
        /// </summary>
        [JsonPropertyName("note")]
        public string? Note { get; set; }
    }
}
