using System;
using System.Collections.Generic;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi sub-user account info
    /// </summary>
    public class HuobiSubUserAccount
    {
        /// <summary>
        /// The type of the account
        /// </summary>
        [JsonProperty("accountType"), JsonConverter(typeof(AccountTypeConverter))]
        public AccountType Type { get; set; }
        /// <summary>
        /// Whether the account is active of not
        /// </summary>
        [JsonConverter(typeof(AccountActivationConverter))]
        public AccountActivation Activation { get; set; }
        /// <summary>
        /// Whether transfers are allowed (only for spot account type)
        /// </summary>
        public bool? Transferrable { get; set; }
        /// <summary>
        /// Account ids
        /// </summary>
        public IEnumerable<HuobiSubUserAccountId> AccountIds { get; set; } = Array.Empty<HuobiSubUserAccountId>();
    }
}
