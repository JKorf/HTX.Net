using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Symbol trade
    /// </summary>
    public class HuobiSymbolTrade
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The timestamp of the trade
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The details of the trade
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<HuobiSymbolTradeDetails> Details { get; set; } = new List<HuobiSymbolTradeDetails>();
    }

    /// <summary>
    /// Symbol trade details
    /// </summary>
    public class HuobiSymbolTradeDetails
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        public string Id { get; set; } = "";
        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The amount of the trade
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The side of the trade
        /// </summary>
        [JsonProperty("direction"), JsonConverter(typeof(OrderSideConverter))]        
        public HuobiOrderSide Side { get; set; }
        /// <summary>
        /// The timestamp of the trade
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
