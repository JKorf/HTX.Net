using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Business type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ElementBusinessType>))]
    public enum ElementBusinessType
    {
        /// <summary>
        /// ["<c>1</c>"] Perpetual futures
        /// </summary>
        [Map("1")]
        PerpetualFutures,
        /// <summary>
        /// ["<c>2</c>"] Delivery futures
        /// </summary>
        [Map("2")]
        DeliveryFutures,
        /// <summary>
        /// ["<c>3</c>"] Perpetual + Delivery futures
        /// </summary>
        [Map("3")]
        PerpetualAndDeliveryFutures
    }
}
