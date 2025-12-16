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
        /// The status
        /// </summary>
        [JsonPropertyName("marketStatus")]
        public MarketStatus Status { get; set; }
        
        /// <summary>
        /// Start time of when market halted
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("haltStartTime")]
        public DateTime? HaltStartTime { get; set; }
        /// <summary>
        /// Estimated end time of the halt
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("haltEndTime")]
        public DateTime? HaltEndTime { get; set; }
        /// <summary>
        /// Reason for halting
        /// </summary>
        [JsonPropertyName("haltReason")]
        public string? HaltReason { get; set; }
        /// <summary>
        /// Affected symbols, comma separated or 'all' if all symbols are affected
        /// </summary>
        [JsonPropertyName("affectedSymbols")]
        public string? AffectedSymbols { get; set; }
    }
}
