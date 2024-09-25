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
        IHTXRestClientUsdtFuturesApiAccount Account { get; }
        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IHTXRestClientUsdtFuturesApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Endpoints related to sub accounts
        /// </summary>
        IHTXRestClientUsdtFuturesApiSubAccount SubAccount { get; }
        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IHTXRestClientUsdtFuturesApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exhanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IHTXRestClientUsdtFuturesApiShared SharedClient { get; }
    }
}