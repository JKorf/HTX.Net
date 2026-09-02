using CryptoExchange.Net.Interfaces.Clients;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot API endpoints
    /// </summary>
    public interface IHTXRestClientSpotApi : IRestApiClient<HTXCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IHTXRestClientSpotApiAccount"/>
        IHTXRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IHTXRestClientSpotApiExchangeData"/>
        IHTXRestClientSpotApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Endpoints related to margin
        /// </summary>
        /// <see cref="IHTXRestClientSpotApiMargin"/>
        IHTXRestClientSpotApiMargin Margin { get; }
        /// <summary>
        /// Endpoints related to sub-accounts
        /// </summary>
        /// <see cref="IHTXRestClientSpotApiSubAccount"/>
        IHTXRestClientSpotApiSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IHTXRestClientSpotApiTrading"/>
        IHTXRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// [V1] Get the shared rest requests client. For new implementations prefer <see cref="SharedApi"/>
        /// </summary>
        public IHTXRestClientSpotApiShared SharedClient { get; }

        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IHTXRestClientSpotSharedApi SharedApi { get; }
    }
}