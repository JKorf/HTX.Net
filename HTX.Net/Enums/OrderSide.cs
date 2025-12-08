using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// Buy
        /// </summary>
        [Map("buy", "buy-market", "buy-limit", "buy-ioc", "buy-limit-maker", "buy-stop-limit", "buy-limit-fok", "buy-stop-limit-fok", "buy-limit-grid", "buy-market-grid")]
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
        [Map("sell", "sell-market", "sell-limit", "sell-ioc", "sell-limit-maker", "sell-stop-limit", "sell-limit-fok", "sell-stop-limit-fok", "sell-limit-grid", "sell-market-grid")]
        Sell
    }
}
