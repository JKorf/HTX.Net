using HTX.Net;
using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
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
using CryptoExchange.Net.SharedApis.Models.Socket;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.SubscribeModels;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXSocketClientSpotApi : IHTXSocketClientSpotApiShared
    {
        public string Exchange => HTXExchange.ExchangeName;

        async Task<CallResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickerUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedTicker>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToTickerUpdatesAsync(update => handler(update.As(update.Data.Select(x => new SharedTicker(x.Symbol, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0))))).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(TickerSubscribeRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.As(new SharedTicker(symbol, update.Data.LastTradePrice, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0)))).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(TradeSubscribeRequest request, Action<DataEvent<IEnumerable<SharedTrade>>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.As(update.Data.Details.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)))), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(BookTickerSubscribeRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.As(new SharedBookTicker(update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return result;
        }
    }
}
