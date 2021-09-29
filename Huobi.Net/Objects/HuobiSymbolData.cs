using System;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
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
        [JsonProperty("amount")]
        public decimal? Quantity { get; set; }
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
    /// Ticker data
    /// </summary>
    public class HuobiSymbolTicker : HuobiSymbolData
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
    }

    /// <summary>
    /// Symbol kline data
    /// </summary>
    public class HuobiKline : HuobiSymbolData, ICommonKline
    {
        /// <summary>
        /// The start time of the kline
        /// </summary>
        [JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime Id { get; set; }

        decimal ICommonKline.CommonHigh => High ?? 0;
        decimal ICommonKline.CommonLow => Low ?? 0;
        decimal ICommonKline.CommonOpen => Open ?? 0;
        decimal ICommonKline.CommonClose => Close ?? 0;
        DateTime ICommonKline.CommonOpenTime => Id;
        decimal ICommonKline.CommonVolume => Volume ?? 0;
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
    public class HuobiSymbolTick : HuobiSymbolData, ICommonTicker
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Size of the best bid
        /// </summary>
        public decimal BidSize { get; set; }
        /// <summary>
        /// Size of the best ask
        /// </summary>
        public decimal AskSize { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        public decimal Bid { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        public decimal Ask { get; set; }

        string ICommonTicker.CommonSymbol => Symbol;
        decimal ICommonTicker.CommonHigh => High ?? 0;
        decimal ICommonTicker.CommonLow => Low ?? 0;
        decimal ICommonTicker.CommonVolume => Volume ?? 0;
    }
}
