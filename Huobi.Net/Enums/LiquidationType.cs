using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Liquidation type
    /// </summary>
    public enum LiquidationType
    {
        /// <summary>
        /// Not a liquidation
        /// </summary>
        [Map("0")]
        NonLiquidated,
        /// <summary>
        /// Long and short netting
        /// </summary>
        [Map("1")]
        LongAndShortNetting,
        /// <summary>
        /// Partial liquidation
        /// </summary>
        [Map("2")]
        PartialLiquidated,
        /// <summary>
        /// Full liquidation
        /// </summary>
        [Map("3")]
        FullLiquidated
    }
}
