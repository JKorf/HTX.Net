using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Filter direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FilterDirection>))]
    public enum FilterDirection
    {
        /// <summary>
        /// ["<c>next</c>"] Get results after
        /// </summary>
        [Map("next")]
        Next,
        /// <summary>
        /// ["<c>prev</c>"] Get results before
        /// </summary>
        [Map("prev")]
        Previous
    }
}
