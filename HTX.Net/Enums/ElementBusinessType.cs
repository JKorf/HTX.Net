using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Business type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ElementBusinessType>))]
    public enum ElementBusinessType
    {
        /// <summary>
        /// Perpetual futures
        /// </summary>
        [Map("1")]
        PerpetualFutures,
        /// <summary>
        /// Delivery futures
        /// </summary>
        [Map("2")]
        DeliveryFutures,
        /// <summary>
        /// Perpetual + Delivery futures
        /// </summary>
        [Map("3")]
        PerpetualAndDeliveryFutures
    }
}
