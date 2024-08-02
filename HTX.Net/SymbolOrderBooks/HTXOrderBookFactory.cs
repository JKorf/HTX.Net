using CryptoExchange.Net.OrderBook;
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

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public HTXOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Spot = new OrderBookFactory<HTXOrderBookOptions>((symbol, options) => CreateSpot(symbol, options), (baseAsset, quoteAsset, options) => CreateSpot(baseAsset.ToLowerInvariant() + quoteAsset.ToLowerInvariant(), options));
        }

        /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<HTXOrderBookOptions>? options = null)
            => new HTXSpotSymbolOrderBook(symbol,
                                        options,
                                        _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                        _serviceProvider.GetRequiredService<IHTXSocketClient>());
    }
}
