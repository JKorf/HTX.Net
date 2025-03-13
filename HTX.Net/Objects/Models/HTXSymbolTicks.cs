using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Symbol ticks
    /// </summary>
    [SerializationModel]
    public record HTXSymbolDatas
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of ticks for symbols
        /// </summary>
        [JsonPropertyName("ticks")]
        public HTXSymbolTicker[] Ticks { get; set; } = Array.Empty<HTXSymbolTicker>();
    }

    /// <summary>
    /// Symbol ticks
    /// </summary>
    [SerializationModel]
    public record HTXSymbolTicks
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of ticks for symbols
        /// </summary>
        [JsonPropertyName("ticks")]
        public HTXSymbolTick[] Ticks { get; set; } = Array.Empty<HTXSymbolTick>();
    }
}
