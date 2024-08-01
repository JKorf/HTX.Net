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
        public static HTXRestOptions Default { get; set; } = new HTXRestOptions()
        {
            Environment = HTXEnvironment.Live
        };

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

        internal HTXRestOptions Copy()
        {
            var options = Copy<HTXRestOptions>();
            options.SpotOptions = SpotOptions.Copy<RestApiOptions>();
            options.UsdtMarginSwapOptions = UsdtMarginSwapOptions.Copy<RestApiOptions>();
            options.SignPublicRequests = SignPublicRequests;
            options.BrokerId = BrokerId;
            return options;
        }
    }
}
