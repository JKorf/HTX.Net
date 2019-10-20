using Huobi.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
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
        public HuobiAccountState State { get; set; }
        /// <summary>
        /// The type of the account
        /// </summary>
        [JsonConverter(typeof(AccountTypeConverter))]
        public HuobiAccountType Type { get; set; }
    }
}
