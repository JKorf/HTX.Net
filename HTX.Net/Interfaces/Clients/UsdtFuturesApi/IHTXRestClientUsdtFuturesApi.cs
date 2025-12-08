using CryptoExchange.Net.Interfaces.Clients;
using HTX.Net.Interfaces.Clients.SpotApi;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Usdt futures api endpoints
    /// </summary>
    public interface IHTXRestClientUsdtFuturesApi : IRestApiClient
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
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IHTXRestClientUsdtFuturesApiShared SharedClient { get; }
    }
}