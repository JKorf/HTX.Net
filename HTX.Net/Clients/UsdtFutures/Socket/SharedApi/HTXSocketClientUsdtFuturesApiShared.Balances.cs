using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Enums;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXSocketClientUsdtFuturesSharedApi
    {
        #region Balance client
        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.SubscribeToCrossMarginBalanceUpdatesAsync(
                    update => handler(update.ToType<SharedBalance[]>(update.Data.Data.Select(x => 
                        new SharedBalance(
                            SupportedTradingModes, 
                            x.MarginAsset,
                            x.WithdrawAvailable,
                            x.MarginBalance) ).ToArray())),
                    ct: ct).ConfigureAwait(false);

                return result;
            }
            else
            {
                var result = await _api.SubscribeToIsolatedMarginBalanceUpdatesAsync(
                    update => handler(update.ToType<SharedBalance[]>(update.Data.Data.Select(x =>
                        new SharedBalance(
                            SupportedTradingModes,
                            "USDT",
                            x.WithdrawAvailable,
                            x.MarginBalance) { IsolatedMarginSymbol = x.MarginAccount }).ToArray())),
                    ct: ct).ConfigureAwait(false);

                return result;
            }

        }
        #endregion
    }
}
