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
using HTX.Net.Objects.Models.Socket;

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

        async Task<CallResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedBalance>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToAccountUpdatesAsync(
                update => handler(update.As<IEnumerable<SharedBalance>>(new[] { new SharedBalance(update.Data.Asset, update.Data.Available ?? 0, update.Data.Balance ?? 0) })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToOrderUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedSpotOrder>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToOrderUpdatesAsync(null,
                update => handler(update.As<IEnumerable<SharedSpotOrder>>(new[] { ParseOrder(update.Data) })),
                update => handler(update.As<IEnumerable<SharedSpotOrder>>(new[] { ParseOrder(update.Data) })),
                update => handler(update.As<IEnumerable<SharedSpotOrder>>(new[] { ParseOrder(update.Data) })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ISpotUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedUserTrade>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToOrderDetailsUpdatesAsync(
                null,
                update => handler(update.As<IEnumerable<SharedUserTrade>>(new[] {
                    new SharedUserTrade(
                        update.Data.OrderId.ToString(),
                        update.Data.Id.ToString(),
                        update.Data.Quantity,
                        update.Data.Price,
                        update.Data.Timestamp)
                    {
                        Role = update.Data.IsTaker ? SharedRole.Taker : SharedRole.Maker,
                        Fee = update.Data.TransactionFee,
                        FeeAsset = update.Data.FeeAsset
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        public SharedSpotOrder ParseOrder(HTXOrderUpdate orderUpdate)
        {
            if (orderUpdate is HTXSubmittedOrderUpdate update)
            {
                return new SharedSpotOrder(
                            update.Symbol,
                            update.OrderId.ToString(),
                            update.Type == Enums.OrderType.Limit ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Limit : update.Type == Enums.OrderType.Market ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Market : CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Other,
                            update.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Buy : CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Sell,
                            SharedOrderStatus.Open,
                            update.CreateTime)
                {
                    ClientOrderId = update.ClientOrderId,
                    Quantity = update.Quantity,
                    QuantityFilled = 0,
                    QuoteQuantityFilled = 0,
                    UpdateTime = update.UpdateTime,
                    Price = update.Price,
                    Fee = 0
                };
            }
            if (orderUpdate is HTXMatchedOrderUpdate matchUpdate)
            {
                return new SharedSpotOrder(
                            matchUpdate.Symbol,
                            matchUpdate.OrderId.ToString(),
                            matchUpdate.Type == Enums.OrderType.Limit ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Limit : matchUpdate.Type == Enums.OrderType.Market ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Market : CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Other,
                            matchUpdate.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Buy : CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Sell,
                            matchUpdate.QuantityRemaining == 0 ? SharedOrderStatus.Filled : SharedOrderStatus.Open,
                            null)
                {
                    ClientOrderId = matchUpdate.ClientOrderId,
                    Quantity = matchUpdate.Type == Enums.OrderType.Market && matchUpdate.Side == Enums.OrderSide.Buy ? null : matchUpdate.Quantity,
                    QuantityFilled = matchUpdate.Type == Enums.OrderType.Market && matchUpdate.Side == Enums.OrderSide.Buy ? null : matchUpdate.Quantity - matchUpdate.QuantityRemaining,
                    QuoteQuantity = matchUpdate.Type == Enums.OrderType.Market && matchUpdate.Side == Enums.OrderSide.Buy ? matchUpdate.Quantity : null,
                    QuoteQuantityFilled = matchUpdate.Type == Enums.OrderType.Market && matchUpdate.Side == Enums.OrderSide.Buy ? matchUpdate.Quantity - matchUpdate.QuantityRemaining : null,
                    UpdateTime = matchUpdate.UpdateTime,
                    Price = matchUpdate.Price,
                    LastTrade = new SharedUserTrade(matchUpdate.OrderId.ToString(), matchUpdate.TradeId.ToString(), matchUpdate.TradeQuantity, matchUpdate.TradePrice, matchUpdate.TradeTime)
                    {
                        Role = matchUpdate.IsTaker ? SharedRole.Taker : SharedRole.Maker
                    }
                };
            }

            if (orderUpdate is HTXCanceledOrderUpdate cancelUpdate)
            {
                return new SharedSpotOrder(
                            cancelUpdate.Symbol,
                            cancelUpdate.OrderId.ToString(),
                            cancelUpdate.Type == Enums.OrderType.Limit ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Limit : cancelUpdate.Type == Enums.OrderType.Market ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Market : CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Other,
                            cancelUpdate.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Buy : CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Sell,
                            SharedOrderStatus.Canceled,
                            null)
                {
                    ClientOrderId = cancelUpdate.ClientOrderId,
                    Quantity = cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? null : cancelUpdate.Quantity,
                    QuantityFilled = cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? null : cancelUpdate.Quantity - cancelUpdate.QuantityRemaining,
                    QuoteQuantity = cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? cancelUpdate.Quantity : null,
                    QuoteQuantityFilled = cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? cancelUpdate.Quantity - cancelUpdate.QuantityRemaining : null,
                    UpdateTime = cancelUpdate.UpdateTime,
                    Price = cancelUpdate.Price
                };
            }

            throw new Exception("Unknown order update type");
        }
    }
}
