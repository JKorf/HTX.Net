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

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IHTXRestClientUsdtFuturesSharedApi :
        IGetBalancesRest,
        IGetFuturesTickerRest,
        IGetAllFuturesTickersRest,
        IGetFuturesSymbolsRest,
        IPlaceFuturesOrderRest,
        IGetFuturesOrderRest,
        IGetOpenFuturesOrdersRest,
        IGetClosedFuturesOrdersRest,
        ICancelFuturesOrderRest,
        IGetFuturesOrderTradesRest,
        IGetFuturesUserTradeHistoryRest,
        IGetPositionsRest,
        IClosePositionRest,
        IGetKlinesRest,
        IGetMarkPriceKlinesRest,
        IGetIndexPriceKlinesRest,
        IGetOrderBookRest,
        IGetRecentTradesRest,
        IGetFundingRateHistoryRest,
        IGetOpenInterestRest,
        IGetPositionModeRest,
        ISetPositionModeRest,
        IGetFeesRest,
        IGetFuturesOrderByClientOrderIdRest,
        ICancelFuturesOrderByClientOrderIdRest,
        IPlaceFuturesTriggerOrderRest,
        IGetFuturesTriggerOrderRest,
        ICancelFuturesTriggerOrderRest,
        ISetFuturesTpSlRest,
        ICancelFuturesTpSlRest,
        IGetBookTickerRest
    {
    }
}
