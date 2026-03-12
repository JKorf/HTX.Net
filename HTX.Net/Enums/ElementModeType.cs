using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Margin mode type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ElementModeType>))]
    public enum ElementModeType
    {
        /// <summary>
        /// ["<c>1</c>"] Isolated margin
        /// </summary>
        [Map("1")]
        IsolatedMargin,
        /// <summary>
        /// ["<c>2</c>"] Cross and isolated margin
        /// </summary>
        [Map("2")]
        CrossAndIsolatedMargin,
        /// <summary>
        /// ["<c>3</c>"] Cross margin
        /// </summary>
        [Map("3")]
        CrossMargin
    }
}
