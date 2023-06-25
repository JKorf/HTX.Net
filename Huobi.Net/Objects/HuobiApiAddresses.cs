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
        public string SocketClientAddress { get; set; } = "";

        /// <summary>
        /// The address used by the HuobiClient for the rest usdt margin swaps API
        /// </summary>
        public string UsdtMarginSwapRestClientAddress { get; set; } = "";        
        /// <summary>
        /// The address used by the HuobiSocketClient for the private user socket API
        /// </summary>
        public string UsdtMarginSwapSocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Huobi.com API
        /// </summary>
        public static HuobiApiAddresses Default = new HuobiApiAddresses
        {
            RestClientAddress = "https://api.huobi.pro",
            SocketClientAddress = "wss://api.huobi.pro",
            UsdtMarginSwapRestClientAddress = "https://api.hbdm.com",
            UsdtMarginSwapSocketClientAddress = "wss://api.hbdm.com"
        };
    }
}
