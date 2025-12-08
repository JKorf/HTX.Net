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
        /// Normal
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("expired")]
        Expired
    }
}
