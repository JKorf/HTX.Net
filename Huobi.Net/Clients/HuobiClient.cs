using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Huobi.Net.Clients.FuturesApi;
using Huobi.Net.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Internal;
using Newtonsoft.Json.Linq;

namespace Huobi.Net.Clients
{
    /// <inheritdoc cref="IHuobiClient" />
    public class HuobiClient : BaseRestClient, IHuobiClient
    {
        #region Api clients

        /// <inheritdoc />
        public IHuobiClientSpotApi SpotApi { get; }

        /// <inheritdoc />
        public IHuobiClientUsdtMarginSwapApi UsdtMarginSwapApi { get; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of HuobiClient using the default options
        /// </summary>
        public HuobiClient() : this(HuobiClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of the HuobiClient with the provided options
        /// </summary>
        public HuobiClient(HuobiClientOptions options) : base("Huobi", options)
        {
            SpotApi = AddApiClient(new HuobiClientSpotApi(log, options));
            UsdtMarginSwapApi = AddApiClient(new HuobiClientUsdtMarginSwapApi(log, options));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(HuobiClientOptions options)
        {
            HuobiClientOptions.Default = options;
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
