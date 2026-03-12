using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionMode>))]
    public enum PositionMode
    {
        /// <summary>
        /// ["<c>single_side</c>"] Single side
        /// </summary>
        [Map("single_side")]
        SingleSide,
        /// <summary>
        /// ["<c>dual_side</c>"] Dual side
        /// </summary>
        [Map("dual_side")]
        DualSide
    }
}
