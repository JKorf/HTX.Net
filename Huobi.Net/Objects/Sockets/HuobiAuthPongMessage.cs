using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiAuthPongMessage
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("data")]
        public HuobiAuthPongMessageTimestamp Data { get; set; }
    }
    internal class HuobiAuthPingMessage
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("data")]
        public HuobiAuthPingMessageTimestamp Data { get; set; }
    }

    public class HuobiAuthPongMessageTimestamp
    {
        [JsonProperty("pong")]
        public long Pong { get; set; }
    }

    public class HuobiAuthPingMessageTimestamp
    {
        [JsonProperty("ts")]
        public long Ping { get; set; }
    }
}
