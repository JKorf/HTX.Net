using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiSocketAuthResponse
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("ch")]
        public string Channel { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
