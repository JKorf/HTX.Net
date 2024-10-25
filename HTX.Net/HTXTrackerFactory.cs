using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using HTX.Net.Interfaces;
using HTX.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace HTX.Net
{
    /// <inheritdoc />
    public class HTXTrackerFactory : IHTXTrackerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public HTXTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            IKlineRestClient restClient;
            IKlineSocketClient socketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                restClient = _serviceProvider.GetRequiredService<IHTXRestClient>().SpotApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IHTXSocketClient>().SpotApi.SharedClient;
            }
            else
            {
                restClient = _serviceProvider.GetRequiredService<IHTXRestClient>().UsdtFuturesApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IHTXSocketClient>().UsdtFuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                socketClient,
                symbol,
                interval,
                limit,
                period
                );
        }
        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            IRecentTradeRestClient? restClient = null;
            ITradeSocketClient socketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                restClient = _serviceProvider.GetRequiredService<IHTXRestClient>().SpotApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IHTXSocketClient>().SpotApi.SharedClient;
            }
            else
            {
                restClient = _serviceProvider.GetRequiredService<IHTXRestClient>().UsdtFuturesApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IHTXSocketClient>().UsdtFuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                socketClient,
                symbol,
                limit,
                period
                );
        }
    }
}
