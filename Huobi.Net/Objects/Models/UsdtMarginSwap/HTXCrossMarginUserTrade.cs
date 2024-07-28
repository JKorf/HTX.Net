using CryptoExchange.Net.Converters;
using HTX.Net.Enums;

using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Margin user trade page
    /// </summary>
    public record HTXCrossMarginUserTradePage : HTXPage
    {
        /// <summary>
        /// Trades
        /// </summary>
        public IEnumerable<HTXCrossMarginUserTrade> Trades { get; set; } = Array.Empty<HTXCrossMarginUserTrade>();
    }

    /// <summary>
    /// User trade info
    /// </summary>
    public record HTXCrossMarginUserTrade : HTXIsolatedMarginUserTrade
    {
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
