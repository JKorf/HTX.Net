using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Loan info
    /// </summary>
    public class HuobiLoanInfo
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Currencies
        /// </summary>
        [JsonProperty("currencies")]
        public IEnumerable<HuobiLoanInfoAsset> Assets { get; set; } = Array.Empty<HuobiLoanInfoAsset>();
    }

    /// <summary>
    /// Asset info
    /// </summary>
    public class HuobiLoanInfoAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Interest rate
        /// </summary>
        [JsonProperty("interest-rate")]
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Minimal loan quantity
        /// </summary>
        [JsonProperty("min-loan-amt")]
        public decimal MinLoanQuantity { get; set; }
        /// <summary>
        /// Maximal loan quantity
        /// </summary>
        [JsonProperty("max-loan-amt")]
        public decimal MaxLoanQuantity { get; set; }
        /// <summary>
        /// Remaining loanable quantity
        /// </summary>
        [JsonProperty("loanable-amt")]
        public decimal LoanableQuantity { get; set; }
        /// <summary>
        /// Actual interest rate
        /// </summary>
        [JsonProperty("actual-rate")]
        public decimal ActualRate { get; set; }
    }
}
