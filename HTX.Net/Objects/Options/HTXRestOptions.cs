using CryptoExchange.Net.Objects.Options;

namespace HTX.Net.Objects.Options
{
    /// <summary>
    /// Options for the HTXRestClient
    /// </summary>
    public class HTXRestOptions : RestExchangeOptions<HTXEnvironment>
    {
        /// <summary>
        /// Default options for the HTXRestClient
        /// </summary>
        internal static HTXRestOptions Default { get; set; } = new HTXRestOptions()
        {
            Environment = HTXEnvironment.Live
        };

        /// <summary>
        /// ctor
        /// </summary>
        public HTXRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        public bool SignPublicRequests { get; set; } = false;

        /// <summary>
        /// Broker id
        /// </summary>
        public string? BrokerId { get; set; }

        /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new RestApiOptions();

        /// <summary>
        /// USDT margin swap API options
        /// </summary>
        public RestApiOptions UsdtMarginSwapOptions { get; private set; } = new RestApiOptions();

        internal HTXRestOptions Set(HTXRestOptions targetOptions)
        {
            targetOptions = base.Set<HTXRestOptions>(targetOptions);
            targetOptions.SignPublicRequests = SignPublicRequests;
            targetOptions.BrokerId = BrokerId;
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.UsdtMarginSwapOptions = UsdtMarginSwapOptions.Set(targetOptions.UsdtMarginSwapOptions);
            return targetOptions;
        }
    }
}
