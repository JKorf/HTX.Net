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
        /// Take profit order
        /// </summary>
        [Map("tp")]
        TakeProfit,
        /// <summary>
        /// Stop loss order
        /// </summary>
        [Map("sl")]
        StopLoss
    }
}
