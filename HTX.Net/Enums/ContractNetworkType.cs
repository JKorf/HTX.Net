using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Contract network type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractNetworkType>))]
    public enum ContractNetworkType
    {
        /// <summary>
        /// Coin
        /// </summary>
        [Map("0")]
        Coin,
        /// <summary>
        /// Token
        /// </summary>
        [Map("1")]
        Token
    }
}
