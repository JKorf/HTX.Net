using CryptoExchange.Net.Interfaces.Clients;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesV5Api
{
    /// <summary>
    /// Usdt futures V5 api endpoints
    /// </summary>
    public interface IHTXRestClientUsdtFuturesV5Api : IRestApiClient<HTXCredentials>
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IHTXRestClientUsdtFuturesV5ApiAccount"/>
        IHTXRestClientUsdtFuturesV5ApiAccount Account { get; }
        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IHTXRestClientUsdtFuturesV5ApiExchangeData"/>
        IHTXRestClientUsdtFuturesV5ApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IHTXRestClientUsdtFuturesV5ApiTrading"/>
        IHTXRestClientUsdtFuturesV5ApiTrading Trading { get; }
    }
}
