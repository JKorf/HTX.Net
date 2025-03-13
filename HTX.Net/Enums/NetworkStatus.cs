using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
