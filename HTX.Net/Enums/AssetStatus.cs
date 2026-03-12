using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Asset status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetStatus>))]
    public enum AssetStatus
    {
        /// <summary>
        /// ["<c>normal</c>"] Normal
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// ["<c>delisted</c>"] Delisted
        /// </summary>
        [Map("delisted")]
        Delisted
    }
}
