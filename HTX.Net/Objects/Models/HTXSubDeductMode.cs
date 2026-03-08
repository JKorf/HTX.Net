using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account deduct mode
    /// </summary>
    [SerializationModel]
    public record HTXSubDeductMode
    {
        /// <summary>
        /// ["<c>subUid</c>"] Sub uid
        /// </summary>
        [JsonPropertyName("subUid")]
        public string SubUid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>deductMode</c>"] Deduct mode
        /// </summary>
        [JsonPropertyName("deductMode")]
        public DeductMode? DeductMode { get; set; }
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
