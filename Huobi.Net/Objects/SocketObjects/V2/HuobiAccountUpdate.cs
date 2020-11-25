using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.SocketObjects.V2
{
    /// <summary>
    /// Account update
    /// </summary>
    public class HuobiAccountUpdate
    {
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; } = "";
        /// <summary>
        /// Account id
        /// </summary>
        public long AccountId { get; set; }
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonOptionalProperty]
        public decimal? Balance { get; set; }
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonOptionalProperty]
        public decimal? Available { get; set; }
        /// <summary>
        /// Type of change
        /// </summary>
        [JsonConverter(typeof(AccountEventConverter))]
        public HuobiAccountEventType? ChangeType { get; set; }
        /// <summary>
        /// Account type
        /// </summary>
        [JsonConverter(typeof(BalanceTypeConverter))]
        public HuobiBalanceType AccountType { get; set; }
        /// <summary>
        /// Change time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime? ChangeTime { get; set; }
    }
}
