using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Info on a symbol's best offer
    /// </summary>
    public class HuobiBestOffer
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// Time of the data
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime QuoteTime { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        public decimal Bid { get; set; }
        /// <summary>
        /// Size of the best bid
        /// </summary>
        public decimal BidSize { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        public decimal Ask { get; set; }
        /// <summary>
        /// Size of the best ask
        /// </summary>
        public decimal AskSize { get; set; }
        /// <summary>
        /// Sequence number
        /// </summary>
        [JsonProperty("seqId")]
        public long Sequence { get; set; }
    }
}
