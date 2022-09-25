using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Margin user trade page
    /// </summary>
    public class HuobiCrossMarginUserTradePage : HuobiPage
    {
        /// <summary>
        /// Trades
        /// </summary>
        public IEnumerable<HuobiCrossMarginUserTrade> Trades { get; set; } = Array.Empty<HuobiCrossMarginUserTrade>();
    }

    /// <summary>
    /// User trade info
    /// </summary>
    public class HuobiCrossMarginUserTrade : HuobiIsolatedMarginUserTrade
    {
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonProperty("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
