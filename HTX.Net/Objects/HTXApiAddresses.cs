namespace HTX.Net.Objects
{
    /// <summary>
    /// Api addresses usable for the HTX clients
    /// </summary>
    public class HTXApiAddresses
    {
        /// <summary>
        /// The address used by the HTXRestClient for the rest spot API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the HTXSocketClient for the public socket API
        /// </summary>
        public string SocketClientAddress { get; set; } = "";

        /// <summary>
        /// The address used by the HTXRestClient for the rest usdt margin swaps API
        /// </summary>
        public string UsdtMarginSwapRestClientAddress { get; set; } = "";        
        /// <summary>
        /// The address used by the HTXSocketClient for the private user socket API
        /// </summary>
        public string UsdtMarginSwapSocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the HTX.com API
        /// </summary>
        public static HTXApiAddresses Default = new HTXApiAddresses
        {
            RestClientAddress = "https://api.huobi.pro",
            SocketClientAddress = "wss://api.huobi.pro",
            UsdtMarginSwapRestClientAddress = "https://api.hbdm.com",
            UsdtMarginSwapSocketClientAddress = "wss://api.hbdm.com"
        };
    }
}
