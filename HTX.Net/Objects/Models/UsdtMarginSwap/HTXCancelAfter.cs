using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Cancel after status
    /// </summary>
    [SerializationModel]
    public record HTXCancelAfter
    {
        /// <summary>
        /// Current time
        /// </summary>
        [JsonPropertyName("current_time")]
        public DateTime CurrentTime { get; set; }
        /// <summary>
        /// Trigger time
        /// </summary>
        [JsonPropertyName("trigger_time")]
        public DateTime? TriggerTime { get; set; }
    }
}
