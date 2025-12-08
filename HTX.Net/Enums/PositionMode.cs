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
        /// Single side
        /// </summary>
        [Map("single_side")]
        SingleSide,
        /// <summary>
        /// Dual side
        /// </summary>
        [Map("dual_side")]
        DualSide
    }
}
