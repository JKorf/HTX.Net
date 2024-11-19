using CryptoExchange.Net.Clients;
using HTX.Net.Clients.SpotApi;
using HTX.Net.Clients.UsdtFutures;
using HTX.Net.Interfaces.Clients;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Objects.Options;
using Microsoft.Extensions.Options;

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
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public HTXSocketClient(Action<HTXSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of the HTXSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public HTXSocketClient(IOptions<HTXSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "HTX")
        {
            Initialize(options.Value);

            SpotApi = AddApiClient(new HTXSocketClientSpotApi(_logger, options.Value));
            UsdtFuturesApi = AddApiClient(new HTXSocketClientUsdtFuturesApi(_logger, options.Value));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<HTXSocketOptions> optionsDelegate)
        {
            HTXSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
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
