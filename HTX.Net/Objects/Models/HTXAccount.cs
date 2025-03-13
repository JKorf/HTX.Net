using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX account info
    /// </summary>
    [SerializationModel]
    public record HTXAccount
    {
        /// <summary>
        /// The id of the account
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// The state of the account
        /// </summary>

        [JsonPropertyName("state")]
        public AccountStatus Status { get; set; }
        /// <summary>
        /// The type of the account
        /// </summary>

        [JsonPropertyName("type")]
        public AccountType Type { get; set; }
        /// <summary>
        /// Sub state
        /// </summary>
        [JsonPropertyName("subtype")]
        public string? SubType { get; set; }
    }
}
