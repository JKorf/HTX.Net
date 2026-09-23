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
            SpotUserDataTrackerConfig? config) : base(
                logger,
                restClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig())
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
            FuturesUserDataTrackerConfig? config,
            ExchangeParameters? exchangeParameters) : base(logger,
                restClient.UsdtFuturesApi.SharedApi,

                restClient.UsdtFuturesApi.SharedApi,
                socketClient.UsdtFuturesApi.SharedApi,

                restClient.UsdtFuturesApi.SharedApi,
                restClient.UsdtFuturesApi.SharedApi,
                socketClient.UsdtFuturesApi.SharedApi,

                restClient.UsdtFuturesApi.SharedApi,
                socketClient.UsdtFuturesApi.SharedApi,

                restClient.UsdtFuturesApi.SharedApi,
                socketClient.UsdtFuturesApi.SharedApi,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig(),
                exchangeParameters: exchangeParameters)
        {
        }
    }
}
