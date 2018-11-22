using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects
{
    public class HuobiBatchCancelResult
    {
        [JsonProperty("success")]
        public long[] Successful { get; set; }
        public long[] Failed { get; set; }
    }
}
