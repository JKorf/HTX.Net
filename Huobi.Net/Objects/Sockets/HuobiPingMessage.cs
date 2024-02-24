using Newtonsoft.Json;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiPingMessage
    {
        [JsonProperty("ping")]
        public long Ping { get; set; }
    }

    internal class HuobiSpotPingWrapper
    {
        [JsonProperty("data")]
        public HuobiSpotPingMessage Data { get; set; } = null!;
    }

    internal class HuobiSpotPingMessage
    {
        [JsonProperty("ts")]
        public long Ping { get; set; }
    }
}
