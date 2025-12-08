using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Market type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SubAccountMarketType>))]
    public enum SubAccountMarketType
    {
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("isolated-margin")]
        IsolatedMargin,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("cross-margin")]
        CrossMargin,
        /// <summary>
        /// Spot
        /// </summary>
        [Map("spot")]
        Spot
    }
}
