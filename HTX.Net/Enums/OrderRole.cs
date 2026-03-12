using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Order role
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderRole>))]
    public enum OrderRole
    {
        /// <summary>
        /// ["<c>maker</c>"] Maker of an order book entry
        /// </summary>
        [Map("maker")]
        Maker,
        /// <summary>
        /// ["<c>taker</c>"] Taker of an order book entry
        /// </summary>
        [Map("taker")]
        Taker
    }
}
