using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Order role
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderRole>))]
    public enum OrderRole
    {
        /// <summary>
        /// Maker of an order book entry
        /// </summary>
        [Map("maker")]
        Maker,
        /// <summary>
        /// Taker of an order book entry
        /// </summary>
        [Map("taker")]
        Taker
    }
}
