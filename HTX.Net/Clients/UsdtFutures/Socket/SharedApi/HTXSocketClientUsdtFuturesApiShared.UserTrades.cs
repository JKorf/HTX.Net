using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Enums;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXSocketClientUsdtFuturesSharedApi
    {
        #region User Trade client
        public SubscribeUserTradeOptions SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.SubscribeToCrossMarginUserTradeUpdatesAsync(
                update => {
                    handler(update.ToType<SharedUserTrade[]>(update.Data.Trades.Select(x =>
                                    new SharedUserTrade(
                                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.ContractCode),
                                        update.Data.ContractCode,
                                        update.Data.OrderId.ToString(),
                                        x.ToString(),
                                        update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                        new SharedOrderQuantity(contractQuantity: x.Quantity),
                                        x.Price,
                                        x.CreateTime)
                                    {
                                        Role = x.Role == Enums.OrderRole.Taker ? SharedRole.Taker : SharedRole.Maker,
                                    }
                    ).ToArray()));
                },
                ct: ct).ConfigureAwait(false);
                return result;
            }
            else
            {
                var result = await _api.SubscribeToIsolatedMarginUserTradeUpdatesAsync(
                update => {
                    handler(update.ToType<SharedUserTrade[]>(update.Data.Trades.Select(x =>
                                    new SharedUserTrade(
                                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.ContractCode),
                                        update.Data.ContractCode,
                                        update.Data.OrderId.ToString(),
                                        x.ToString(),
                                        update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                        new SharedOrderQuantity(contractQuantity: x.Quantity),
                                        x.Price,
                                        x.CreateTime)
                                    {
                                        Role = x.Role == Enums.OrderRole.Taker ? SharedRole.Taker : SharedRole.Maker,
                                    }
                    ).ToArray()));
                },
                ct: ct).ConfigureAwait(false);
                return result;
            }
        }
        #endregion
    }
}
