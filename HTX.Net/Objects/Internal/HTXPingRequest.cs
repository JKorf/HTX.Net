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
        public Parameters Data { get; set; }

        public HTXPingAuthResponse(long id)
        {
            Action = "pong";
            Data = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "ts", id}
            };
        }
    }
}
