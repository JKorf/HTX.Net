using System;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Account history data
    /// </summary>
    public class HuobiAccountHistory
    {
        /// <summary>
        /// Account ID
        /// </summary>
        [JsonProperty("account-id")]
        public long AccountId { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; } = "";
		
        /// <summary>
        /// Amount change (positive value if income, negative value if outcome)	
        /// </summary>
        [JsonProperty("transact-amt")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Amount change types
        /// </summary>
        [JsonProperty("transact-type"), JsonConverter(typeof(TransactionTypeConverter))]
        public HuobiTransactionType Type { get; set; }

        /// <summary>
        /// Available balance
        /// </summary>
        [JsonProperty("avail-balance")]
        public decimal AvailableBalance { get; set; }
		
        /// <summary>
        /// Account balance
        /// </summary>
        [JsonProperty("acct-balance")]
        public decimal AccountBalance { get; set; }
		
        /// <summary>
        /// Transaction time (database time)
        /// </summary>
        [JsonProperty("transact-time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }

        /// <summary>
        /// Unique record ID in the database
        /// </summary>
        [JsonProperty("record-id")]
        public string RecordId { get; set; } = "";
    }
}