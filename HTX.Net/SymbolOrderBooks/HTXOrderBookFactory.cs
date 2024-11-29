using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;
using HTX.Net.Interfaces;
using HTX.Net.Interfaces.Clients;
using HTX.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;

namespace HTX.Net.SymbolOrderBooks
{
    /// <inheritdoc />
    public class HTXOrderBookFactory : IHTXOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <inheritdoc />
        public IOrderBookFactory<HTXOrderBookOptions> Spot { get; }
        /// <inheritdoc />
        public IOrderBookFactory<HTXOrderBookOptions> UsdtFutures { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public HTXOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Spot = new OrderBookFactory<HTXOrderBookOptions>(CreateSpot, Create);
            UsdtFutures = new OrderBookFactory<HTXOrderBookOptions>(CreateUsdtFutures, Create);
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<HTXOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(HTXExchange.FormatSymbol);
            if (symbol.TradingMode == TradingMode.Spot)
                return CreateSpot(symbolName, options);

            return CreateUsdtFutures(symbolName, options);
        }

        /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<HTXOrderBookOptions>? options = null)
            => new HTXSpotSymbolOrderBook(symbol,
                                        options,
                                        _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                        _serviceProvider.GetRequiredService<IHTXSocketClient>());

        /// <inheritdoc />
        public ISymbolOrderBook CreateUsdtFutures(string symbol, Action<HTXOrderBookOptions>? options = null)
            => new HTXUsdtFuturesSymbolOrderBook(symbol,
                                        options,
                                        _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                        _serviceProvider.GetRequiredService<IHTXSocketClient>());
    }
}
