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
        /// Settlement
        /// </summary>
        [Map("settlement")]
        Settlement,
        /// <summary>
        /// Delivery
        /// </summary>
        [Map("delivery")]
        Delivery
    }
}
