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
        /// ["<c>isolated-margin</c>"] Isolated margin
        /// </summary>
        [Map("isolated-margin")]
        IsolatedMargin,
        /// <summary>
        /// ["<c>cross-margin</c>"] Cross margin
        /// </summary>
        [Map("cross-margin")]
        CrossMargin,
        /// <summary>
        /// ["<c>spot</c>"] Spot
        /// </summary>
        [Map("spot")]
        Spot
    }
}
