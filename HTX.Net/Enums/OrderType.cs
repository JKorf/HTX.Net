using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Limit
        /// </summary>
        [Map("limit", "buy-limit", "sell-limit")]
        Limit,
        /// <summary>
        /// Market
        /// </summary>
        [Map("market", "buy-market", "sell-market")]
        Market,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("ioc", "buy-ioc", "sell-ioc")]
        IOC,
        /// <summary>
        /// Limit maker
        /// </summary>
        [Map("limit-maker", "buy-limit-maker", "sell-limit-maker")]
        LimitMaker,
        /// <summary>
        /// Stop limit
        /// </summary>
        [Map("stop-limit", "buy-stop-limit", "sell-stop-limit")]
        StopLimit,
        /// <summary>
        /// Fill or kill limit
        /// </summary>
        [Map("limit-fok", "buy-limit-fok", "sell-limit-fok")]
        FillOrKillLimit,
        /// <summary>
        /// Fill or kill stop limit
        /// </summary>
        [Map("stop-limit-fok", "buy-stop-limit-fok", "sell-stop-limit-fok")]
        FillOrKillStopLimit,
        /// <summary>
        /// Grid market order
        /// </summary>
        [Map("buy-market-grid", "sell-market-grid")]
        MarketGrid,
        /// <summary>
        /// Grid limit order
        /// </summary>
        [Map("buy-limit-grid", "sell-limit-grid")]
        LimitGrid,
    }
}
