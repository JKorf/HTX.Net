using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Models.Socket
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class HuobiUsdtMarginSwapTradesUpdate
    {
        /// <summary>
        /// Update id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Update timestamp
        /// </summary>
        [JsonProperty("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Trades
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<HuobiUsdtMarginSwapTradeUpdate> Trades { get; set; } = Array.Empty<HuobiUsdtMarginSwapTradeUpdate>();
    }

    /// <summary>
    /// Trade info
    /// </summary>
    public class HuobiUsdtMarginSwapTradeUpdate
    {
        /// <summary>
        /// Amount of trades
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonProperty("direction")]
        [JsonConverter(typeof(EnumConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Turnover
        /// </summary>
        [JsonProperty("trade_turnover")]
        public decimal TradeTurnover { get; set; }
    }
}
