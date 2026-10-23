using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtFuturesV5;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    internal partial class HTXSocketClientUsdtFuturesV5SharedApi
    {
        #region Subscribe User Trades

        public SubscribeUserTradeOptions SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserTradeUpdatesAsync(
                null,
                update => {
                    var data = update.Data.Data.Where(x => x.TradeId != null);

                    handler(update.ToType<SharedUserTrade[]>(data.Select(x =>
                                new SharedUserTrade(
                                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.ContractCode),
                                    update.Data.ContractCode!,
                                    x.OrderId,
                                    x.TradeId,
                                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                    new SharedOrderQuantity(quoteAssetQuantity: x.TradeTurnover, contractQuantity: x.Quantity),
                                    x.Price ?? 0,
                                    x.CreateTime)
                                {
                                    Role = x.Role == Enums.OrderRole.Taker ? SharedRole.Taker : SharedRole.Maker,
                                }
                ).ToArray()));
            },
            ct: ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
