using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Trigger price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerPriceType>))]
    public enum TriggerPriceType
    {
        /// <summary>
        /// ["<c>last</c>"] Last price
        /// </summary>
        [Map("last")]
        Last,
        /// <summary>
        /// ["<c>mark</c>"] Mark price
        /// </summary>
        [Map("mark", "market")]
        Mark,
        /// <summary>
        /// ["<c>index</c>"] Index price
        /// </summary>
        [Map("index")]
        Index
    }
}
