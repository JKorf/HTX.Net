using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Take profit / stop loss type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TpslOrderType>))]
    public enum TpslOrderType
    {
        /// <summary>
        /// ["<c>tp</c>"] Take profit order
        /// </summary>
        [Map("tp")]
        TakeProfit,
        /// <summary>
        /// ["<c>sl</c>"] Stop loss order
        /// </summary>
        [Map("sl")]
        StopLoss
    }
}
