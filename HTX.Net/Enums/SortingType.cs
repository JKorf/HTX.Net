using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Sorting order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SortingType>))]
    public enum SortingType
    {
        /// <summary>
        /// ["<c>asc</c>"] Ascending
        /// </summary>
        [Map("asc")]
        Ascending,
        /// <summary>
        /// ["<c>desc</c>"] Descending
        /// </summary>
        [Map("desc")]
        Descending
    }
}
