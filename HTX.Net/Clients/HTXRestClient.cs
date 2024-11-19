using HTX.Net.Clients.SpotApi;
using HTX.Net.Interfaces.Clients;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Clients.UsdtFutures;
using Microsoft.Extensions.Options;

namespace HTX.Net.Clients
{
    /// <inheritdoc cref="IHTXRestClient" />
    public class HTXRestClient : BaseRestClient, IHTXRestClient
    {
        #region Api clients

        /// <inheritdoc />
        public IHTXRestClientSpotApi SpotApi { get; }

        /// <inheritdoc />
        public IHTXRestClientUsdtFuturesApi UsdtFuturesApi { get; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of the HTXRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public HTXRestClient(Action<HTXRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the HTXRestClient
        /// </summary>
        /// <param name="options">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public HTXRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<HTXRestOptions> options)
            : base(loggerFactory, "HTX")
        {
            Initialize(options.Value);

            SpotApi = AddApiClient(new HTXRestClientSpotApi(_logger, httpClient, options.Value));
            UsdtFuturesApi = AddApiClient(new HTXRestClientUsdtFuturesApi(_logger, httpClient, options.Value));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<HTXRestOptions> optionsDelegate)
        {
            HTXRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials apiCredentials)
        {
            SpotApi.SetApiCredentials(apiCredentials);
            UsdtFuturesApi.SetApiCredentials(apiCredentials);
        }
        #endregion
    }
}
