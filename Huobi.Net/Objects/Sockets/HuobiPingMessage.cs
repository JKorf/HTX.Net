using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiPingMessage
    {
        [JsonProperty("ping")]
        public long Ping { get; set; }
    }
}
