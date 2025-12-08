using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Conditional order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ConditionalOrderType>))]
    public enum ConditionalOrderType
    {
        /// <summary>
        /// Limit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        [Map("market")]
        Market
    }
}
