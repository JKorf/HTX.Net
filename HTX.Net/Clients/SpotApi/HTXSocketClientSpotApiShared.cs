using HTX.Net.Interfaces.Clients.SpotApi;
﻿using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.Socket;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXSocketClientSpotApi : IHTXSocketClientSpotApiShared
    {
        private const string _topicId = "HTXSpot";
        public string Exchange => HTXExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Tickers client
        EndpointOptions<SubscribeAllTickersRequest> ITickersSocketClient.SubscribeAllTickersOptions { get; } = new EndpointOptions<SubscribeAllTickersRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<ExchangeEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = ((ITickersSocketClient)this).SubscribeAllTickersOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToTickerUpdatesAsync(update => handler(update.AsExchangeEvent<SharedSpotTicker[]>(Exchange, update.Data.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.Volume ?? 0, x.OpenPrice == null ? null : Math.Round((x.ClosePrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))).ToArray())), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Ticker client
        EndpointOptions<SubscribeTickerRequest> ITickerSocketClient.SubscribeTickerOptions { get; } = new EndpointOptions<SubscribeTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, update.Data.LastTradePrice, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0, update.Data.Volume ?? 0, update.Data.OpenPrice == null ? null : Math.Round((update.Data.ClosePrice ?? 0) / update.Data.OpenPrice.Value * 100 - 100, 2)))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Trade client

        EndpointOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new EndpointOptions<SubscribeTradeRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<ExchangeEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, update.Data.Details.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray())), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Book Ticker client

        EndpointOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new EndpointOptions<SubscribeBookTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<ExchangeEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.ThreeMinutes,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth);
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<ExchangeEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.AsExchangeEvent(Exchange, new SharedKline(update.Data.OpenTime, update.Data.ClosePrice ?? 0, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0, update.Data.OpenPrice ?? 0, update.Data.Volume ?? 0))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(false, new[] { 5, 10, 20 });
        async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<ExchangeEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToPartialOrderBookUpdates100MilisecondAsync(symbol, request.Limit ?? 20, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Balance client
        EndpointOptions<SubscribeBalancesRequest> IBalanceSocketClient.SubscribeBalanceOptions { get; } = new EndpointOptions<SubscribeBalancesRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<ExchangeEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToAccountUpdatesAsync(
                update => handler(update.AsExchangeEvent<SharedBalance[]>(Exchange, new[] { new SharedBalance(update.Data.Asset, update.Data.Available ?? 0, update.Data.Balance ?? update.Data.Available ?? 0) })),
                2, ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Spot Order client

        EndpointOptions<SubscribeSpotOrderRequest> ISpotOrderSocketClient.SubscribeSpotOrderOptions { get; } = new EndpointOptions<SubscribeSpotOrderRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<ExchangeEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
        {
            var validationError = ((ISpotOrderSocketClient)this).SubscribeSpotOrderOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);
            var result = await SubscribeToOrderUpdatesAsync(null,
                update => handler(update.AsExchangeEvent<SharedSpotOrder[]>(Exchange, new[] { ParseOrder(update.Data) })),
                update => handler(update.AsExchangeEvent<SharedSpotOrder[]>(Exchange, new[] { ParseOrder(update.Data) })),
                update => handler(update.AsExchangeEvent<SharedSpotOrder[]>(Exchange, new[] { ParseOrder(update.Data) })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region User Trade client
        EndpointOptions<SubscribeUserTradeRequest> IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new EndpointOptions<SubscribeUserTradeRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<ExchangeEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToOrderDetailsUpdatesAsync(
                null,
                update => handler(update.AsExchangeEvent<SharedUserTrade[]>(Exchange, new[] {
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        update.Data.Id.ToString(),
                        update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
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

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        public SharedSpotOrder ParseOrder(HTXOrderUpdate orderUpdate)
        {
            if (orderUpdate is HTXSubmittedOrderUpdate update)
            {
                return new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol),
                            update.Symbol,
                            update.OrderId.ToString(),
                            update.Type == Enums.OrderType.Limit ? SharedOrderType.Limit : update.Type == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            update.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            SharedOrderStatus.Open,
                            update.CreateTime)
                {
                    ClientOrderId = update.ClientOrderId,
                    OrderQuantity = new SharedOrderQuantity(update.Quantity, update.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(0, 0),
                    UpdateTime = update.UpdateTime,
                    OrderPrice = update.Price,
                    Fee = 0
                };
            }
            if (orderUpdate is HTXMatchedOrderUpdate matchUpdate)
            {
                return new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, matchUpdate.Symbol),
                            matchUpdate.Symbol,
                            matchUpdate.OrderId.ToString(),
                            matchUpdate.Type == Enums.OrderType.Limit ? SharedOrderType.Limit : matchUpdate.Type == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            matchUpdate.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            matchUpdate.QuantityRemaining == 0 ? SharedOrderStatus.Filled : SharedOrderStatus.Open,
                            null)
                {
                    ClientOrderId = matchUpdate.ClientOrderId,
                    OrderQuantity = new SharedOrderQuantity(matchUpdate.Type == Enums.OrderType.Market && matchUpdate.Side == Enums.OrderSide.Buy ? null : matchUpdate.Quantity, matchUpdate.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(matchUpdate.Type == Enums.OrderType.Market && matchUpdate.Side == Enums.OrderSide.Buy ? null : matchUpdate.QuantityFilled, matchUpdate.Type == Enums.OrderType.Market && matchUpdate.Side == Enums.OrderSide.Buy ? matchUpdate.QuantityFilled : null),
                    UpdateTime = matchUpdate.UpdateTime,
                    OrderPrice = matchUpdate.Price,
                    LastTrade = new SharedUserTrade(ExchangeSymbolCache.ParseSymbol(_topicId, matchUpdate.Symbol), matchUpdate.Symbol, matchUpdate.OrderId.ToString(), matchUpdate.TradeId.ToString(), matchUpdate.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, matchUpdate.TradeQuantity, matchUpdate.TradePrice, matchUpdate.TradeTime)
                    {
                        Role = matchUpdate.IsTaker ? SharedRole.Taker : SharedRole.Maker
                    }
                };
            }

            if (orderUpdate is HTXCanceledOrderUpdate cancelUpdate)
            {
                return new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, cancelUpdate.Symbol),
                            cancelUpdate.Symbol,
                            cancelUpdate.OrderId.ToString(),
                            cancelUpdate.Type == Enums.OrderType.Limit ? SharedOrderType.Limit : cancelUpdate.Type == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            cancelUpdate.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            SharedOrderStatus.Canceled,
                            null)
                {
                    ClientOrderId = cancelUpdate.ClientOrderId,
                    OrderQuantity = new SharedOrderQuantity(cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? null : cancelUpdate.Quantity, cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? cancelUpdate.Quantity : null),
                    QuantityFilled = new SharedOrderQuantity(cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? null : cancelUpdate.QuantityFilled, cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? cancelUpdate.QuantityFilled : null),
                    UpdateTime = cancelUpdate.UpdateTime,
                    OrderPrice = cancelUpdate.Price
                };
            }

            throw new Exception("Unknown order update type");
        }
    }
}
