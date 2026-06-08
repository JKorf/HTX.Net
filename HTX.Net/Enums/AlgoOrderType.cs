using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Algo order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AlgoOrderType>))]
    public enum AlgoOrderType
    {
        /// <summary>
        /// ["<c>trigger</c>"] Trigger order
        /// </summary>
        [Map("trigger")]
        Trigger,
        /// <summary>
        /// ["<c>tpsl</c>"] Take profit or stop loss order
        /// </summary>
        [Map("tpsl")]
        TakeProfitStopLoss,
        /// <summary>
        /// ["<c>trailing</c>"] Trailing order
        /// </summary>
        [Map("trailing")]
        Trailing
    }
}
