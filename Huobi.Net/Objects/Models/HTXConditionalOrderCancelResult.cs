namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Conditional order cancelation result
    /// </summary>
    public record HTXConditionalOrderCancelResult
    {
        /// <summary>
        /// Orders accepted for cancelation
        /// </summary>
        [JsonPropertyName("accepted")]
        public IEnumerable<string> Accepted { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Orders rejected for cancelation
        /// </summary>
        [JsonPropertyName("rejected")]
        public IEnumerable<string> Rejected { get; set; } = Array.Empty<string>();
    }
}
