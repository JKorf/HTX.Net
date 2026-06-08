using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// V5 order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderTypeV5>))]
    public enum OrderTypeV5
    {
        /// <summary>
        /// ["<c>market</c>"] Market
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// ["<c>limit</c>"] Limit
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// ["<c>post_only</c>"] Post only
        /// </summary>
        [Map("post_only")]
        PostOnly
    }
}
