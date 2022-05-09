using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Time an order is active
    /// </summary>
    public enum TimeInForce
    {
        /// <summary>
        /// Good until the order is canceled
        /// </summary>
        [Map("gtc")]
        GoodTillCancel,
        /// <summary>
        /// Should execute at least partly upon placing
        /// </summary>
        [Map("ioc")]
        ImmediateOrCancel,
        /// <summary>
        /// Should enter the book upon placing
        /// </summary>
        [Map("boc")]
        BookOrCancel,
        /// <summary>
        /// Should fill entirely upon placing
        /// </summary>
        [Map("fok")]
        FillOrKill
    }
}
