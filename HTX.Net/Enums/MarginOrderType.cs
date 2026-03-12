using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Margin order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginOrderType>))]
    public enum MarginOrderType
    {
        /// <summary>
        /// ["<c>1</c>"] Quoatation
        /// </summary>
        [Map("1")]
        Quatation,
        /// <summary>
        /// ["<c>2</c>"] Canceled order
        /// </summary>
        [Map("2")]
        CanceledOrder,
        /// <summary>
        /// ["<c>3</c>"] Forced liquidation
        /// </summary>
        [Map("3")]
        ForcedLiquidation,
        /// <summary>
        /// ["<c>4</c>"] Delivery
        /// </summary>
        [Map("4")]
        DeliveryOrder
    }
}
