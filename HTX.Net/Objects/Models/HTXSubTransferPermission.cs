namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Transfer permission
    /// </summary>
    [SerializationModel]
    public record HTXSubTransferPermission
    {
        /// <summary>
        /// ["<c>accountType</c>"] Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public string AccountType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transferrable</c>"] Transferrable
        /// </summary>
        [JsonPropertyName("transferrable")]
        public bool? Transferrable { get; set; }
        /// <summary>
        /// ["<c>subUid</c>"] Sub user id
        /// </summary>
        [JsonPropertyName("subUid")]
        public long SubUserId { get; set; }
        /// <summary>
        /// ["<c>errCode</c>"] Error code
        /// </summary>
        [JsonPropertyName("errCode")]
        public int? ErrorCode { get; set; }
        /// <summary>
        /// ["<c>errMessage</c>"] Error message
        /// </summary>
        [JsonPropertyName("errMessage")]
        public string? ErrorMessage { get; set; }
    }


}
