using CryptoExchange.Net.SharedApis;
using HTX.Net.Interfaces.Clients;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Objects.Options;
using Microsoft.Extensions.Options;

namespace HTX.Net.Clients
{
    /// <inheritdoc />
    public class HTXSharedApiClient : SharedApiClientBase, IHTXSharedApiClient
    {
        /// <inheritdoc />
        public IHTXRestClientSpotSharedApi SpotRest { get; }
        /// <inheritdoc />
        public IHTXRestClientUsdtFuturesSharedApi UsdtFuturesRest { get; }
        /// <inheritdoc />
        public IHTXSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IHTXSocketClientUsdtFuturesSharedApi UsdtFuturesSocket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public HTXSharedApiClient(
            IHTXRestClient restClient,
            IHTXSocketClient socketClient,
            IOptions<HTXOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                restClient.SpotApi.SharedApi,
                restClient.UsdtFuturesApi.SharedApi,
                socketClient.SpotApi.SharedApi,
                socketClient.UsdtFuturesApi.SharedApi)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            UsdtFuturesRest = restClient.UsdtFuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            UsdtFuturesSocket = socketClient.UsdtFuturesApi.SharedApi;
        }
    }
}
