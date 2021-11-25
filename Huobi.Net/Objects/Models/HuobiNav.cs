using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// NAV info for ETP
    /// </summary>
    public class HuobiNav
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Latest Nav
        /// </summary>
        public decimal Nav { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime NavTime { get; set; }
        /// <summary>
        /// Outstanding shares
        /// </summary>
        public decimal Outstanding { get; set; }
        /// <summary>
        /// Baskets
        /// </summary>
        public IEnumerable<HuobiBasket> Basket { get; set; } = Array.Empty<HuobiBasket>();
        /// <summary>
        /// Actual leverage ratio
        /// </summary>
        public decimal ActualLeverage { get; set; }
    }
    
    /// <summary>
    /// Basket
    /// </summary>
    public class HuobiBasket
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
    }
}
