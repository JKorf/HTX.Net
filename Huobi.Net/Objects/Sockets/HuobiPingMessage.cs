﻿using Newtonsoft.Json;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiPingMessage
    {
        [JsonProperty("ping")]
        public long Ping { get; set; }
    }
}
