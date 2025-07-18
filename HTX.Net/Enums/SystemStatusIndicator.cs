using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// System status indicator
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SystemStatusIndicator>))]
    public enum SystemStatusIndicator
    {
        /// <summary>
        /// None
        /// </summary>
        [Map("none")]
        None,
        /// <summary>
        /// Minor
        /// </summary>
        [Map("minor")]
        Minor,
        /// <summary>
        /// Major
        /// </summary>
        [Map("major")]
        Major,
        /// <summary>
        /// Critical
        /// </summary>
        [Map("critical")]
        Critical,
        /// <summary>
        /// Maintenance
        /// </summary>
        [Map("maintenance")]
        Maintenance
    }
}
