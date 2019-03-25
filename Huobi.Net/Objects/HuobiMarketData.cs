using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    public class HuobiMarketData
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

    public class HuobiMarketKline : HuobiMarketData
    {
        /// <summary>
        /// The start time of the kline
        /// </summary>
        [JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime Id { get; set; }
    }

    public class HuobiMarketDetails : HuobiMarketData
    {
        /// <summary>
        /// The id of the details
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The timestamp of the data
        /// </summary>
        public DateTime Timestamp { get; set; }
    }

    public class HuobiMarketTick : HuobiMarketData
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; }
    }
}
