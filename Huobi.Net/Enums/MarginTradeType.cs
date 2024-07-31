using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Trade type
    /// </summary>
    public enum MarginTradeType
    {
        /// <summary>
        /// All trades
        /// </summary>
        [Map("0")]
        All,
        /// <summary>
        /// Buy long
        /// </summary>
        [Map("1")]
        BuyLong,
        /// <summary>
        /// Sell short
        /// </summary>
        [Map("2")]
        SellShort,
        /// <summary>
        /// Buy short
        /// </summary>
        [Map("3")]
        BuyShort,
        /// <summary>
        /// Sell long
        /// </summary>
        [Map("4")]
        SellLong,
        /// <summary>
        /// Liquidate long positions
        /// </summary>
        [Map("5")]
        LiquidateLong,
        /// <summary>
        /// Liquidate short positions
        /// </summary>
        [Map("6")]
        LiquidateShort,
        /// <summary>
        /// Buy one way
        /// </summary>
        [Map("17")]
        BuyOneWay,
        /// <summary>
        /// Sell one way
        /// </summary>
        [Map("18")]
        SellOneWay
    }
}
