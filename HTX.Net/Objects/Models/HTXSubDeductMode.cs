using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account deduct mode
    /// </summary>
    public record HTXSubDeductMode
    {
        /// <summary>
        /// Sub uid
        /// </summary>
        [JsonPropertyName("subUid")]
        public string SubUid { get; set; } = string.Empty;
        /// <summary>
        /// Deduct mode
        /// </summary>
        [JsonPropertyName("deductMode")]
        public DeductMode? DeductMode { get; set; }
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
