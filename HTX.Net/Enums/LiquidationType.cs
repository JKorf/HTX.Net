using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Liquidation type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LiquidationType>))]
    public enum LiquidationType
    {
        /// <summary>
        /// ["<c>0</c>"] Not a liquidation
        /// </summary>
        [Map("0")]
        NonLiquidated,
        /// <summary>
        /// ["<c>1</c>"] Long and short netting
        /// </summary>
        [Map("1")]
        LongAndShortNetting,
        /// <summary>
        /// ["<c>2</c>"] Partial liquidation
        /// </summary>
        [Map("2")]
        PartialLiquidated,
        /// <summary>
        /// ["<c>3</c>"] Full liquidation
        /// </summary>
        [Map("3")]
        FullLiquidated
    }
}
