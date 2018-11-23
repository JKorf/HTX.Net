using Huobi.Net.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Huobi.Net.Objects.SocketObjects
{
    public class HuobiAccountEvent
    {
        /// <summary>
        /// The event type that triggered the update
        /// </summary>
        [JsonConverter(typeof(AccountEventConverter))]
        public HuobiAccountEventType Event { get; set; }
        /// <summary>
        /// List of changes
        /// </summary>
        [JsonProperty("list")]
        public List<HuobiBalanceChange> BalanceChanges { get; set; }
    }

    public class HuobiBalanceChange
    {
        /// <summary>
        /// The id of the account
        /// </summary>
        [JsonProperty("account-id")]
        public long AccountId { get; set; }
        /// <summary>
        /// The currency that changed
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// The type of the balance that changed
        /// </summary>
        public HuobiBalanceType Type { get; set; }
        /// <summary>
        /// The new amount
        /// </summary>
        public decimal Balance { get; set; }
    }
}
