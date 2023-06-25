using CryptoExchange.Net.Interfaces;
using Huobi.Net.Interfaces;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Objects.Options;
using Huobi.Net.SymbolOrderBooks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Huobi.Net.SymbolOrderBooks
{
    /// <inheritdoc />
    public class HuobiOrderBookFactory : IHuobiOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public HuobiOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<HuobiOrderBookOptions>? options = null)
            => new HuobiSpotSymbolOrderBook(symbol,
                                        options,
                                        _serviceProvider.GetRequiredService<ILogger<HuobiSpotSymbolOrderBook>>(),
                                        _serviceProvider.GetRequiredService<IHuobiSocketClient>());
    }
}
