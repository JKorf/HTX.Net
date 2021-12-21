using System;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Symbol data
    /// </summary>
    public class HuobiSymbolData
    {
        /// <summary>
        /// The highest price
        /// </summary>
        [JsonProperty("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// The lowest price
        /// </summary>
        [JsonProperty("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// The price at the opening
        /// </summary>
        [JsonProperty("open")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// The last price
        /// </summary>
        [JsonProperty("close")]
        public decimal? ClosePrice { get; set; }
        /// <summary>
        /// The volume in base asset
        /// </summary>
        [JsonProperty("amount")]
        public decimal? Volume { get; set; }
        /// <summary>
        /// The volume in quote asset (quantity * price)
        /// </summary>
        [JsonProperty("vol")]
        public decimal? QuoteVolume { get; set; }
        /// <summary>
        /// The number of trades
        /// </summary>
        [JsonProperty("count")]
        public int? TradeCount { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public long? Version { get; set; }
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
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("id")]
        public DateTime OpenTime { get; set; }

        decimal ICommonKline.CommonHighPrice => HighPrice ?? 0;
        decimal ICommonKline.CommonLowPrice => LowPrice ?? 0;
        decimal ICommonKline.CommonOpenPrice => OpenPrice ?? 0;
        decimal ICommonKline.CommonClosePrice => ClosePrice ?? 0;
        DateTime ICommonKline.CommonOpenTime => OpenTime;
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
        /// Quantity of the best bid
        /// </summary>
        [JsonProperty("bidSize")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Quantity of the best ask
        /// </summary>
        [JsonProperty("askSize")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonProperty("bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonProperty("ask")]
        public decimal BestAskPrice { get; set; }

        string ICommonTicker.CommonSymbol => Symbol;
        decimal ICommonTicker.CommonHighPrice => HighPrice ?? 0;
        decimal ICommonTicker.CommonLowPrice => LowPrice ?? 0;
        decimal ICommonTicker.CommonVolume => Volume ?? 0;
    }
}
