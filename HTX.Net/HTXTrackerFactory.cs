using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using HTX.Net.Clients;
using HTX.Net.Interfaces;
using HTX.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace HTX.Net
{
    /// <inheritdoc />
    public class HTXTrackerFactory : IHTXTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public HTXTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public HTXTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval)
        {
            var client = (_serviceProvider?.GetRequiredService<IHTXSocketClient>() ?? new HTXSocketClient());
            SubscribeKlineOptions klineOptions = symbol.TradingMode == TradingMode.Spot ? client.SpotApi.SharedClient.SubscribeKlineOptions : client.UsdtFuturesApi.SharedClient.SubscribeKlineOptions;
            return klineOptions.IsSupported(interval);
        }

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IHTXRestClient>() ?? new HTXRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IHTXSocketClient>() ?? new HTXSocketClient();

            IKlineRestClient sharedRestClient;
            IKlineSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.UsdtFuturesApi.SharedClient;
                sharedSocketClient = socketClient.UsdtFuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                sharedSocketClient,
                symbol,
                interval,
                limit,
                period
                );
        }
        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IHTXRestClient>() ?? new HTXRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IHTXSocketClient>() ?? new HTXSocketClient();

            IRecentTradeRestClient? sharedRestClient;
            ITradeSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.UsdtFuturesApi.SharedClient;
                sharedSocketClient = socketClient.UsdtFuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                null,
                sharedSocketClient,
                symbol,
                limit,
                period
                );
        }
        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IHTXRestClient>() ?? new HTXRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IHTXSocketClient>() ?? new HTXSocketClient();
            return new HTXUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<HTXUserSpotDataTracker>>() ?? new NullLogger<HTXUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, ApiCredentials credentials, SpotUserDataTrackerConfig? config = null, HTXEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IHTXUserClientProvider>() ?? new HTXUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new HTXUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<HTXUserSpotDataTracker>>() ?? new NullLogger<HTXUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(SharedMarginMode marginMode, FuturesUserDataTrackerConfig? config = null)
        {
            var exchangeParams = new ExchangeParameters(new ExchangeParameter("HTX", "MarginMode", marginMode));

            var restClient = _serviceProvider?.GetRequiredService<IHTXRestClient>() ?? new HTXRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IHTXSocketClient>() ?? new HTXSocketClient();
            return new HTXUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<HTXUserFuturesDataTracker>>() ?? new NullLogger<HTXUserFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config,
                exchangeParams
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, ApiCredentials credentials, SharedMarginMode marginMode, FuturesUserDataTrackerConfig? config = null, HTXEnvironment? environment = null)
        {
            var exchangeParams = new ExchangeParameters(new ExchangeParameter("HTX", "MarginMode", marginMode));

            var clientProvider = _serviceProvider?.GetRequiredService<IHTXUserClientProvider>() ?? new HTXUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new HTXUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<HTXUserFuturesDataTracker>>() ?? new NullLogger<HTXUserFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config,
                exchangeParams
                );
        }
    }
}
