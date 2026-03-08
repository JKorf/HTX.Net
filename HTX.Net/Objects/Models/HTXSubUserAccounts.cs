namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX sub-user account info
    /// </summary>
    [SerializationModel]
    public record HTXSubUserAccounts
    {
        /// <summary>
        /// ["<c>uid</c>"] The id of the sub-user
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }

        /// <summary>
        /// ["<c>deductMode</c>"] Deduct mode
        /// </summary>
        [JsonPropertyName("deductMode")]
        public string DeductMode { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>list</c>"] List of accounts for the sub-user
        /// </summary>
        [JsonPropertyName("list")]
        public HTXSubUserAccount[] Accounts { get; set; } = Array.Empty<HTXSubUserAccount>();
    }
}
