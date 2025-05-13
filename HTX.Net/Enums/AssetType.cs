using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Asset type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetType>))]
    public enum AssetType
    {
        /// <summary>
        /// Virtual asset
        /// </summary>
        [Map("1")]
        Virtual,
        /// <summary>
        /// Fiat asset
        /// </summary>
        [Map("2")]
        Fiat
    }
}
