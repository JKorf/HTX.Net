using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Trade type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LiquidationTradeType>))]
    public enum LiquidationTradeType
    {
        /// <summary>
        /// Fully filled liquidation orders
        /// </summary>
        [Map("0")]
        FullyFilledLiquidationOrders,
        /// <summary>
        /// Liquidated close orders
        /// </summary>
        [Map("5")]
        LiquidatedCloseOrders,
        /// <summary>
        /// Liquidated open orders
        /// </summary>
        [Map("6")]
        LiquidatedOpenOrders
    }
}
