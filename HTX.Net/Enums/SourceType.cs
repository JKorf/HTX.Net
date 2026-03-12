using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Source
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SourceType>))]
    public enum SourceType
    {
        /// <summary>
        /// ["<c>spot-api</c>"] Spot api
        /// </summary>
        [Map("spot-api")]
        Spot,
        /// <summary>
        /// ["<c>margin-api</c>"] Isolate margin api
        /// </summary>
        [Map("margin-api")]
        IsolatedMargin,
        /// <summary>
        /// ["<c>super-margin-api</c>"] Cross margin api
        /// </summary>
        [Map("super-margin-api")]
        CrossMargin,
        /// <summary>
        /// ["<c>c2c-margin-api</c>"] c2c margin api
        /// </summary>
        [Map("c2c-margin-api")]
        C2CMargin
    }
}
