

namespace HTX.Net.Objects.Sockets
{
    internal class HTXAuthPongMessage
    {
        [JsonPropertyName("action")]
        public string Action { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public HTXAuthPongMessageTimestamp Data { get; set; } = null!;
    }

    internal class HTXAuthPingMessage
    {
        [JsonPropertyName("action")]
        public string Action { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public HTXAuthPingMessageTimestamp Data { get; set; } = null!;
    }

    internal class HTXAuthPongMessageTimestamp
    {
        [JsonPropertyName("pong")]
        public long Pong { get; set; }
    }

    internal class HTXAuthPingMessageTimestamp
    {
        [JsonPropertyName("ts")]
        public long Ping { get; set; }
    }
}
