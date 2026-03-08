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
        /// ["<c>uid</c>"] The id of the user
        /// </summary>
        [JsonPropertyName("uid")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>userState</c>"] The status of the user
        /// </summary>
        [JsonPropertyName("userState")]
        public UserStatus Status { get; set; }
        /// <summary>
        /// ["<c>subUserName</c>"] User name
        /// </summary>
        [JsonPropertyName("subUserName")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>note</c>"] User name
        /// </summary>
        [JsonPropertyName("note")]
        public string? Note { get; set; }
    }
}
