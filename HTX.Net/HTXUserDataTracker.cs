using HTX.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;

namespace HTX.Net
{
    /// <inheritdoc/>
    public class HTXUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public HTXUserSpotDataTracker(
            ILogger<HTXUserSpotDataTracker> logger,
            IHTXRestClient restClient,
            IHTXSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig config) : base(
                logger,
                restClient.SpotApi.SharedClient,
                null,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                userIdentifier,
                config)
        {
        }
    }

    /// <inheritdoc/>
    public class HTXUserFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc/>
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => true;

        /// <summary>
        /// ctor
        /// </summary>
        public HTXUserFuturesDataTracker(
            ILogger<HTXUserFuturesDataTracker> logger,
            IHTXRestClient restClient,
            IHTXSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig config,
            ExchangeParameters? exchangeParameters) : base(logger,
                restClient.UsdtFuturesApi.SharedClient,
                null,
                restClient.UsdtFuturesApi.SharedClient,
                socketClient.UsdtFuturesApi.SharedClient,
                restClient.UsdtFuturesApi.SharedClient,
                socketClient.UsdtFuturesApi.SharedClient,
                socketClient.UsdtFuturesApi.SharedClient,
                socketClient.UsdtFuturesApi.SharedClient,
                userIdentifier,
                config,
                exchangeParameters: exchangeParameters)
        {
        }
    }
}
