using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Network action status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<NetworkStatus>))]
    public enum NetworkStatus
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
