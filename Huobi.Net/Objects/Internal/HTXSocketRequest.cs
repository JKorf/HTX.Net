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

    internal class HTXAuthenticatedSubscribeRequest
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }
        [JsonPropertyName("ch")]
        public string Channel { get; set; }

        public HTXAuthenticatedSubscribeRequest(string channel, string action = "sub")
        {
            Action = action;
            Channel = channel;
        }
    }

    internal class HTXSubscribeRequest: HTXRequest
    {
        [JsonPropertyName("sub")]
        public string Topic { get; set; }
        [JsonPropertyName("id")]
        public new string Id { get; set; }

        public HTXSubscribeRequest(string id, string topic)
        {
            Id = id;
            Topic = topic;
        }
    }

    internal class HTXIncrementalOrderBookSubscribeRequest : HTXSubscribeRequest
    {
        [JsonPropertyName("data_type")]
        public string DataType { get; set; }
        public HTXIncrementalOrderBookSubscribeRequest(string id, string topic, string dataType) : base(id, topic) 
        { 
            DataType = dataType;
        }
    }
}
