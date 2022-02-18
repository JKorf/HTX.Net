using System;
using System.Collections.Generic;
using System.Linq;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
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
        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The details of the trade
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<HuobiSymbolTradeDetails> Details { get; set; } = Array.Empty<HuobiSymbolTradeDetails>();
    }

    /// <summary>
    /// Symbol trade details
    /// </summary>
    public class HuobiSymbolTradeDetails
    {
        /// <summary>
        /// The id of the update
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonProperty("trade-id")]
        public long TradeId { get; set; }
        // Rest uses trade-id, socket uses tradeId
        [JsonProperty("tradeId")]
        private long TradeIdInternal { get => TradeId; set => TradeId = value; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The side of the trade
        /// </summary>
        [JsonProperty("direction"), JsonConverter(typeof(OrderSideConverter))]        
        public OrderSide Side { get; set; }
        /// <summary>
        /// The timestamp of the trade
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
