using CryptoExchange.Net.Objects.Options;

namespace HTX.Net.Objects.Options
{
    /// <summary>
    /// Options for the HTXSocketClient
    /// </summary>
    public class HTXSocketOptions : SocketExchangeOptions<HTXEnvironment>
    {
        /// <summary>
        /// Default options for the HTXSocketClient
        /// </summary>
        public static HTXSocketOptions Default { get; set; } = new HTXSocketOptions
        {
            Environment = HTXEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions();

        /// <summary>
        /// Usdt Margin Swap API options
        /// </summary>
        public SocketApiOptions UsdtMarginSwapOptions { get; private set; } = new SocketApiOptions();

        internal HTXSocketOptions Copy()
        {
            var options = Copy<HTXSocketOptions>();
            options.SpotOptions = SpotOptions.Copy<SocketApiOptions>();
            options.UsdtMarginSwapOptions = UsdtMarginSwapOptions.Copy<SocketApiOptions>();
            return options;
        }
    }
}
