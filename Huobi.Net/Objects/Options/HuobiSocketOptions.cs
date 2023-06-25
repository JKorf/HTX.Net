using CryptoExchange.Net.Objects.Options;

namespace Huobi.Net.Objects.Options
{
    /// <summary>
    /// Options for the HuobiSocketClient
    /// </summary>
    public class HuobiSocketOptions : SocketExchangeOptions<HuobiEnvironment>
    {
        /// <summary>
        /// Default options for the HuobiSocketClient
        /// </summary>
        public static HuobiSocketOptions Default { get; set; } = new HuobiSocketOptions
        {
            Environment = HuobiEnvironment.Live,
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

        internal HuobiSocketOptions Copy()
        {
            var options = Copy<HuobiSocketOptions>();
            options.SpotOptions = SpotOptions.Copy<SocketApiOptions>();
            options.UsdtMarginSwapOptions = UsdtMarginSwapOptions.Copy<SocketApiOptions>();
            return options;
        }
    }
}
