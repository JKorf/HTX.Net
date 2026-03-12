using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Asset type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetType>))]
    public enum AssetType
    {
        /// <summary>
        /// ["<c>1</c>"] Virtual asset
        /// </summary>
        [Map("1")]
        Virtual,
        /// <summary>
        /// ["<c>2</c>"] Fiat asset
        /// </summary>
        [Map("2")]
        Fiat
    }
}
