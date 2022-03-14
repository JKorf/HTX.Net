using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi sub-user account id and state
    /// </summary>
    public class HuobiSubUserAccountId
    {
        /// <summary>
        /// The id of the account
        /// </summary>
        [JsonProperty("accountId")]
        public long Id { get; set; }
        /// <summary>
        /// The state of the account
        /// </summary>
        [JsonProperty("accountStatus"), JsonConverter(typeof(AccountStateConverter))]
        public AccountState State { get; set; }
        /// <summary>
        /// Sub state
        /// </summary>
        public string? SubType { get; set; }
    }
}
