using HTX.Net.Interfaces.Clients;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;

namespace HTX.Net.Clients
{
    /// <inheritdoc />
    public class HTXSharedApiClient : IHTXSharedApiClient
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
            IHTXSocketClient socketClient)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            UsdtFuturesRest = restClient.UsdtFuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            UsdtFuturesSocket = socketClient.UsdtFuturesApi.SharedApi;
        }
    }
}
