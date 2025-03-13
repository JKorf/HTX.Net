using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;
namespace HTX.Net.Enums
{
    /// <summary>
    /// Status of an instrument
    /// </summary>
    [JsonConverter(typeof(EnumConverter<InstrumentStatus>))]
    public enum InstrumentStatus
    {
        /// <summary>
        /// Normal
        /// </summary>
        Normal,
        /// <summary>
        /// Delisted
        /// </summary>
        Delisted
    }
}
