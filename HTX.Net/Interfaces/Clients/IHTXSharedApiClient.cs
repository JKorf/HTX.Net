using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;

namespace HTX.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of HTX
    /// </summary>
    public interface IHTXSharedApiClient
    {
        /// <summary>
        /// Spot REST shared API implementations
        /// </summary>
        IHTXRestClientSpotSharedApi SpotRest { get; }

        /// <summary>
        /// USDT Futures REST shared API implementations
        /// </summary>
        IHTXRestClientUsdtFuturesSharedApi UsdtFuturesRest { get; }

        /// <summary>
        /// Spot WebSocket shared API implementations
        /// </summary>
        IHTXSocketClientSpotSharedApi SpotSocket { get; }

        /// <summary>
        /// USDT Futures WebSocket shared API implementations
        /// </summary>
        IHTXSocketClientUsdtFuturesSharedApi UsdtFuturesSocket { get; }
    }
}
