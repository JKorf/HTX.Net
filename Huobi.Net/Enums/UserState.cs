using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// User state
    /// </summary>
    public enum UserState
    {
        /// <summary>
        /// Normal
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// Locked
        /// </summary>
        [Map("lock")]
        Locked
    }
}
