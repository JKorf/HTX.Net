namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account info
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountInfo
    {
        /// <summary>
        /// ["<c>userName</c>"] User name
        /// </summary>
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>note</c>"] Note
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>uid</c>"] User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long? UserId { get; set; }
        /// <summary>
        /// ["<c>errCode</c>"] Error code
        /// </summary>
        [JsonPropertyName("errCode")]
        public string? ErrorCode { get; set; }
        /// <summary>
        /// ["<c>errMessage</c>"] Error message
        /// </summary>
        [JsonPropertyName("errMessage")]
        public string? ErrorMessage { get; set; }
    }


}
