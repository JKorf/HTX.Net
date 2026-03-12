using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Balance type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BalanceType>))]
    public enum BalanceType
    {
        /// <summary>
        /// ["<c>trade</c>"] Trade balance
        /// </summary>
        [Map("trade")]
        Trade,
        /// <summary>
        /// ["<c>frozen</c>"] Frozen balance
        /// </summary>
        [Map("frozen")]
        Frozen,
        /// <summary>
        /// ["<c>loan</c>"] Loan balance
        /// </summary>
        [Map("loan")]
        Loan,
        /// <summary>
        /// ["<c>lock</c>"] Locked balance
        /// </summary>
        [Map("lock")]
        Lock,
        /// <summary>
        /// ["<c>bank</c>"] Bank balance
        /// </summary>
        [Map("bank")]
        Bank,
        /// <summary>
        /// ["<c>interest</c>"] Interest balance
        /// </summary>
        [Map("interest")]
        Interest,
        /// <summary>
        /// ["<c>credit-repay</c>"] Credit repay
        /// </summary>
        [Map("credit-repay")]
        CreditRepay,
        /// <summary>
        /// ["<c>trust-asset</c>"] Trust asset
        /// </summary>
        [Map("trust-asset")]
        TrustAsset,
        /// <summary>
        /// ["<c>transfer-out-available</c>"] Transfer out available
        /// </summary>
        [Map("transfer-out-available")]
        TransferOutAvailable,
        /// <summary>
        /// ["<c>loan-available</c>"] Loan available
        /// </summary>
        [Map("loan-available")]
        LoanAvailable,
        /// <summary>
        /// ["<c>lending</c>"] Lending
        /// </summary>
        [Map("lending")]
        Lending
    }
}
