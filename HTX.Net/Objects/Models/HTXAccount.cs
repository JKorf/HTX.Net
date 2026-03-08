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
        /// ["<c>id</c>"] The id of the account
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>state</c>"] The state of the account
        /// </summary>

        [JsonPropertyName("state")]
        public AccountStatus Status { get; set; }
        /// <summary>
        /// ["<c>type</c>"] The type of the account
        /// </summary>

        [JsonPropertyName("type")]
        public AccountType Type { get; set; }
        /// <summary>
        /// ["<c>subtype</c>"] Sub state
        /// </summary>
        [JsonPropertyName("subtype")]
        public string? SubType { get; set; }
    }
}
