using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
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
