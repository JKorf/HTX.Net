using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiAuthRequest
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("ch")]
        public string Channel { get; set; }
    }

    internal class HuobiAuthRequest<T> : HuobiAuthRequest
    {
        [JsonProperty("params")]
        public T Params { get; set; }
    }
}
