using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
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
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
