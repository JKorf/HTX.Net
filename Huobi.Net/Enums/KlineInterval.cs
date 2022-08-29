using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Interval for klines, int value represent the time in seconds
    /// </summary>
    public enum KlineInterval
    {
        /// <summary>
        /// 1m
        /// </summary>
        [Map("1min")]
        OneMinute = 60,
        /// <summary>
        /// 5m
        /// </summary>
        [Map("5min")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// 15m
        /// </summary>
        [Map("15min")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// 30m
        /// </summary>
        [Map("30min")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// 1h
        /// </summary>
        [Map("60min")]
        OneHour = 60 * 60,
        /// <summary>
        /// 4h
        /// </summary>
        [Map("4hour")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// 1d
        /// </summary>
        [Map("1day")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// 1w
        /// </summary>
        [Map("1week")]
        OneWeek = 60 * 60 * 24 * 7,
        /// <summary>
        /// 1m
        /// </summary>
        [Map("1mon")]
        OneMonth = 60 * 60 * 24 * 30,
        /// <summary>
        /// 1y
        /// </summary>
        [Map("1year")]
        OneYear = 60 * 60 * 24 * 365
    }
}
