using System;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
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
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Quantity change (positive value if income, negative value if outcome)	
        /// </summary>
        [JsonProperty("transact-amt")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Blance change types
        /// </summary>
        [JsonProperty("transact-type"), JsonConverter(typeof(TransactionTypeConverter))]
        public TransactionType Type { get; set; }

        /// <summary>
        /// Available balance
        /// </summary>
        [JsonProperty("avail-balance")]
        public decimal Available { get; set; }
		
        /// <summary>
        /// Account balance
        /// </summary>
        [JsonProperty("acct-balance")]
        public decimal Total { get; set; }
		
        /// <summary>
        /// Transaction time (database time)
        /// </summary>
        [JsonProperty("transact-time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Unique record ID in the database
        /// </summary>
        [JsonProperty("record-id")]
        public string RecordId { get; set; } = string.Empty;
    }
}