

namespace HTX.Net.Objects.Sockets
{
    internal class HTXPingMessage
    {
        [JsonPropertyName("ping")]
        public long Ping { get; set; }
    }

    internal class HTXSpotPingWrapper
    {
        [JsonPropertyName("data")]
        public HTXSpotPingMessage Data { get; set; } = null!;
    }

    internal class HTXSpotPingMessage
    {
        [JsonPropertyName("ts")]
        public long Ping { get; set; }
    }
}
