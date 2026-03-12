using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Time an order is active
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TimeInForce>))]
    public enum TimeInForce
    {
        /// <summary>
        /// ["<c>gtc</c>"] Good until the order is canceled
        /// </summary>
        [Map("gtc")]
        GoodTillCancel,
        /// <summary>
        /// ["<c>ioc</c>"] Should execute at least partly upon placing
        /// </summary>
        [Map("ioc")]
        ImmediateOrCancel,
        /// <summary>
        /// ["<c>boc</c>"] Should enter the book upon placing
        /// </summary>
        [Map("boc")]
        BookOrCancel,
        /// <summary>
        /// ["<c>fok</c>"] Should fill entirely upon placing
        /// </summary>
        [Map("fok")]
        FillOrKill
    }
}
