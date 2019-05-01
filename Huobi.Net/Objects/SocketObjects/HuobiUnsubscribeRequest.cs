using Newtonsoft.Json;

namespace Huobi.Net.Objects.SocketObjects
{
    public class HuobiUnsubscribeRequest
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

    public class HuobiAuthUnsubscribeRequest
    {
        [JsonProperty("op")]
        public string Operation { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("cid")]
        public string Id { get; set; }

        public HuobiAuthUnsubscribeRequest(string id, string topic)
        {
            Operation = "unsub";
            Topic = topic;
            Id = id;
        }
    }
}
