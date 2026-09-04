using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Enums;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXSocketClientUsdtFuturesSharedApi
    {
        #region Subscribe Positions

        public SubscribePositionOptions SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.SubscribeToCrossMarginPositionUpdatesAsync(
                update => handler(update.ToType(update.Data.Data.Select(x =>
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode), 
                        x.ContractCode,
                        new SharedOrderQuantity(contractQuantity: x.Quantity), 
                        update.Data.Timestamp)
                    {
                        AverageOpenPrice = x.PositionPrice,
                        PositionMode = x.PositionMode == PositionMode.SingleSide ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                        PositionSide = x.OrderSide == Enums.OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long,
                        Leverage = x.LeverageRate,
                        UnrealizedPnl = x.UnrealizedPnl
                    }).ToArray())),
                ct: ct).ConfigureAwait(false);
                return result;
            }
            else
            {
                var result = await _api.SubscribeToIsolatedMarginPositionUpdatesAsync(
                update => handler(update.ToType(update.Data.Data.Select(x =>
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode),
                        x.ContractCode,
                        new SharedOrderQuantity(contractQuantity: x.Quantity),
                        update.Data.Timestamp)
                    {
                        AverageOpenPrice = x.PositionPrice,
                        PositionMode = x.PositionMode == PositionMode.SingleSide ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                        PositionSide = x.OrderSide == Enums.OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long,
                        Leverage = x.LeverageRate,
                        UnrealizedPnl = x.UnrealizedPnl
                    }).ToArray())),
                ct: ct).ConfigureAwait(false);
                return result;
            }
        }

        #endregion
    }
}
