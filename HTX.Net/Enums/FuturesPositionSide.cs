using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Futures position side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesPositionSide>))]
    public enum FuturesPositionSide
    {
        /// <summary>
        /// ["<c>long</c>"] Long
        /// </summary>
        [Map("long")]
        Long,
        /// <summary>
        /// ["<c>short</c>"] Short
        /// </summary>
        [Map("short")]
        Short,
        /// <summary>
        /// ["<c>both</c>"] One-way position
        /// </summary>
        [Map("both")]
        Both
    }
}
