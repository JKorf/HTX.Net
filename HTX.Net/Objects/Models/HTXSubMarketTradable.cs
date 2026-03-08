using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account tradable market status
    /// </summary>
    [SerializationModel]
    public record HTXSubMarketTradable
    {
        /// <summary>
        /// ["<c>subUid</c>"] Sub user id
        /// </summary>
        [JsonPropertyName("subUid")]
        public string SubUserId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>accountType</c>"] Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public SubAccountMarketType AccountType { get; set; }
        /// <summary>
        /// ["<c>activation</c>"] Activation
        /// </summary>
        [JsonPropertyName("activation")]
        public string Activation { get; set; } = string.Empty;

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
