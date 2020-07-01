using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Symbol data
    /// </summary>
    public class HuobiSymbolData
    {
        /// <summary>
        /// The highest price
        /// </summary>
        public decimal? High { get; set; }
        /// <summary>
        /// The lowest price
        /// </summary>
        public decimal? Low { get; set; }
        /// <summary>
        /// The price at the opening
        /// </summary>
        public decimal? Open { get; set; }
        /// <summary>
        /// The last price
        /// </summary>
        public decimal? Close { get; set; }
        /// <summary>
        /// The amount of the symbol trades
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// The volume of the symbol trades (amount * price)
        /// </summary>
        [JsonProperty("vol")]
        public decimal? Volume { get; set; }
        /// <summary>
        /// The number of trades
        /// </summary>
        [JsonProperty("count")]
        public int? TradeCount { get; set; }
    }

    /// <summary>
    /// Symbol kline data
    /// </summary>
    public class HuobiKline : HuobiSymbolData
    {
        /// <summary>
        /// The start time of the kline
        /// </summary>
        [JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime Id { get; set; }
    }

    /// <summary>
    /// Symbol details
    /// </summary>
    public class HuobiSymbolDetails : HuobiSymbolData
    {
        /// <summary>
        /// The id of the details
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Timestamp of the data
        /// </summary>
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Symbol tick
    /// </summary>
    public class HuobiSymbolTick : HuobiSymbolData
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = "";
    }
}
