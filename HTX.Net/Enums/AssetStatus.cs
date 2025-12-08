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
        /// Normal
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// Delisted
        /// </summary>
        [Map("delisted")]
        Delisted
    }
}
