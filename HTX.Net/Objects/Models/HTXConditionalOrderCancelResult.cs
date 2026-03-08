namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Conditional order cancelation result
    /// </summary>
    [SerializationModel]
    public record HTXConditionalOrderCancelResult
    {
        /// <summary>
        /// ["<c>accepted</c>"] Orders accepted for cancelation
        /// </summary>
        [JsonPropertyName("accepted")]
        public string[] Accepted { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>rejected</c>"] Orders rejected for cancelation
        /// </summary>
        [JsonPropertyName("rejected")]
        public string[] Rejected { get; set; } = Array.Empty<string>();
    }
}
