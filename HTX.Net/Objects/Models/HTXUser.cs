using HTX.Net.Enums;


namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX user info
    /// </summary>
    [SerializationModel]
    public record HTXUser
    {
        /// <summary>
        /// The id of the user
        /// </summary>
        [JsonPropertyName("uid")]
        public long Id { get; set; }
        /// <summary>
        /// The status of the user
        /// </summary>
        [JsonPropertyName("userState")]
        public UserStatus Status { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        [JsonPropertyName("subUserName")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// User name
        /// </summary>
        [JsonPropertyName("note")]
        public string? Note { get; set; }
    }
}
