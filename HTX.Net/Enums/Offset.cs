using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Offset
    /// </summary>
    [JsonConverter(typeof(EnumConverter<Offset>))]
    public enum Offset
    {
        /// <summary>
        /// ["<c>open</c>"] Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// ["<c>close</c>"] Close
        /// </summary>
        [Map("close")]
        Close,
        /// <summary>
        /// ["<c>both</c>"] Both
        /// </summary>
        [Map("both")]
        Both
    }
}
