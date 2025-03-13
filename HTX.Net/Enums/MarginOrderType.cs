using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Margin order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginOrderType>))]
    public enum MarginOrderType
    {
        /// <summary>
        /// Quoatation
        /// </summary>
        [Map("1")]
        Quatation,
        /// <summary>
        /// Canceled order
        /// </summary>
        [Map("2")]
        CanceledOrder,
        /// <summary>
        /// Forced liquidation
        /// </summary>
        [Map("3")]
        ForcedLiquidation,
        /// <summary>
        /// Delivery
        /// </summary>
        [Map("4")]
        DeliveryOrder
    }
}
