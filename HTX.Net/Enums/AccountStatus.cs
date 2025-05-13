using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Account state
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountStatus>))]
    public enum AccountStatus
    {
        /// <summary>
        /// Working
        /// </summary>
        [Map("working", "normal")]
        Working,
        /// <summary>
        /// Locked
        /// </summary>
        [Map("lock")]
        Locked
    }
}
