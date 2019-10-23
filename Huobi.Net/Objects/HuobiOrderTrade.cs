using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class HuobiOrderTrade
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The symbol of the trade
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// Time the trade was made
        /// </summary>
        [JsonProperty("created-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The amount that was filled in this trade
        /// </summary>
        [JsonProperty("filled-amount")]
        public decimal FilledAmount { get; set; }
        /// <summary>
        /// The fee paid for this trade
        /// </summary>
        [JsonProperty("filled-fees")]
        public decimal FilledFees { get; set; }
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonProperty("match-id")]
        public long MatchId { get; set; }
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("order-id")]
        public long OrderId { get; set; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The source of the trade
        /// </summary>
        public string Source { get; set; } = "";
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter))]
        public HuobiOrderType OrderType { get; set; }
    }
}
