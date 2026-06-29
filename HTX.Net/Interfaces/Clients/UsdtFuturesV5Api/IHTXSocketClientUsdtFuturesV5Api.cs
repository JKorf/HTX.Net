using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Objects.Models.UsdtFuturesV5;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesV5Api
{
    /// <summary>
    /// Usdt futures V5 streams
    /// </summary>
    public interface IHTXSocketClientUsdtFuturesV5Api : ISocketApiClient<HTXCredentials>
    {
        /// <summary>
        /// Subscribe to account updates
        /// </summary>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HTXDataEventV5<HTXAccountUpdateV5>>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to order updates
        /// </summary>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? contractCode, Action<DataEvent<HTXDataEventV5<HTXOrderUpdateV5>>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to position updates
        /// </summary>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string? contractCode, Action<DataEvent<HTXDataEventV5<HTXPositionUpdateV5[]>>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to user trade updates
        /// </summary>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string? contractCode, Action<DataEvent<HTXDataEventV5<HTXMatchOrderUpdateV5[]>>> onData, CancellationToken ct = default);
    }
}
