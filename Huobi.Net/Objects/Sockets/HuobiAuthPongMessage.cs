using Newtonsoft.Json;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiAuthPongMessage
    {
        [JsonProperty("action")]
        public string Action { get; set; } = string.Empty;
        [JsonProperty("data")]
        public HuobiAuthPongMessageTimestamp Data { get; set; } = null!;
    }

    internal class HuobiAuthPingMessage
    {
        [JsonProperty("action")]
        public string Action { get; set; } = string.Empty;
        [JsonProperty("data")]
        public HuobiAuthPingMessageTimestamp Data { get; set; } = null!;
    }

    internal class HuobiAuthPongMessageTimestamp
    {
        [JsonProperty("pong")]
        public long Pong { get; set; }
    }

    internal class HuobiAuthPingMessageTimestamp
    {
        [JsonProperty("ts")]
        public long Ping { get; set; }
    }
}
