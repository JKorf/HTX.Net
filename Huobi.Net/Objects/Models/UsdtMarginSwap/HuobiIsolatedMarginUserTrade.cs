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
    public class HuobiIsolatedMarginUserTradePage : HuobiPage
    {
        /// <summary>
        /// Trades
        /// </summary>
        public IEnumerable<HuobiIsolatedMarginUserTrade> Trades { get; set; } = Array.Empty<HuobiIsolatedMarginUserTrade>();
    }

    /// <summary>
    /// User trade info
    /// </summary>
    public class HuobiIsolatedMarginUserTrade : HuobiMarginTrade
    {
        /// <summary>
        /// Match id
        /// </summary>
        [JsonProperty("match_id")]
        public long MatchId { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Side
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public Offset Offset { get; set; }
        /// <summary>
        /// Offset profit loss
        /// </summary>
        [JsonProperty("offset_profitloss")]
        public decimal OffsetProfitloss { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonProperty("reduce_only")]
        public bool ReduceOnly { get; set; }
    }
}
