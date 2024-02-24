using Newtonsoft.Json;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiUnsubscribeRequest
    {
        [JsonProperty("unsub")]
        public string Topic { get; set; } = string.Empty;
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
    }
}
