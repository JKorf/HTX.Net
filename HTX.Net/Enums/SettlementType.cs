using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Settlement type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SettlementType>))]
    public enum SettlementType
    {
        /// <summary>
        /// ["<c>settlement</c>"] Settlement
        /// </summary>
        [Map("settlement")]
        Settlement,
        /// <summary>
        /// ["<c>delivery</c>"] Delivery
        /// </summary>
        [Map("delivery")]
        Delivery
    }
}
