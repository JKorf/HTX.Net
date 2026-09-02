using CryptoExchange.Net.SharedApis;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot socket API usage
    /// </summary>
    public interface IHTXSocketClientSpotApiShared :
        ITickerSocketClient,
        ITickersSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        IBalanceSocketClient,
        ISpotOrderSocketClient,
        IUserTradeSocketClient,
        ISpotOrderManagementSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IHTXSocketClientSpotSharedApi :
        ISubscribeTickerSocket,
        ISubscribeAllTickersSocket,
        ISubscribeTradesSocket,
        ISubscribeBookTickerSocket,
        ISubscribeKlinesSocket,
        ISubscribeOrderBookSocket,
        ISubscribeBalancesSocket,
        ISubscribeSpotOrdersSocket,
        ISubscribeUserTradesSocket,
        IPlaceSpotOrderSocket,
        ICancelSpotOrderSocket
    {
    }
}
