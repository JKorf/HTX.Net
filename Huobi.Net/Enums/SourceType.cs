using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Source
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// Spot api
        /// </summary>
        [Map("spot-api")]
        Spot,
        /// <summary>
        /// Isolate margin api
        /// </summary>
        [Map("margin-api")]
        IsolatedMargin,
        /// <summary>
        /// Cross margin api
        /// </summary>
        [Map("super-margin-api")]
        CrossMargin,
        /// <summary>
        /// c2c margin api
        /// </summary>
        [Map("c2c-margin-api")]
        C2CMargin
    }
}
