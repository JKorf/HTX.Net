using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.OrderBook;
using Huobi.Net.Interfaces;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Huobi.Net.SymbolOrderBooks
{
    /// <inheritdoc />
    public class HuobiOrderBookFactory : IHuobiOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <inheritdoc />
        public IOrderBookFactory<HuobiOrderBookOptions> Spot { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public HuobiOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Spot = new OrderBookFactory<HuobiOrderBookOptions>((symbol, options) => CreateSpot(symbol, options), (baseAsset, quoteAsset, options) => CreateSpot(baseAsset.ToLowerInvariant() + quoteAsset.ToLowerInvariant(), options));
        }

        /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<HuobiOrderBookOptions>? options = null)
            => new HuobiSpotSymbolOrderBook(symbol,
                                        options,
                                        _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                        _serviceProvider.GetRequiredService<IHuobiSocketClient>());
    }
}
