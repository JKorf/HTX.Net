using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Status of the symbol
    /// </summary>
    [SerializationModel]
    public record HTXSymbolStatus
    {
        /// <summary>
        /// ["<c>marketStatus</c>"] The status
        /// </summary>
        [JsonPropertyName("marketStatus")]
        public MarketStatus Status { get; set; }
        
        /// <summary>
        /// ["<c>haltStartTime</c>"] Start time of when market halted
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("haltStartTime")]
        public DateTime? HaltStartTime { get; set; }
        /// <summary>
        /// ["<c>haltEndTime</c>"] Estimated end time of the halt
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("haltEndTime")]
        public DateTime? HaltEndTime { get; set; }
        /// <summary>
        /// ["<c>haltReason</c>"] Reason for halting
        /// </summary>
        [JsonPropertyName("haltReason")]
        public string? HaltReason { get; set; }
        /// <summary>
        /// ["<c>affectedSymbols</c>"] Affected symbols, comma separated or 'all' if all symbols are affected
        /// </summary>
        [JsonPropertyName("affectedSymbols")]
        public string? AffectedSymbols { get; set; }
    }
}
