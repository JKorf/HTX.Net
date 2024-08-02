namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account info
    /// </summary>
    public record HTXSubAccountInfo
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
        public string Note { get; set; } = string.Empty;
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long? UserId { get; set; }
        /// <summary>
        /// Error code
        /// </summary>
        [JsonPropertyName("errCode")]
        public string? ErrorCode { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        [JsonPropertyName("errMessage")]
        public string? ErrorMessage { get; set; }
    }


}
