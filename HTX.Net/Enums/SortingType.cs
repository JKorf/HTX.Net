using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Sorting order
    /// </summary>
    public enum SortingType
    {
        /// <summary>
        /// Ascending
        /// </summary>
        [Map("asc")]
        Ascending,
        /// <summary>
        /// Descending
        /// </summary>
        [Map("desc")]
        Descending
    }
}
