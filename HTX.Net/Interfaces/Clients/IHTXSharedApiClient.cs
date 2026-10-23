using CryptoExchange.Net.SharedApis;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;

namespace HTX.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of HTX
    /// </summary>
    public interface IHTXSharedApiClient : ISharedApiClientBase
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
        /// USDT Futures REST shared V5 API implementations
        /// </summary>
        IHTXRestClientUsdtFuturesV5SharedApi UsdtFuturesV5Rest { get; }

        /// <summary>
        /// Spot WebSocket shared API implementations
        /// </summary>
        IHTXSocketClientSpotSharedApi SpotSocket { get; }

        /// <summary>
        /// USDT Futures WebSocket shared API implementations
        /// </summary>
        IHTXSocketClientUsdtFuturesSharedApi UsdtFuturesSocket { get; }

        /// <summary>
        /// USDT Futures WebSocket shared V5 API implementations
        /// </summary>
        IHTXSocketClientUsdtFuturesV5SharedApi UsdtFuturesV5Socket { get; }
    }
}
