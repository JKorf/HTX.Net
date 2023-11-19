using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiPongMessage
    {
        [JsonProperty("pong")]
        public long Pong { get; set; }
    }
}
