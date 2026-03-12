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
        /// ["<c>spot</c>"] Spot
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// ["<c>linear-swap</c>"] Linear swap
        /// </summary>
        [Map("linear-swap")]
        LinearSwap,
        /// <summary>
        /// ["<c>otc</c>"] OTC
        /// </summary>
        [Map("otc")]
        Otc,
        /// <summary>
        /// ["<c>futures</c>"] Futures
        /// </summary>
        [Map("futures")]
        Futures,
        /// <summary>
        /// ["<c>swap</c>"] Swap
        /// </summary>
        [Map("swap")]
        Swap
    }
}
