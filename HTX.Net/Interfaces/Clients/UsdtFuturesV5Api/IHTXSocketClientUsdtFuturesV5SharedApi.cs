using CryptoExchange.Net.SharedApis;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesV5Api
{
    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IHTXSocketClientUsdtFuturesV5SharedApi
        : ISubscribeBalancesSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribeUserTradesSocket,
        ISubscribePositionsSocket,
        IPlaceFuturesOrderSocket,
        ICancelFuturesOrderSocket,
        ICancelFuturesOrderByClientOrderIdSocket,
        ICancelAllFuturesSymbolOrdersSocket,
        ICancelAllFuturesOrdersSocket
    {
    }
}
