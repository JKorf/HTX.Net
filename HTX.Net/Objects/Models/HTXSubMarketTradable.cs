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
        /// Sub user id
        /// </summary>
        [JsonPropertyName("subUid")]
        public string SubUserId { get; set; } = string.Empty;
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public SubAccountMarketType AccountType { get; set; }
        /// <summary>
        /// Activation
        /// </summary>
        [JsonPropertyName("activation")]
        public string Activation { get; set; } = string.Empty;

        /// <summary>
        /// Error code
        /// </summary>
        [JsonPropertyName("errCode")]
        public int? ErrorCode { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        [JsonPropertyName("errMessage")]
        public string? ErrorMessage { get; set; }
    }


}
