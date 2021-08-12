using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Account and balance info
    /// </summary>
    public class HuobiAccountBalances: HuobiAccount
    {
        /// <summary>
        /// The list of balances
        /// </summary>
        [JsonProperty("list")]
        public IEnumerable<HuobiBalance> Data { get; set; } = Array.Empty<HuobiBalance>();
    }
}
