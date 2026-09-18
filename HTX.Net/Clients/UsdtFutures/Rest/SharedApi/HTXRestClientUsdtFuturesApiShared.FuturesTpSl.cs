using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXRestClientUsdtFuturesSharedApi
    {
        #region Set Futures Tp Sl

        async Task<IExchangeCallResult<SharedId>> ISetFuturesTpSl.SetFuturesTpSlAsync(SetTpSlRequest request, CancellationToken ct)
            => await SetFuturesTpSlAsync(request, ct).ConfigureAwait(false);

        public SetFuturesTpSlOptions SetFuturesTpSlOptions { get; } = new SetFuturesTpSlOptions(_exchangeName, true)
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<SetTpSlRequest>.Required(x => x.PositionMode),
                RequestParameterRuleOverride<SetTpSlRequest>.Required(x => x.MarginMode),
                RequestParameterRuleOverride<SetTpSlRequest>.Required(x => x.Quantity)
            ]
        };

        public async Task<HttpResult<SharedId>> SetFuturesTpSlAsync(SetTpSlRequest request, CancellationToken ct)
        {
            var validationError = SetFuturesTpSlOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            HttpResult<HTXTpSlResult> result;
            if (request.MarginMode == SharedMarginMode.Cross)
            {
                result = await _api.Trading.SetCrossMarginTpSlAsync(
                    request.PositionSide == SharedPositionSide.Long ? OrderSide.Sell: OrderSide.Buy,
                    request.Quantity!.Value,
                    request.Symbol!.GetSymbol(FormatSymbol),
                    takeProfitTriggerPrice: request.TpSlSide == SharedTpSlSide.TakeProfit ? request.TriggerPrice : null,
                    takeProfitOrderPriceType: request.TpSlSide == SharedTpSlSide.TakeProfit ? OrderPriceType.Optimal20 : null,
                    stopLossTriggerPrice: request.TpSlSide == SharedTpSlSide.StopLoss ? request.TriggerPrice : null,
                    stopLossOrderPriceType: request.TpSlSide == SharedTpSlSide.StopLoss ? OrderPriceType.Optimal20 : null,
                    ct: ct).ConfigureAwait(false);
            }
            else
            {
                result = await _api.Trading.SetIsolatedMarginTpSlAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.PositionSide == SharedPositionSide.Long ? OrderSide.Sell : OrderSide.Buy,
                    request.Quantity!.Value,
                    takeProfitTriggerPrice: request.TpSlSide == SharedTpSlSide.TakeProfit ? request.TriggerPrice : null,
                    takeProfitOrderPriceType: request.TpSlSide == SharedTpSlSide.TakeProfit ? OrderPriceType.Optimal20 : null,
                    stopLossTriggerPrice: request.TpSlSide == SharedTpSlSide.StopLoss ? request.TriggerPrice : null,
                    stopLossOrderPriceType: request.TpSlSide == SharedTpSlSide.StopLoss ? OrderPriceType.Optimal20 : null,
                    ct: ct).ConfigureAwait(false);
            }

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.TpOrder?.OrderIdStr ?? result.Data.SlOrder!.OrderIdStr));
        }

        #endregion
        #region Cancel Futures Tp Sl

        async Task<IExchangeCallResult<bool>> ICancelFuturesTpSl.CancelFuturesTpSlAsync(CancelTpSlRequest request, CancellationToken ct)
            => await CancelFuturesTpSlAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesTpSlOptions CancelFuturesTpSlOptions { get; } = new CancelFuturesTpSlOptions(_exchangeName, true)
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<CancelTpSlRequest>.Required(x => x.OrderId),
                RequestParameterRuleOverride<CancelTpSlRequest>.Required(x => x.MarginMode)
            ]
        };

        public async Task<HttpResult<bool>> CancelFuturesTpSlAsync(CancelTpSlRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTpSlOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<bool>(Exchange, validationError);

            HttpResult<HTXTriggerOrderResult> result;
            if (request.MarginMode == SharedMarginMode.Cross)
            {
                result = await _api.Trading.CancelCrossMarginTpSlAsync(
                    request.OrderId!,
                    request.Symbol!.GetSymbol(FormatSymbol),
                    ct: ct).ConfigureAwait(false);
            }
            else
            {
                result = await _api.Trading.CancelIsolatedMarginTpSlAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.OrderId!,
                    ct: ct).ConfigureAwait(false);
            }
            if (!result.Success)
                return HttpResult.Fail<bool>(result);

            // Return
            return HttpResult.Ok(result, true);
        }

        #endregion
    }
}
