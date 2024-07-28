using CryptoExchange.Net.Converters;
using HTX.Net.Enums;

using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Trade info
    /// </summary>
    public record HTXUsdtMarginSwapTradesUpdate
    {
        /// <summary>
        /// Update id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Update timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Trades
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<HTXUsdtMarginSwapTradeUpdate> Trades { get; set; } = Array.Empty<HTXUsdtMarginSwapTradeUpdate>();
    }

    /// <summary>
    /// Trade info
    /// </summary>
    public record HTXUsdtMarginSwapTradeUpdate
    {
        /// <summary>
        /// Amount of trades
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
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
        [JsonPropertyName("direction")]
        [JsonConverter(typeof(EnumConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal TradeTurnover { get; set; }
    }
}
