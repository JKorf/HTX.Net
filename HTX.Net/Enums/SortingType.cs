using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
