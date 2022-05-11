using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Margin account balance
    /// </summary>
    public class HuobiMarginBalances
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        public string? Symbol { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; } = string.Empty;
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonProperty("risk-rate")]
        public decimal RiskRate { get; set; }
        /// <summary>
        /// Account balance sum
        /// </summary>
        [JsonProperty("acct-balance-sum")]
        public decimal? AccountBalanceSum { get; set; }
        /// <summary>
        /// Debt balance sum
        /// </summary>
        [JsonProperty("debt-balance-sum")]
        public decimal? DebtBalanceSum { get; set; }
        /// <summary>
        /// The price which margin closeout was triggered
        /// </summary>
        [JsonProperty("fl-price")]
        public decimal? FlPrice { get; set; }
        /// <summary>
        /// Fl type
        /// </summary>
        [JsonProperty("fl-type")]
        public string? FlType { get; set; } = string.Empty;
        /// <summary>
        /// Account details
        /// </summary>
        public IEnumerable<HuobiIsolatedBalance> List { get; set; } = Array.Empty<HuobiIsolatedBalance>();
    }

    /// <summary>
    /// Balance info
    /// </summary>
    public class HuobiIsolatedBalance
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Balance type
        /// </summary>
        [JsonConverter(typeof(BalanceTypeConverter))]
        public BalanceType Type { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
    }
}
