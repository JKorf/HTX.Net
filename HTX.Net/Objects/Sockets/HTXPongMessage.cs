

namespace HTX.Net.Objects.Sockets
{
    internal class HTXPongMessage
    {
        [JsonPropertyName("pong")]
        public long Pong { get; set; }
    }

    internal class HTXSpotPongMessage
    {
        [JsonPropertyName("action")]
        public string Action { get; set; } = "pong";
        [JsonPropertyName("data")]
        public HTXSpotPingMessage Pong { get; set; } = null!;
    }
}
