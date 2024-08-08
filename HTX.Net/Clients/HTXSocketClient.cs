using CryptoExchange.Net.Clients;
using HTX.Net.Clients.SpotApi;
using HTX.Net.Clients.UsdtFutures;
using HTX.Net.Interfaces.Clients;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Objects.Options;

namespace HTX.Net.Clients
{
    /// <inheritdoc cref="IHTXSocketClient" />
    public class HTXSocketClient : BaseSocketClient, IHTXSocketClient
    {
        #region fields
        /// <inheritdoc />
        public IHTXSocketClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IHTXSocketClientUsdtFuturesApi UsdtFuturesApi { get; }
        #endregion

        #region ctor
        /// <summary>
        /// Create a new instance of the HTXSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        public HTXSocketClient(ILoggerFactory? loggerFactory = null) : this((x) => { }, loggerFactory)
        {
        }

        /// <summary>
        /// Create a new instance of the HTXSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public HTXSocketClient(Action<HTXSocketOptions> optionsDelegate) : this(optionsDelegate, null)
        {
        }

        /// <summary>
        /// Create a new instance of the HTXSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public HTXSocketClient(Action<HTXSocketOptions> optionsDelegate, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "HTX")
        {
            var options = HTXSocketOptions.Default.Copy();
            optionsDelegate(options);
            Initialize(options);

            SpotApi = AddApiClient(new HTXSocketClientSpotApi(_logger, options));
            UsdtFuturesApi = AddApiClient(new HTXSocketClientUsdtFuturesApi(_logger, options));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<HTXSocketOptions> optionsDelegate)
        {
            var options = HTXSocketOptions.Default.Copy();
            optionsDelegate(options);
            HTXSocketOptions.Default = options;
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
