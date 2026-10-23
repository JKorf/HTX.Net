using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Enums;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    internal partial class HTXSocketClientUsdtFuturesV5SharedApi
    {
        #region Subscribe Balances

        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToAccountUpdatesAsync(
                update => handler(update.ToType<SharedBalance[]>(update.Data.Data.Details.Select(x => 
                    new SharedBalance(
                        SupportedTradingModes, 
                        x.Currency,
                        x.AvailableMargin,
                        x.Equity) ).ToArray())),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
