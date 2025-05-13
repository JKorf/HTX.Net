using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Locked balance
        /// </summary>
        [Map("lock")]
        Lock,
        /// <summary>
        /// Bank balance
        /// </summary>
        [Map("bank")]
        Bank,
        /// <summary>
        /// Interest balance
        /// </summary>
        [Map("interest")]
        Interest,
        /// <summary>
        /// Credit repay
        /// </summary>
        [Map("credit-repay")]
        CreditRepay,
        /// <summary>
        /// Trust asset
        /// </summary>
        [Map("trust-asset")]
        TrustAsset,
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
