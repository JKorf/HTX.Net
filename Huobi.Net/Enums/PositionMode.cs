using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Position mode
    /// </summary>
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
