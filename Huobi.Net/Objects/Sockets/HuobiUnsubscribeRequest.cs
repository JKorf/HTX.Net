using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiUnsubscribeRequest
    {
        [JsonProperty("unsub")]
        public string Topic { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
