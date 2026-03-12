using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Interest period
    /// </summary>
    [JsonConverter(typeof(EnumConverter<InterestPeriod>))]
    public enum InterestPeriod
    {
        /// <summary>
        /// ["<c>60min</c>"] One hour
        /// </summary>
        [Map("60min")]
        OneHour,
        /// <summary>
        /// ["<c>4hour</c>"] Four hours
        /// </summary>
        [Map("4hour")]
        FourHour,
        /// <summary>
        /// ["<c>12hour</c>"] Twelf hours
        /// </summary>
        [Map("12hour")]
        TwelfHour,
        /// <summary>
        /// ["<c>1day</c>"] One day
        /// </summary>
        [Map("1day")]
        OneDay
    }
}
