using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Asset mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetMode>))]
    public enum AssetMode
    {
        /// <summary>
        /// ["<c>0</c>"] Single-asset collateral mode (old)
        /// </summary>
        [Map("0")]
        SingleAssetCollateralOld,
        /// <summary>
        /// ["<c>1</c>"] Multi-assets collateral mode
        /// </summary>
        [Map("1")]
        MultiAssetsCollateral,
        /// <summary>
        /// ["<c>2</c>"] Single-asset collateral mode
        /// </summary>
        [Map("2")]
        SingleAssetCollateral
    }
}
