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
        /// Whether to allow the client to adjust the clientOrderId parameter send by the user when placing orders to include a client reference. This reference is used by the exchange to allocate a small percentage of the paid trading fees to developer of this library. Defaults to false.<br />
        /// Note that:<br />
        /// * It does not impact the amount of fees a user pays in any way<br />
        /// * It does not impact functionality. The reference is added just before sending the request and removed again during data deserialization<br />
        /// * It does respect client order id field limitations. For example if the user provided client order id parameter is too long to fit the reference it will not be added<br />
        /// * Toggling this option might fail operations using a clientOrderId parameter for pre-existing orders which were placed before the toggle. Operations on orders placed after the toggle will work as expected. It's adviced to toggle when there are no open orders
        /// </summary>
        public bool AllowAppendingClientOrderId { get; set; } = false;

        /// <summary>
        /// Broker id
        /// </summary>
        public string? BrokerId { get; set; }

        /// <summary>
        /// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        public bool SignPublicRequests { get; set; } = false;

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
            targetOptions.AllowAppendingClientOrderId = AllowAppendingClientOrderId;
            targetOptions.SignPublicRequests = SignPublicRequests;
            targetOptions.BrokerId = BrokerId;
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.UsdtMarginSwapOptions = UsdtMarginSwapOptions.Set(targetOptions.UsdtMarginSwapOptions);
            return targetOptions;
        }
    }
}
