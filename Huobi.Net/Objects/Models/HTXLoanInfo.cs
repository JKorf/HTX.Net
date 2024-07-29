
using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Loan info
    /// </summary>
    public record HTXLoanInfo
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Currencies
        /// </summary>
        [JsonPropertyName("currencies")]
        public IEnumerable<HTXLoanInfoAsset> Assets { get; set; } = Array.Empty<HTXLoanInfoAsset>();
    }

    /// <summary>
    /// Asset info
    /// </summary>
    public record HTXLoanInfoAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Interest rate
        /// </summary>
        [JsonPropertyName("interest-rate")]
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Minimal loan quantity
        /// </summary>
        [JsonPropertyName("min-loan-amt")]
        public decimal MinLoanQuantity { get; set; }
        /// <summary>
        /// Maximal loan quantity
        /// </summary>
        [JsonPropertyName("max-loan-amt")]
        public decimal MaxLoanQuantity { get; set; }
        /// <summary>
        /// Remaining loanable quantity
        /// </summary>
        [JsonPropertyName("loanable-amt")]
        public decimal LoanableQuantity { get; set; }
        /// <summary>
        /// Actual interest rate
        /// </summary>
        [JsonPropertyName("actual-rate")]
        public decimal ActualRate { get; set; }
    }
}
