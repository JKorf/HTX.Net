using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Fee type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FeeType>))]
    public enum FeeType
    {
        /// <summary>
        /// ["<c>fixed</c>"] Fixed
        /// </summary>
        [Map("fixed")]
        Fixed,
        /// <summary>
        /// ["<c>circulated</c>"] Circulated
        /// </summary>
        [Map("circulated")]
        Circulated,
        /// <summary>
        /// ["<c>ratio</c>"] Ratio
        /// </summary>
        [Map("ratio")]
        Ratio
    }
}
