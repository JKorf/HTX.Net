using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Converters;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Ledger entry
    /// </summary>
    public class HuobiLedgerEntry
    {
        /// <summary>
        /// Account id
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; } = "";
        /// <summary>
        /// Amount of the transaction
        /// </summary>
        [JsonProperty("transactAmt")]
        public decimal TransactionAmount { get; set; }
        /// <summary>
        /// Type of transaction
        /// </summary>
        public HuobiTransactionType TransactionType { get; set; }

        /// <summary>
        /// Type of transfer
        /// </summary>
        public string TransferType { get; set; } = "";
        /// <summary>
        /// Transaction id
        /// </summary>
        public long TransactionId { get; set; }
        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// Transferer
        /// </summary>
        public long Transferer { get; set; }
        /// <summary>
        /// Transferee
        /// </summary>
        public long Transferee { get; set; }
    }
}
