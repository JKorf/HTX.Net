using CryptoExchange.Net.SharedApis.Interfaces.Rest;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Futures;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
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
        IPositionModeRestClient
    {
    }
}
