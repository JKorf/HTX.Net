using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Offset
    /// </summary>
    [JsonConverter(typeof(EnumConverter<Offset>))]
    public enum Offset
    {
        /// <summary>
        /// Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Close
        /// </summary>
        [Map("close")]
        Close,
        /// <summary>
        /// Both
        /// </summary>
        [Map("both")]
        Both
    }
}
