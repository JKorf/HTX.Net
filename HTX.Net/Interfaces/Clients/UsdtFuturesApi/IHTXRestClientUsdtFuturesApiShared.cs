using CryptoExchange.Net.SharedApis;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Usdt futures rest API usage
    /// </summary>
    public interface IHTXRestClientUsdtFuturesApiShared :
        IBalanceRestClient,
        IFuturesTickerRestClient,
        IFuturesSymbolRestClient,
        IFuturesOrderRestClient,
        IKlineRestClient,
        IMarkPriceKlineRestClient,
        IIndexPriceKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        IFundingRateRestClient,
        IOpenInterestRestClient,
        IPositionModeRestClient,
        IFeeRestClient,
        IFuturesOrderClientIdRestClient,
        IFuturesTriggerOrderRestClient,
        IFuturesTpSlRestClient,
        IBookTickerRestClient
    {
    }
}
