using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Account activation
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountActivation>))]
    public enum AccountActivation
    {
        /// <summary>
        /// ["<c>activated</c>"] Activated
        /// </summary>
        [Map("activated")]
        Activated,
        /// <summary>
        /// ["<c>deactivated</c>"] Deactivated
        /// </summary>
        [Map("deactivated")]
        Deactivated
    }
}
