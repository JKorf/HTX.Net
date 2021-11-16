using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi account info
    /// </summary>
    public class HuobiAccount
    {
        /// <summary>
        /// The id of the account
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The state of the account
        /// </summary>
        [JsonConverter(typeof(AccountStateConverter))]
        public AccountState State { get; set; }
        /// <summary>
        /// The type of the account
        /// </summary>
        [JsonConverter(typeof(AccountTypeConverter))]
        public AccountType Type { get; set; }
        /// <summary>
        /// Sub state
        /// </summary>
        public string? SubType { get; set; }
    }
}
