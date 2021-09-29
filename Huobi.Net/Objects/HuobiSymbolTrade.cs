using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Symbol trade
    /// </summary>
    public class HuobiSymbolTrade: ICommonRecentTrade
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
        public IEnumerable<HuobiSymbolTradeDetails> Details { get; set; } = Array.Empty<HuobiSymbolTradeDetails>();

        decimal ICommonRecentTrade.CommonPrice => Details.First().Price;
        decimal ICommonRecentTrade.CommonQuantity => Details.First().Quantity;
        DateTime ICommonRecentTrade.CommonTradeTime => Details.First().Timestamp;
    }

    /// <summary>
    /// Symbol trade details
    /// </summary>
    public class HuobiSymbolTradeDetails
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The amount of the trade
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
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
