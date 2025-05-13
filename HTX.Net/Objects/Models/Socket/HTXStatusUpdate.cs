using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Objects.Sockets;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// System status update
    /// </summary>
    [SerializationModel]
    public record HTXStatusUpdate : HTXOpMessage
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Event, init (initial) status or update
        /// </summary>
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXStatus Data { get; set; } = null!;
    }

    /// <summary>
    /// System status update
    /// </summary>
    [SerializationModel]
    public record HTXStatus
    {
        /// <summary>
        /// Is system available
        /// </summary>
        [JsonPropertyName("heartbeat")]
        public bool Available { get; set; }

        /// <summary>
        /// Estimated recovery time
        /// </summary>
        [JsonPropertyName("estimated_recovery_time")]
        public DateTime? EstimatedRecoveryTime { get; set; }
    }
}
