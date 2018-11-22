using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.SocketObjects
{
    public class HuobiAccountEvent
    {
        public string Event { get; set; }
        [JsonProperty("list")]
        public List<HuobiBalanceChange> BalanceChanges { get; set; }
    }

    public class HuobiBalanceChange
    {
        [JsonProperty("account-id")]
        public long AccountId { get; set; }
        public string Currency { get; set; }
        public HuobiBalanceType Type { get; set; }
        public decimal Balance { get; set; }
    }
}
