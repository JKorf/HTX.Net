using CryptoExchange.Net.SharedApis;
using HTX.Net.Interfaces.Clients;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;
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
        public IHTXRestClientUsdtFuturesV5SharedApi UsdtFuturesV5Rest { get; }
        /// <inheritdoc />
        public IHTXSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IHTXSocketClientUsdtFuturesSharedApi UsdtFuturesSocket { get; }
        /// <inheritdoc />
        public IHTXSocketClientUsdtFuturesV5SharedApi UsdtFuturesV5Socket { get; }

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
                restClient.UsdtFuturesV5Api.SharedApi,
                socketClient.SpotApi.SharedApi,
                socketClient.UsdtFuturesApi.SharedApi,
                socketClient.UsdtFuturesV5Api.SharedApi)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            UsdtFuturesRest = restClient.UsdtFuturesApi.SharedApi;
            UsdtFuturesV5Rest = restClient.UsdtFuturesV5Api.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            UsdtFuturesSocket = socketClient.UsdtFuturesApi.SharedApi;
            UsdtFuturesV5Socket = socketClient.UsdtFuturesV5Api.SharedApi;
        }
    }
}
