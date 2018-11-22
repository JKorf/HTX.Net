using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects
{
    public class HuobiAccountBalances: HuobiAccount
    {
        [JsonProperty("list")]
        public List<HuobiBalance> Data { get; set; }
    }
}
