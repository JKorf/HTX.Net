using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Margin order purpose
    /// </summary>
    public enum MarginPurpose
    {
        /// <summary>
        /// Loan
        /// </summary>
        [Map("1")]
        AutomaticLoan,
        /// <summary>
        /// Repayment
        /// </summary>
        [Map("2")]
        AutomaticRepayment
    }
}
