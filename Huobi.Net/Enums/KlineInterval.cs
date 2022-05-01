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
        OneMinute = 60,
        /// <summary>
        /// 5m
        /// </summary>
        FiveMinutes = 60 * 5,
        /// <summary>
        /// 15m
        /// </summary>
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// 30m
        /// </summary>
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// 1h
        /// </summary>
        OneHour = 60 * 60,
        /// <summary>
        /// 4h
        /// </summary>
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// 1d
        /// </summary>
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// 1w
        /// </summary>
        OneWeek = 60 * 60 * 24 * 7,
        /// <summary>
        /// 1m
        /// </summary>
        OneMonth = 60 * 60 * 24 * 30,
        /// <summary>
        /// 1y
        /// </summary>
        OneYear = 60 * 60 * 24 * 365
    }
}
