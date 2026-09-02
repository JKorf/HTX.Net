using CryptoExchange.Net.SharedApis;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Shared interface for Usdt futures socket API usage
    /// </summary>
    public interface IHTXSocketClientUsdtFuturesApiShared :
        ITickerSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        IBalanceSocketClient,
        IFuturesOrderSocketClient,
        IUserTradeSocketClient,
        IPositionSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IHTXSocketClientUsdtFuturesSharedApi :
        ISubscribeTickerSocket,
        ISubscribeTradesSocket,
        ISubscribeBookTickerSocket,
        ISubscribeOrderBookSocket,
        ISubscribeKlinesSocket,
        ISubscribeBalancesSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribeUserTradesSocket,
        ISubscribePositionsSocket
    { 
    }
}
