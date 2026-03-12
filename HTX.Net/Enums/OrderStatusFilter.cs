using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Order status filter
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatusFilter>))]
    public enum OrderStatusFilter
    {
        /// <summary>
        /// ["<c>0</c>"] Order statuses
        /// </summary>
        [Map("0")]
        All,
        /// <summary>
        /// ["<c>1</c>"] Placing in book
        /// </summary>
        [Map("1", "2")]
        ReadyToPlace,
        /// <summary>
        /// ["<c>3</c>"] Submitted orders
        /// </summary>
        [Map("3")]
        Submitted,
        /// <summary>
        /// ["<c>4</c>"] Partially matched orders
        /// </summary>
        [Map("4")]
        PartiallyMatched,
        /// <summary>
        /// ["<c>5</c>"] Partially canceled orders
        /// </summary>
        [Map("5")]
        PartiallyCanceled,
        /// <summary>
        /// ["<c>6</c>"] Fully executed orders
        /// </summary>
        [Map("6")]
        FullyMatched,
        /// <summary>
        /// ["<c>7</c>"] Canceled orders
        /// </summary>
        [Map("7")]
        Canceled,
        /// <summary>
        /// ["<c>11</c>"] Canceled orders
        /// </summary>
        [Map("11")]
        Canceling
    }
}
