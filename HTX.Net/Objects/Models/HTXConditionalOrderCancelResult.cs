using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Conditional order cancelation result
    /// </summary>
    [SerializationModel]
    public record HTXConditionalOrderCancelResult
    {
        /// <summary>
        /// Orders accepted for cancelation
        /// </summary>
        [JsonPropertyName("accepted")]
        public string[] Accepted { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Orders rejected for cancelation
        /// </summary>
        [JsonPropertyName("rejected")]
        public string[] Rejected { get; set; } = Array.Empty<string>();
    }
}
