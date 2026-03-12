using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginMode>))]
    public enum MarginMode
    {
        /// <summary>
        /// ["<c>cross</c>"] Cross margin
        /// </summary>
        [Map("cross")]
        Cross,
        /// <summary>
        /// ["<c>isolated</c>"] Isolated margin
        /// </summary>
        [Map("isolated")]
        Isolated,
        /// <summary>
        /// ["<c>all</c>"] All (filter)
        /// </summary>
        [Map("all")]
        All
    }
}
