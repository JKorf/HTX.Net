﻿using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

using HTX.Net.Enums;


namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Symbol trade
    /// </summary>
    public record HTXSymbolTrade
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The timestamp of the trade
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The details of the trade
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<HTXSymbolTradeDetails> Details { get; set; } = Array.Empty<HTXSymbolTradeDetails>();
    }

    /// <summary>
    /// Symbol trade details
    /// </summary>
    public record HTXSymbolTradeDetails
    {
        /// <summary>
        /// The id of the update
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonPropertyName("trade-id")]
        public long TradeId { get; set; }
        // Rest uses trade-id, socket uses tradeId
        [JsonPropertyName("tradeId")]
        private long TradeIdInternal { get => TradeId; set => TradeId = value; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The side of the trade
        /// </summary>
        [JsonPropertyName("direction"), JsonConverter(typeof(EnumConverter))]        
        public OrderSide Side { get; set; }
        /// <summary>
        /// The timestamp of the trade
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}