using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using Huobi.Net.Clients.FuturesApi;
using Huobi.Net.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using Huobi.Net.Objects.Options;

namespace Huobi.Net.Clients
{
    /// <inheritdoc cref="IHuobiRestClient" />
    public class HuobiRestClient : BaseRestClient, IHuobiRestClient
    {
        #region Api clients

        /// <inheritdoc />
        public IHuobiClientSpotApi SpotApi { get; }

        /// <inheritdoc />
        public IHuobiClientUsdtMarginSwapApi UsdtMarginSwapApi { get; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of the HuobiRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public HuobiRestClient(Action<HuobiRestOptions> optionsDelegate) : this(null, null, optionsDelegate)
        {
        }

        /// <summary>
        /// Create a new instance of the HuobiRestClient using default options
        /// </summary>
        public HuobiRestClient(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) : this(httpClient, loggerFactory, null)
        {
        }

        /// <summary>
        /// Create a new instance of the HuobiRestClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public HuobiRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, Action<HuobiRestOptions>? optionsDelegate = null)
            : base(loggerFactory, "Huobi")
        {
            var options = HuobiRestOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            SpotApi = AddApiClient(new HuobiRestClientSpotApi(_logger, httpClient, options));
            UsdtMarginSwapApi = AddApiClient(new HuobiClientUsdtMarginSwapApi(_logger, httpClient, options));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<HuobiRestOptions> optionsDelegate)
        {
            var options = HuobiRestOptions.Default.Copy();
            optionsDelegate(options);
            HuobiRestOptions.Default = options;
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
