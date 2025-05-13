using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Liquidation type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LiquidationType>))]
    public enum LiquidationType
    {
        /// <summary>
        /// Not a liquidation
        /// </summary>
        [Map("0")]
        NonLiquidated,
        /// <summary>
        /// Long and short netting
        /// </summary>
        [Map("1")]
        LongAndShortNetting,
        /// <summary>
        /// Partial liquidation
        /// </summary>
        [Map("2")]
        PartialLiquidated,
        /// <summary>
        /// Full liquidation
        /// </summary>
        [Map("3")]
        FullLiquidated
    }
}
