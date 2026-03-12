using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Business type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BusinessType>))]
    public enum BusinessType
    {
        /// <summary>
        /// ["<c>futures</c>"] Futures
        /// </summary>
        [Map("futures")]
        Futures,
        /// <summary>
        /// ["<c>swap</c>"] Swap
        /// </summary>
        [Map("swap")]
        Swap,
        /// <summary>
        /// ["<c>all</c>"] All (for filtering)
        /// </summary>
        [Map("all")]
        All
    }
}
