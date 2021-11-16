using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
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
