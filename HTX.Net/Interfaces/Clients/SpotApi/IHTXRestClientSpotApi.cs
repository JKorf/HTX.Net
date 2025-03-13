namespace HTX.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot API endpoints
    /// </summary>
    public interface IHTXRestClientSpotApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IHTXRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IHTXRestClientSpotApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Endpoints related to margin
        /// </summary>
        IHTXRestClientSpotApiMargin Margin { get; }
        /// <summary>
        /// Endpoints related to sub-accounts
        /// </summary>
        IHTXRestClientSpotApiSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IHTXRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IHTXRestClientSpotApiShared SharedClient { get; }
    }
}