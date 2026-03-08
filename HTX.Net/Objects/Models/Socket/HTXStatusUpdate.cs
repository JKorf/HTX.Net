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
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>event</c>"] Event, init (initial) status or update
        /// </summary>
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>data</c>"] Data
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
        /// ["<c>heartbeat</c>"] Is system available
        /// </summary>
        [JsonPropertyName("heartbeat")]
        public bool Available { get; set; }

        /// <summary>
        /// ["<c>estimated_recovery_time</c>"] Estimated recovery time
        /// </summary>
        [JsonPropertyName("estimated_recovery_time")]
        public DateTime? EstimatedRecoveryTime { get; set; }
    }
}
