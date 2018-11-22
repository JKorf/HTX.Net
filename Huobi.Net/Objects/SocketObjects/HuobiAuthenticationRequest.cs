using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.SocketObjects
{
    public class HuobiAuthenticationRequest
    {
        [JsonProperty("op")]
        public string Operation { get; set; }
        public string AccessKeyId { get; set; }
        public string SignatureMethod { get; set; }
        public string SignatureVersion { get; set; }
        public string Timestamp { get; set; }
        public string Signature { get; set; }
    }
}
