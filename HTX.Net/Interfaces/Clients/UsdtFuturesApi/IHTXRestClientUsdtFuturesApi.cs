using CryptoExchange.Net.Interfaces.Clients;
using HTX.Net.Interfaces.Clients.SpotApi;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Usdt futures api endpoints
    /// </summary>
    public interface IHTXRestClientUsdtFuturesApi : IRestApiClient<HTXCredentials>
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IHTXRestClientUsdtFuturesApiAccount"/>
        IHTXRestClientUsdtFuturesApiAccount Account { get; }
        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IHTXRestClientUsdtFuturesApiExchangeData"/>
        IHTXRestClientUsdtFuturesApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Endpoints related to sub accounts
        /// </summary>
        /// <see cref="IHTXRestClientUsdtFuturesApiSubAccount"/>
        IHTXRestClientUsdtFuturesApiSubAccount SubAccount { get; }
        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IHTXRestClientUsdtFuturesApiTrading"/>
        IHTXRestClientUsdtFuturesApiTrading Trading { get; }

        /// <summary>
        /// [V1] Get the shared rest requests client. For new implementations prefer <see cref="SharedApi"/>
        /// </summary>
        public IHTXRestClientUsdtFuturesApiShared SharedClient { get; }

        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IHTXRestClientUsdtFuturesSharedApi SharedApi { get; }
    }
}