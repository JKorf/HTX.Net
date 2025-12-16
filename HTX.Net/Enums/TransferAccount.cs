using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Account type for transfer
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferAccount>))]
    public enum TransferAccount
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// Linear swap
        /// </summary>
        [Map("linear-swap")]
        LinearSwap,
        /// <summary>
        /// OTC
        /// </summary>
        [Map("otc")]
        Otc,
        /// <summary>
        /// Futures
        /// </summary>
        [Map("futures")]
        Futures,
        /// <summary>
        /// Swap
        /// </summary>
        [Map("swap")]
        Swap
    }
}
