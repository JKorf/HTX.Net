using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Api key status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ApiKeyStatus>))]
    public enum ApiKeyStatus
    {
        /// <summary>
        /// ["<c>normal</c>"] Normal
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// ["<c>expired</c>"] Expired
        /// </summary>
        [Map("expired")]
        Expired
    }
}
