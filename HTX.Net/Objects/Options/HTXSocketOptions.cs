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
        internal static HTXSocketOptions Default { get; set; } = new HTXSocketOptions
        {
            Environment = HTXEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public HTXSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Broker id
        /// </summary>
        public string? BrokerId { get; set; }

        /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions();

        /// <summary>
        /// Usdt Margin Swap API options
        /// </summary>
        public SocketApiOptions UsdtMarginSwapOptions { get; private set; } = new SocketApiOptions();

        internal HTXSocketOptions Set(HTXSocketOptions targetOptions)
        {
            targetOptions = base.Set<HTXSocketOptions>(targetOptions);
            targetOptions.BrokerId = BrokerId;
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.UsdtMarginSwapOptions = UsdtMarginSwapOptions.Set(targetOptions.UsdtMarginSwapOptions);
            return targetOptions;
        }
    }
}
