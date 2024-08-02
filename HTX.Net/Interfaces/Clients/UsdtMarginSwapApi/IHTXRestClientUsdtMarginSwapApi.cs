namespace HTX.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Usdt margin swap api endpoints
    /// </summary>
    public interface IHTXRestClientUsdtMarginSwapApi : IRestApiClient
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IHTXRestClientUsdtMarginSwapApiAccount Account { get; }
        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IHTXRestClientUsdtMarginSwapApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IHTXRestClientUsdtMarginSwapApiTrading Trading { get; }
    }
}