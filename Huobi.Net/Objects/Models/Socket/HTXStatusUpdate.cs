using HTX.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// System status update
    /// </summary>
    public record HTXStatusUpdate : HTXOpMessage
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXStatus Data { get; set; } = null!;
    }

    /// <summary>
    /// System status update
    /// </summary>
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
