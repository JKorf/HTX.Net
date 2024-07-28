using CryptoExchange.Net.Converters;

using System;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Repayment result
    /// </summary>
    public record HTXRepaymentResult
    {
        /// <summary>
        /// Repayment id
        /// </summary>
        public string RepayId { get; set; } = string.Empty;
        /// <summary>
        /// Repay time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime RepayTime { get; set; }
    }
}
