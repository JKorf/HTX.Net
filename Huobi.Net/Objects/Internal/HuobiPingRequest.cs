using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
{
    internal class HuobiPingRequest
    {
        [JsonProperty("ping")]
        public long Ping { get; set; }
    }

    internal class HuobiPingResponse
    {
        [JsonProperty("pong")]
        public long Pong { get; set; }

        public HuobiPingResponse(long id)
        {
            Pong = id;
        }
    }

    internal class HuobiPingAuthResponse
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }

        public HuobiPingAuthResponse(long id)
        {
            Action = "pong";
            Data = new Dictionary<string, object>()
            {
                { "ts", id}
            };
        }
    }
}
