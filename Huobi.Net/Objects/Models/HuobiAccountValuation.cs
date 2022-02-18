using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Account valuation
    /// </summary>
    public class HuobiAccountValuation
    {
        /// <summary>
        /// The balance
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
