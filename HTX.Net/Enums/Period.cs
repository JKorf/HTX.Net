using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Period
    /// </summary>
    [JsonConverter(typeof(EnumConverter<Period>))]
    public enum Period
    {
        /// <summary>
        /// Five minutes
        /// </summary>
        [Map("5min")]
        FiveMinutes,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("15min")]
        FifteenMinutes,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("30min")]
        ThirtyMinutes,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("60min")]
        OneHour,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("4hour")]
        FourHours,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1day")]
        OneDay
    }
}
