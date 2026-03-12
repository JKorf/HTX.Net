using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Trade type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginTradeType>))]
    public enum MarginTradeType
    {
        /// <summary>
        /// ["<c>0</c>"] All trades
        /// </summary>
        [Map("0")]
        All,
        /// <summary>
        /// ["<c>1</c>"] Buy long
        /// </summary>
        [Map("1")]
        BuyLong,
        /// <summary>
        /// ["<c>2</c>"] Sell short
        /// </summary>
        [Map("2")]
        SellShort,
        /// <summary>
        /// ["<c>3</c>"] Buy short
        /// </summary>
        [Map("3")]
        BuyShort,
        /// <summary>
        /// ["<c>4</c>"] Sell long
        /// </summary>
        [Map("4")]
        SellLong,
        /// <summary>
        /// ["<c>5</c>"] Liquidate long positions
        /// </summary>
        [Map("5")]
        LiquidateLong,
        /// <summary>
        /// ["<c>6</c>"] Liquidate short positions
        /// </summary>
        [Map("6")]
        LiquidateShort,
        /// <summary>
        /// ["<c>17</c>"] Buy one way
        /// </summary>
        [Map("17")]
        BuyOneWay,
        /// <summary>
        /// ["<c>18</c>"] Sell one way
        /// </summary>
        [Map("18")]
        SellOneWay
    }
}
