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
        /// ["<c>0</c>"] Fully filled liquidation orders
        /// </summary>
        [Map("0")]
        FullyFilledLiquidationOrders,
        /// <summary>
        /// ["<c>5</c>"] Liquidated close orders
        /// </summary>
        [Map("5")]
        LiquidatedCloseOrders,
        /// <summary>
        /// ["<c>6</c>"] Liquidated open orders
        /// </summary>
        [Map("6")]
        LiquidatedOpenOrders
    }
}
