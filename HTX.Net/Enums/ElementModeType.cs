using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Margin mode type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ElementModeType>))]
    public enum ElementModeType
    {
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("1")]
        IsolatedMargin,
        /// <summary>
        /// Cross and isolated margin
        /// </summary>
        [Map("2")]
        CrossAndIsolatedMargin,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("3")]
        CrossMargin
    }
}
