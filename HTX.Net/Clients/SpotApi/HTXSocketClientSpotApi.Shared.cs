using HTX.Net;
using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.SubscribeModels;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXSocketClientSpotApi : IHTXSocketClientSpotApiShared
    {
        public string Exchange => HTXExchange.ExchangeName;

        async Task<CallResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickerUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedTicker>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToTickerUpdatesAsync(update =>  handler(update.As(update.Data.Select(x => new SharedTicker
            {
                Symbol = x.Symbol,
                HighPrice = x.HighPrice ?? 0,
                LastPrice = x.ClosePrice ?? 0,
                LowPrice = x.LowPrice ?? 0
            }))) , ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(TickerSubscribeRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.As(new SharedTicker
            {
                Symbol = update.Data.Symbol,
                HighPrice = update.Data.HighPrice ?? 0,
                LastPrice = update.Data.LastTradePrice,
                LowPrice = update.Data.LowPrice ?? 0
            })), ct).ConfigureAwait(false);

            return result;
        }
    }
}
