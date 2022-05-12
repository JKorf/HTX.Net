using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Repayment info
    /// </summary>
    public class HuobiRepayment
    {
        /// <summary>
        /// Repayment id
        /// </summary>
        public long RepayId { get; set; }
        /// <summary>
        /// Account id
        /// </summary>
        public long AccountId { get; set; }
        /// <summary>
        /// Repay time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime RepayTime { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Repay quantity
        /// </summary>
        [JsonProperty("repaidAmount")]
        public decimal RepaidQuantity { get; set; }
        /// <summary>
        /// Transactions
        /// </summary>
        [JsonProperty("transactIds")]
        public HuobiRepayTransaction Transactions { get; set; } = null!;
    }

    /// <summary>
    /// Repayment transaction
    /// </summary>
    public class HuobiRepayTransaction
    {
        /// <summary>
        /// Transact id
        /// </summary>
        public long TransactId { get; set; }
        /// <summary>
        /// Principal repaid
        /// </summary>
        public decimal RepaidPrincipal { get; set; }
        /// <summary>
        /// Interest repaid
        /// </summary>
        public decimal RepaidInterest { get; set; }
        /// <summary>
        /// HT paid
        /// </summary>
        public decimal PaidHt { get; set; }
        /// <summary>
        /// Points paid
        /// </summary>
        public decimal PaidPoint { get; set; }
    }
}
