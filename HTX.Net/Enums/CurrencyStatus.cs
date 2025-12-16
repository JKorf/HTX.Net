using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<CurrencyStatus>))]
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
