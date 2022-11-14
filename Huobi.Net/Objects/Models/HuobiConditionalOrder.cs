using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Conditional order info
    /// </summary>
    public class HuobiConditionalOrder
    {
        /// <summary>
        /// Acount id
        /// </summary>
        public long AccountId { get; set; }
        /// <summary>
        /// Source
        /// </summary>
        public string Source { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        public string? OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("orderPrice")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("orderSize")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonProperty("orderValue")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonProperty("orderSide")]
        [JsonConverter(typeof(EnumConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("orderType")]
        public ConditionalOrderType Type { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        public decimal StopPrice { get; set; }
        /// <summary>
        /// Trailing rate
        /// </summary>
        public decimal? TrailingRate { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("orderOrigTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonProperty("lastActTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("orderStatus")]
        public ConditionalOrderStatus Status { get; set; }
        /// <summary>
        /// Error code if the conditional order is rejected
        /// </summary>
        [JsonProperty("errCode")]
        public int? ErrorCode { get; set; }
        /// <summary>
        /// Error message if conditional order is rejected
        /// </summary>
        [JsonProperty("errMessage")]
        public string? ErrorMessage { get; set; }
    }
}
