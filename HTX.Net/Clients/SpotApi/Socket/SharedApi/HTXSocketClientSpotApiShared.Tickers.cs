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
        #region Subscribe All Tickers

        async Task<WebSocketResult<UpdateSubscription>> ISubscribeAllTickersSocket.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedTicker[]>> handler, CancellationToken ct)
            => await SubscribeToAllTickersUpdatesAsync(request, x => handler(x.ToType<SharedTicker[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickersOptions SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions(_exchangeName);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToTickerUpdatesAsync(update => handler(update.ToType<SharedSpotTicker[]>(update.Data.Select(x => 
            new SharedSpotTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol), 
                x.Symbol,
                x.ClosePrice ?? 0, 
                x.HighPrice ?? 0,
                x.LowPrice ?? 0,
                new SharedOrderQuantity(x.Volume, x.QuoteVolume), 
                (x.OpenPrice == null || x.OpenPrice == 0) ? null : Math.Round((x.ClosePrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))
            {
            }).ToArray())), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
        #region Subscribe Ticker

        async Task<WebSocketResult<UpdateSubscription>> ISubscribeTickerSocket.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
            => await SubscribeToTickerUpdatesAsync(request, x => handler(x.ToType<SharedTicker>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickerOptions SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToTickerUpdatesAsync(symbol, update => handler(update.ToType(
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol),
                    symbol,
                    update.Data.LastTradePrice, 
                    update.Data.HighPrice ?? 0,
                    update.Data.LowPrice ?? 0,
                    new SharedOrderQuantity(update.Data.Volume, update.Data.QuoteVolume),
                    (update.Data.OpenPrice == null || update.Data.OpenPrice == 0) ? null : Math.Round((update.Data.ClosePrice ?? 0) / update.Data.OpenPrice.Value * 100 - 100, 2))
            {
            })), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
