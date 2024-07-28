using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Account state
    /// </summary>
    public enum AccountState
    {
        /// <summary>
        /// Working
        /// </summary>
        [Map("working")]
        Working,
        /// <summary>
        /// Locked
        /// </summary>
        [Map("lock")]
        Locked
    }
}
