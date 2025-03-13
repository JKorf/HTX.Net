using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// User status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UserStatus>))]
    public enum UserStatus
    {
        /// <summary>
        /// Normal
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// Locked
        /// </summary>
        [Map("lock")]
        Locked
    }
}
