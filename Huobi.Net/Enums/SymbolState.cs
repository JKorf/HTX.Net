using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Symbol state
    /// </summary>
    public enum SymbolState
    {
        /// <summary>
        /// Not yet online
        /// </summary>
        [Map("pre-online")]
        PreOnline,
        /// <summary>
        /// Online
        /// </summary>
        [Map("online")]
        Online,
        /// <summary>
        /// Offline
        /// </summary>
        [Map("offline")]
        Offline,
        /// <summary>
        /// Suspended
        /// </summary>
        [Map("suspend")]
        Suspended
    }
}
