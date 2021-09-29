namespace Huobi.Net.Objects
{
    /// <summary>
    /// Huobi API addresses
    /// </summary>
    public class HuobiApiAddresses
    {
        /// <summary>
        /// The address used by the HuobiClient for the rest API
        /// </summary>
        public string RestApiAddress { get; set; } = string.Empty;

        /// <summary>
        /// The address used by the HuobiSocketClient for basic subscriptions
        /// </summary>
        public string BaseWebsocketAddress { get; set; } = string.Empty;

        /// <summary>
        /// The address used by the HuobiSocketClient for authenticated subscriptions
        /// </summary>
        public string AuthWebsocketAddress { get; set; } = string.Empty;

        /// <summary>
        /// The address used by the HuobiSocketClient for Market-By-Price subscriptions ( see https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update )
        /// </summary>
        public string MarketByPriceWebsocketAddress { get; set; } = string.Empty;

        /// <summary>
        /// The default addresses to connect to the huobi.com API
        /// </summary>
        public static HuobiApiAddresses Default = new HuobiApiAddresses
        {
            RestApiAddress = "https://api.huobi.pro",
            BaseWebsocketAddress = "wss://api.huobi.pro/ws",
            AuthWebsocketAddress = "wss://api.huobi.pro/ws/v2",
            MarketByPriceWebsocketAddress = "wss://api.huobi.pro/feed"
        };
    }
}
