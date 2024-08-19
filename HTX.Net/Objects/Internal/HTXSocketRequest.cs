namespace HTX.Net.Objects.Internal
{
    internal class HTXSocketRequest
    {
        [JsonPropertyName("req")]
        public string Request { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }


        public HTXSocketRequest(string id, string topic)
        {
            Id = id;
            Request = topic;
        }
    }
}
