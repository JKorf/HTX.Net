using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Status of the market
    /// </summary>
    public class HuobiMarketStatus
    {
        /// <summary>
        /// The status
        /// </summary>
        [JsonProperty("marketStatus")]
        public MarketStatus Status { get; set; }
        
        /// <summary>
        /// Start time of when market halted
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime? HaltStartTime { get; set; }
        /// <summary>
        /// Estimated end time of the halt
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime? HaltEndTime { get; set; }
        /// <summary>
        /// Reason for halting
        /// </summary>
        public string? HaltReason { get; set; }
        /// <summary>
        /// Affected symbols, comma separated or 'all' if all symbols are affected
        /// </summary>
        public string? AffectedSymbols { get; set; }
    }
}
