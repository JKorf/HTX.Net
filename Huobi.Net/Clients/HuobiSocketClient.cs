using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using Huobi.Net.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using System;

namespace Huobi.Net.Clients
{
    /// <inheritdoc cref="IHuobiSocketClient" />
    public class HuobiSocketClient : BaseSocketClient, IHuobiSocketClient
    {
        #region fields
        /// <inheritdoc />
        public IHuobiSocketClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IHuobiSocketClientUsdtMarginSwapApi UsdtMarginSwapApi { get; }
        #endregion

        #region ctor
        /// <summary>
        /// Create a new instance of the HuobiSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        public HuobiSocketClient(ILoggerFactory? loggerFactory = null) : this((x) => { }, loggerFactory)
        {
        }

        /// <summary>
        /// Create a new instance of the HuobiSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public HuobiSocketClient(Action<HuobiSocketOptions> optionsDelegate) : this(optionsDelegate, null)
        {
        }

        /// <summary>
        /// Create a new instance of the HuobiSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public HuobiSocketClient(Action<HuobiSocketOptions> optionsDelegate, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Huobi")
        {
            var options = HuobiSocketOptions.Default.Copy();
            optionsDelegate(options);
            Initialize(options);

            SpotApi = AddApiClient(new HuobiSocketClientSpotApi(_logger, options));
            UsdtMarginSwapApi = AddApiClient(new HuobiSocketClientUsdtMarginSwapApi(_logger, options));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<HuobiSocketOptions> optionsDelegate)
        {
            var options = HuobiSocketOptions.Default.Copy();
            optionsDelegate(options);
            HuobiSocketOptions.Default = options;
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
