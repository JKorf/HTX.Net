using Newtonsoft.Json;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiPongMessage
    {
        [JsonProperty("pong")]
        public long Pong { get; set; }
    }
}
