namespace HTX.Net.Objects.Internal
{
    internal class HTXPingRequest
    {
        [JsonPropertyName("ping")]
        public long Ping { get; set; }
    }

    internal class HTXPingResponse
    {
        [JsonPropertyName("pong")]
        public long Pong { get; set; }

        public HTXPingResponse(long id)
        {
            Pong = id;
        }
    }

    internal class HTXPingAuthResponse
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }
        [JsonPropertyName("data")]
        public Dictionary<string, object> Data { get; set; }

        public HTXPingAuthResponse(long id)
        {
            Action = "pong";
            Data = new ParameterCollection()
            {
                { "ts", id}
            };
        }
    }
}
