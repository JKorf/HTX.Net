using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Order status filter
    /// </summary>
    public enum OrderStatusFilter
    {
        /// <summary>
        /// Placing in book
        /// </summary>
        [Map("1", "2")]
        ReadyToPlace,
        /// <summary>
        /// Submitted orders
        /// </summary>
        [Map("3")]
        Submitted,
        /// <summary>
        /// Partially matched orders
        /// </summary>
        [Map("4")]
        PartiallyMatched,
        /// <summary>
        /// Partially canceled orders
        /// </summary>
        [Map("5")]
        PartiallyCanceled,
        /// <summary>
        /// Fully executed orders
        /// </summary>
        [Map("6")]
        FullyMatched,
        /// <summary>
        /// Canceled orders
        /// </summary>
        [Map("7")]
        Canceled,
        /// <summary>
        /// Canceled orders
        /// </summary>
        [Map("11")]
        Canceling
    }
}
