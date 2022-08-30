using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Trade type
    /// </summary>
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
