using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Delivery type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SwapDeliveryType>))]
    public enum SwapDeliveryType
    {
        /// <summary>
        /// ["<c>1</c>"] Perpetual futures
        /// </summary>
        [Map("1")]
        UsdtPerpetualFutures,
        /// <summary>
        /// ["<c>2</c>"] Delivery futures
        /// </summary>
        [Map("2")]
        UsdtDeliveryFutures,
        /// <summary>
        /// ["<c>3</c>"] Both USDT perpetual and delivery futures
        /// </summary>
        [Map("3")]
        UsdtPerpetualAndDeliveryFutures
    }
}
