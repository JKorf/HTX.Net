using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi sub-user account info
    /// </summary>
    public class HuobiSubUserAccounts
    {
        /// <summary>
        /// The id of the sub-user
        /// </summary>
        [JsonProperty("uid")]
        public long UserId { get; set; }

        /// <summary>
        /// Deduct mode
        /// </summary>
        public string DeductMode { get; set; } = string.Empty;

        /// <summary>
        /// List of accounts for the sub-user
        /// </summary>
        [JsonProperty("list")]
        public IEnumerable<HuobiSubUserAccount> Accounts { get; set; } = Array.Empty<HuobiSubUserAccount>();
    }
}
