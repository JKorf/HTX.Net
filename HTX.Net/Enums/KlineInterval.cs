using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Interval for klines, int value represent the time in seconds
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KlineInterval>))]
    public enum KlineInterval
    {
        /// <summary>
        /// ["<c>1min</c>"] 1m
        /// </summary>
        [Map("1min")]
        OneMinute = 60,
        /// <summary>
        /// ["<c>5min</c>"] 5m
        /// </summary>
        [Map("5min")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// ["<c>15min</c>"] 15m
        /// </summary>
        [Map("15min")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// ["<c>30min</c>"] 30m
        /// </summary>
        [Map("30min")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// ["<c>60min</c>"] 1h
        /// </summary>
        [Map("60min")]
        OneHour = 60 * 60,
        /// <summary>
        /// ["<c>4hour</c>"] 4h
        /// </summary>
        [Map("4hour")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// ["<c>1day</c>"] 1d
        /// </summary>
        [Map("1day")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// ["<c>1week</c>"] 1w
        /// </summary>
        [Map("1week")]
        OneWeek = 60 * 60 * 24 * 7,
        /// <summary>
        /// ["<c>1mon</c>"] 1m
        /// </summary>
        [Map("1mon")]
        OneMonth = 60 * 60 * 24 * 30,
        /// <summary>
        /// ["<c>1year</c>"] 1y
        /// </summary>
        [Map("1year")]
        OneYear = 60 * 60 * 24 * 365
    }
}
