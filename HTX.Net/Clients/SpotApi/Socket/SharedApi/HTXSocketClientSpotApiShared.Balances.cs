using HTX.Net.Interfaces.Clients.SpotApi;
﻿using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.Socket;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXSocketClientSpotSharedApi
    {
        #region Subscribe Balances

        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToAccountUpdatesAsync(
                update => handler(update.ToType<SharedBalance[]>(new[] { 
                    new SharedBalance(
                        SupportedTradingModes,
                        update.Data.Asset, 
                        update.Data.Available ?? 0, 
                        update.Data.Balance ?? update.Data.Available ?? 0) })),
                2, ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
