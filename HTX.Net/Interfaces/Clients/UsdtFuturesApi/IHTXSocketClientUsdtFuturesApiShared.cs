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
}
