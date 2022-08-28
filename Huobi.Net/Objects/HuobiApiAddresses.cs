namespace Huobi.Net.Objects
{
    /// <summary>
    /// Api addresses usable for the Huobi clients
    /// </summary>
    public class HuobiApiAddresses
    {
        /// <summary>
        /// The address used by the HuobiClient for the rest spot API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the HuobiSocketClient for the public socket API
        /// </summary>
        public string SocketClientPublicAddress { get; set; } = "";
        /// <summary>
        /// The address used by the HuobiSocketClient for the private socket API
        /// </summary>
        public string SocketClientPrivateAddress { get; set; } = "";
        /// <summary>
        /// The address used by the HuobiSocketClient for the incremental order book socket API
        /// </summary>
        public string SocketClientIncrementalOrderBookAddress { get; set; } = "";

        /// <summary>
        /// The address used by the HuobiClient for the rest usdt margin swaps API
        /// </summary>
        public string UsdtMarginSwapRestClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Huobi.com API
        /// </summary>
        public static HuobiApiAddresses Default = new HuobiApiAddresses
        {
            RestClientAddress = "https://api.huobi.pro",
            SocketClientPublicAddress = "wss://api.huobi.pro/ws",
            SocketClientPrivateAddress = "wss://api.huobi.pro/ws/v2",
            SocketClientIncrementalOrderBookAddress = "wss://api.huobi.pro/feed",
            UsdtMarginSwapRestClientAddress = "https://api.hbdm.com",
        };
    }
}
