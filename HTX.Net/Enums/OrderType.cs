using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// ["<c>limit</c>"] Limit
        /// </summary>
        [Map("limit", "buy-limit", "sell-limit")]
        Limit,
        /// <summary>
        /// ["<c>market</c>"] Market
        /// </summary>
        [Map("market", "buy-market", "sell-market")]
        Market,
        /// <summary>
        /// ["<c>ioc</c>"] Immediate or cancel
        /// </summary>
        [Map("ioc", "buy-ioc", "sell-ioc")]
        IOC,
        /// <summary>
        /// ["<c>limit-maker</c>"] Limit maker
        /// </summary>
        [Map("limit-maker", "buy-limit-maker", "sell-limit-maker")]
        LimitMaker,
        /// <summary>
        /// ["<c>stop-limit</c>"] Stop limit
        /// </summary>
        [Map("stop-limit", "buy-stop-limit", "sell-stop-limit")]
        StopLimit,
        /// <summary>
        /// ["<c>limit-fok</c>"] Fill or kill limit
        /// </summary>
        [Map("limit-fok", "buy-limit-fok", "sell-limit-fok")]
        FillOrKillLimit,
        /// <summary>
        /// ["<c>stop-limit-fok</c>"] Fill or kill stop limit
        /// </summary>
        [Map("stop-limit-fok", "buy-stop-limit-fok", "sell-stop-limit-fok")]
        FillOrKillStopLimit,
        /// <summary>
        /// ["<c>buy-market-grid</c>"] Grid market order
        /// </summary>
        [Map("buy-market-grid", "sell-market-grid")]
        MarketGrid,
        /// <summary>
        /// ["<c>buy-limit-grid</c>"] Grid limit order
        /// </summary>
        [Map("buy-limit-grid", "sell-limit-grid")]
        LimitGrid,
    }
}
