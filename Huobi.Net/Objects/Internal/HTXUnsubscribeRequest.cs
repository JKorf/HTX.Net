

namespace HTX.Net.Objects.Internal
{
    internal class HTXUnsubscribeRequest
    {
        [JsonPropertyName("unsub")]
        public string Topic { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        public HTXUnsubscribeRequest(string id, string topic)
        {
            Topic = topic;
            Id = id;
        }
    }
}
