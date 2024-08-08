

namespace HTX.Net.Objects.Internal
{
    internal class HTXSocketRequestOp
    {
        [JsonPropertyName("op")]
        public string Operation { get; set; }
        [JsonPropertyName("cid")]
        public string ClientId { get; set; }
        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        public HTXSocketRequestOp(string operation, string clientId, string topic)
        {
            Operation = operation;
            ClientId = clientId;
            Topic = topic;
        }
    }
}
