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
        /// Whether to allow the client to adjust the clientOrderId parameter send by the user when placing orders to include a client reference. This reference is used by the exchange to allocate a small percentage of the paid trading fees to developer of this library. Defaults to false.<br />
        /// Note that:<br />
        /// * It does not impact the amount of fees a user pays in any way<br />
        /// * It does not impact functionality. The reference is added just before sending the request and removed again during data deserialization<br />
        /// * It does respect client order id field limitations. For example if the user provided client order id parameter is too long to fit the reference it will not be added<br />
        /// * Toggling this option might fail operations using a clientOrderId parameter for pre-existing orders which were placed before the toggle. Operations on orders placed after the toggle will work as expected. It's adviced to toggle when there are no open orders
        /// </summary>
        public bool AllowAppendingClientOrderId { get; set; } = false;

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
            targetOptions.AllowAppendingClientOrderId = AllowAppendingClientOrderId;
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.UsdtMarginSwapOptions = UsdtMarginSwapOptions.Set(targetOptions.UsdtMarginSwapOptions);
            return targetOptions;
        }
    }
}
