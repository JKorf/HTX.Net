using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// GetNetworksAsync filter
    /// </summary>
    [JsonConverter(typeof(EnumConverter<NetworkRequestFilter>))]
    public enum NetworkRequestFilter
    {
        /// <summary>
        /// Do not return descriptions
        /// </summary>
        [Map("0")]
        NoDescriptions,
        /// <summary>
        /// Include all descriptions
        /// </summary>
        [Map("1")]
        AllDescriptions,
        /// <summary>
        /// Only include suspended withdrawal/deposit descriptions
        /// </summary>
        [Map("2")]
        SuspendedDescriptions
    }
}
