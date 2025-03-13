using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Trigger type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerType>))]
    public enum TriggerType
    {
        /// <summary>
        /// Greater than or equal to price
        /// </summary>
        [Map("ge")]
        GreaterThanOrEqual,
        /// <summary>
        /// Lesser than or equal to price
        /// </summary>
        [Map("le")]
        LesserThanOrEqual
    }
}
