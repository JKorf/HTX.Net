using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Account activation
    /// </summary>
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
