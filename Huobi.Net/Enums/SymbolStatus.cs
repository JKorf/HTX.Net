using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Symbol status
    /// </summary>
    public enum SymbolStatus
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("unknown")]
        Unknown,
        /// <summary>
        /// Not online
        /// </summary>
        [Map("not-online")]
        NotOnline,
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
        Suspended,
        /// <summary>
        /// Transfer board
        /// </summary>
        [Map("transfer-board")]
        TransferBoard,
        /// <summary>
        /// Fuse
        /// </summary>
        [Map("fuse")]
        Fuse
    }
}
