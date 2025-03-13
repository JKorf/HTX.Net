using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Transfer permission
    /// </summary>
    [SerializationModel]
    public record HTXSubTransferPermission
    {
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public string AccountType { get; set; } = string.Empty;
        /// <summary>
        /// Transferrable
        /// </summary>
        [JsonPropertyName("transferrable")]
        public bool? Transferrable { get; set; }
        /// <summary>
        /// Sub user id
        /// </summary>
        [JsonPropertyName("subUid")]
        public long SubUserId { get; set; }
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
