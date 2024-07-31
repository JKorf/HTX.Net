namespace HTX.Net.Objects.Internal
{
    internal class HTXRequest
    {
        [JsonIgnore]
        public string? Id { get; set; }
    }

    internal class HTXSocketRequest: HTXRequest
    {
        [JsonPropertyName("req")]
        public string Request { get; set; }

        [JsonPropertyName("id")]
        public new string Id { get; set; }


        public HTXSocketRequest(string id, string topic)
        {
            Id = id;
            Request = topic;
        }
    }
}
