using Newtonsoft.Json;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiSubscribeRequest
    {
        [JsonProperty("sub")]
        public string Topic { get; set; } = string.Empty;
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
    }
}
