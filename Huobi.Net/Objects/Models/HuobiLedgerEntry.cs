using System;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
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
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity of the transaction
        /// </summary>
        [JsonProperty("transactAmt")]
        public decimal TransactionQuantity { get; set; }
        /// <summary>
        /// Type of transaction
        /// </summary>
        [JsonProperty("transactType")]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Type of transfer
        /// </summary>
        public string TransferType { get; set; } = string.Empty;
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("transactId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("transactTime")]
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
