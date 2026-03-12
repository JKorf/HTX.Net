using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Symbol status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolStatus>))]
    public enum SymbolStatus
    {
        /// <summary>
        /// ["<c>unknown</c>"] Unknown
        /// </summary>
        [Map("unknown")]
        Unknown,
        /// <summary>
        /// ["<c>not-online</c>"] Not online
        /// </summary>
        [Map("not-online")]
        NotOnline,
        /// <summary>
        /// ["<c>pre-online</c>"] Not yet online
        /// </summary>
        [Map("pre-online")]
        PreOnline,
        /// <summary>
        /// ["<c>online</c>"] Online
        /// </summary>
        [Map("online")]
        Online,
        /// <summary>
        /// ["<c>offline</c>"] Offline
        /// </summary>
        [Map("offline")]
        Offline,
        /// <summary>
        /// ["<c>suspend</c>"] Suspended
        /// </summary>
        [Map("suspend")]
        Suspended,
        /// <summary>
        /// ["<c>transfer-board</c>"] Transfer board
        /// </summary>
        [Map("transfer-board")]
        TransferBoard,
        /// <summary>
        /// ["<c>fuse</c>"] Fuse
        /// </summary>
        [Map("fuse")]
        Fuse
    }
}
