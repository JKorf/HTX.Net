using HTX.Net.Objects.Sockets;

namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// V5 socket data event
    /// </summary>
    [SerializationModel]
    public record HTXDataEventV5<T> : HTXOpMessageV5
    {
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>uid</c>"] User id
        /// </summary>
        [JsonPropertyName("uid")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>event</c>"] Event
        /// </summary>
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
