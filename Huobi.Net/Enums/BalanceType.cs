using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Balance type
    /// </summary>
    public enum BalanceType
    {
        /// <summary>
        /// Trade balance
        /// </summary>
        [Map("trade")]
        Trade,
        /// <summary>
        /// Frozen balance
        /// </summary>
        [Map("frozen")]
        Frozen,
        /// <summary>
        /// Loan balance
        /// </summary>
        [Map("loan")]
        Loan,
        /// <summary>
        /// Interest balance
        /// </summary>
        [Map("interest")]
        Interest,
        /// <summary>
        /// Transfer out available
        /// </summary>
        [Map("transfer-out-available")]
        TransferOutAvailable,
        /// <summary>
        /// Loan available
        /// </summary>
        [Map("loan-available")]
        LoanAvailable
    }
}
