using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    internal partial class HTXRestClientUsdtFuturesV5SharedApi
    {
        #region Get Positions

        async Task<IExchangeCallResult<SharedPosition[]>> IGetPositions.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
            => await GetPositionsAsync(request, ct).ConfigureAwait(false);

        public GetPositionsOptions GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true);
        public async Task<HttpResult<SharedPosition[]>> GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);

            var result = await _api.Account.GetOpenPositionsAsync(contractCode: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPosition[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x =>
                new SharedPosition(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode),
                    x.ContractCode,
                    new SharedOrderQuantity(contractQuantity: x.Quantity),
                    default)
                {
                    UnrealizedPnl = x.UnrealizedPnl,
                    AverageOpenPrice = x.OpenAveragePrice,
                    Leverage = x.LeverageRate,
                    PositionSide = x.PositionSide == FuturesPositionSide.Both ? default : x.PositionSide == FuturesPositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                    LiquidationPrice = x.LiquidationPrice,
                    UpdateTime = x.UpdateTime
                }).ToArray());
            
        }

        #endregion
    }
}
