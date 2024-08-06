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
    }
}