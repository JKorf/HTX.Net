using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Status of the market
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarketStatus>))]
    public enum MarketStatus
    {
        /// <summary>
        /// Operating normally
        /// </summary>
        [Map("1")]
        Normal = 1,
        /// <summary>
        /// Trading halted
        /// </summary>
        [Map("2")]
        Halted = 2,
        /// <summary>
        /// Only cancelation is possible
        /// </summary>
        [Map("3")]
        CancelOnly = 3
    }
}
