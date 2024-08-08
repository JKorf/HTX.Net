using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Status
    /// </summary>
    public enum CurrencyStatus
    {
        /// <summary>
        /// Allowed
        /// </summary>
        [Map("allowed")]
        Allowed,
        /// <summary>
        /// Prohibited
        /// </summary>
        [Map("prohibited")]
        Prohibited
    }
}
