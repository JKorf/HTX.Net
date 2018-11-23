using Newtonsoft.Json;
using System.Collections.Generic;

namespace Huobi.Net.Objects
{
    public class HuobiAccountBalances: HuobiAccount
    {
        /// <summary>
        /// The list of balances
        /// </summary>
        [JsonProperty("list")]
        public List<HuobiBalance> Data { get; set; }
    }
}
