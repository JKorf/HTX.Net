using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class HuobiMarginTrade
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonProperty("trade_id")]
        public long TradeId { get; set; }
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonProperty("trade_price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Trade quantity
        /// </summary>
        [JsonProperty("trade_volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade value
        /// </summary>
        [JsonProperty("trade_turnover")]
        public decimal Value { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonProperty("trade_fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Trade role
        /// </summary>
        [JsonConverter(typeof(OrderRoleConverter))]
        public OrderRole Role { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonProperty("fee_asset")]
        public string FeeAssset { get; set; } = string.Empty;
        /// <summary>
        /// Profit
        /// </summary>
        public decimal Profit { get; set; }
        /// <summary>
        /// Real profit
        /// </summary>
        [JsonProperty("real_profit")]
        public decimal RealProfit { get; set; }
    }
}
