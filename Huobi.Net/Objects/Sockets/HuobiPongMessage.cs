using Newtonsoft.Json;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiPongMessage
    {
        [JsonProperty("pong")]
        public long Pong { get; set; }
    }

    internal class HuobiSpotPongMessage
    {
        [JsonProperty("action")]
        public string Action { get; set; } = "pong";
        [JsonProperty("data")]
        public HuobiSpotPingMessage Pong { get; set; } = null!;
    }
}
