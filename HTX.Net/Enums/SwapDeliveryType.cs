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
        /// Perpetual futures
        /// </summary>
        [Map("1")]
        UsdtPerpetualFutures,
        /// <summary>
        /// Delivery futures
        /// </summary>
        [Map("2")]
        UsdtDeliveryFutures,
        /// <summary>
        /// Both USDT perpetual and delivery futures
        /// </summary>
        [Map("3")]
        UsdtPerpetualAndDeliveryFutures
    }
}
