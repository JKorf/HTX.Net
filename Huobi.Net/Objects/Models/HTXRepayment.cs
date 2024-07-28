using CryptoExchange.Net.Converters;

using System;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Repayment info
    /// </summary>
    public record HTXRepayment
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
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Repay quantity
        /// </summary>
        [JsonPropertyName("repaidAmount")]
        public decimal RepaidQuantity { get; set; }
        /// <summary>
        /// Transactions
        /// </summary>
        [JsonPropertyName("transactIds")]
        public HTXRepayTransaction Transactions { get; set; } = null!;
    }

    /// <summary>
    /// Repayment transaction
    /// </summary>
    public record HTXRepayTransaction
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
