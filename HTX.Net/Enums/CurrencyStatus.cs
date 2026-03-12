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
        /// ["<c>allowed</c>"] Allowed
        /// </summary>
        [Map("allowed")]
        Allowed,
        /// <summary>
        /// ["<c>prohibited</c>"] Prohibited
        /// </summary>
        [Map("prohibited")]
        Prohibited
    }
}
