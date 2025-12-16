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
        /// Activated
        /// </summary>
        [Map("activated")]
        Activated,
        /// <summary>
        /// Deactivated
        /// </summary>
        [Map("deactivated")]
        Deactivated
    }
}
