using CryptoExchange.Net.Authentication;
using HTX.Net.Clients.FuturesApi;
using HTX.Net.Clients.SpotApi;
using HTX.Net.Interfaces.Clients;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using HTX.Net.Objects.Options;
using CryptoExchange.Net.Clients;

namespace HTX.Net.Clients
{
    /// <inheritdoc cref="IHTXRestClient" />
    public class HTXRestClient : BaseRestClient, IHTXRestClient
    {
        #region Api clients

        /// <inheritdoc />
        public IHTXRestClientSpotApi SpotApi { get; }

        /// <inheritdoc />
        public IHTXRestClientUsdtMarginSwapApi UsdtMarginSwapApi { get; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of the HTXRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public HTXRestClient(Action<HTXRestOptions>? optionsDelegate = null) : this(null, null, optionsDelegate)
        {
        }

        /// <summary>
        /// Create a new instance of the HTXRestClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public HTXRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, Action<HTXRestOptions>? optionsDelegate = null)
            : base(loggerFactory, "HTX")
        {
            var options = HTXRestOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            SpotApi = AddApiClient(new HTXRestClientSpotApi(_logger, httpClient, options));
            UsdtMarginSwapApi = AddApiClient(new HTXRestClientUsdtMarginSwapApi(_logger, httpClient, options));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<HTXRestOptions> optionsDelegate)
        {
            var options = HTXRestOptions.Default.Copy();
            optionsDelegate(options);
            HTXRestOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials apiCredentials)
        {
            SpotApi.SetApiCredentials(apiCredentials);
            UsdtMarginSwapApi.SetApiCredentials(apiCredentials);
        }
        #endregion
    }
}
