using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Repayment result
    /// </summary>
    public class HuobiRepaymentResult
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
