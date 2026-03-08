using HTX.Net.Enums;


namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX sub-user account id and state
    /// </summary>
    [SerializationModel]
    public record HTXSubUserAccountId
    {
        /// <summary>
        /// ["<c>accountId</c>"] The id of the account
        /// </summary>
        [JsonPropertyName("accountId")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>accountStatus</c>"] The status of the account
        /// </summary>
        [JsonPropertyName("accountStatus")]
        public AccountStatus Status { get; set; }
        /// <summary>
        /// ["<c>subtype</c>"] Sub state
        /// </summary>
        [JsonPropertyName("subtype")]
        public string? SubType { get; set; }
    }
}
