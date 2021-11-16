using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
{
    internal class HuobiUnsubscribeRequest
    {
        [JsonProperty("unsub")]
        public string Topic { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        public HuobiUnsubscribeRequest(string id, string topic)
        {
            Topic = topic;
            Id = id;
        }
    }
}
