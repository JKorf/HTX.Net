using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesV5Api
{
    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IHTXRestClientUsdtFuturesV5SharedApi
        : IGetBalancesRest,
        IGetFundingInfoRest,
        IGetFundingRateHistoryRest,
        IPlaceFuturesOrderRest,
        IGetFuturesOrderRest,
        IGetFuturesOrderByClientOrderIdRest,
        IGetOpenFuturesOrdersRest,
        IGetClosedFuturesOrdersRest,
        IGetFuturesOrderTradesRest,
        ICancelFuturesOrderRest,
        ICancelFuturesOrderByClientOrderIdRest,
        ICancelAllFuturesSymbolOrdersRest,
        ICancelAllFuturesOrdersRest,
        IGetPositionsRest,
        IGetPositionModeRest,
        ISetPositionModeRest,
        IGetLeverageRest,
        ISetLeverageRest,
        IGetLeverageTiersRest
    {
    }
}
