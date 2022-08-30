using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Interest period
    /// </summary>
    public enum InterestPeriod
    {
        /// <summary>
        /// One hour
        /// </summary>
        [Map("60min")]
        OneHour,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("4hour")]
        FourHour,
        /// <summary>
        /// Twelf hours
        /// </summary>
        [Map("12hour")]
        TwelfHour,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1day")]
        OneDay
    }
}
