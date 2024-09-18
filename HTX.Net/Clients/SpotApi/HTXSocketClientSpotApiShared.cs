using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Models.Socket;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.SubscribeModels;
using HTX.Net.Objects.Models.Socket;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXSocketClientSpotApi : IHTXSocketClientSpotApiShared
    {
        public string Exchange => HTXExchange.ExchangeName;
        public ApiType[] SupportedApiTypes { get; } = new[] { ApiType.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Tickers client
        SubscriptionOptions<SubscribeAllTickersRequest> ITickersSocketClient.SubscribeAllTickersOptions { get; } = new SubscriptionOptions<SubscribeAllTickersRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<ExchangeEvent<IEnumerable<SharedSpotTicker>>> handler, CancellationToken ct)
        {
            var validationError = ((ITickersSocketClient)this).SubscribeAllTickersOptions.ValidateRequest(Exchange, request, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToTickerUpdatesAsync(update => handler(update.AsExchangeEvent<IEnumerable<SharedSpotTicker>>(Exchange, update.Data.Select(x => new SharedSpotTicker(x.Symbol.ToUpperInvariant(), x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.Volume ?? 0, x.OpenPrice == null ? null : Math.Round((x.ClosePrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))).ToArray()))).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Ticker client
        SubscriptionOptions<SubscribeTickerRequest> ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscriptionOptions<SubscribeTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(symbol.ToUpperInvariant(), update.Data.LastTradePrice, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0, update.Data.Volume ?? 0, update.Data.OpenPrice == null ? null : Math.Round((update.Data.ClosePrice ?? 0) / update.Data.OpenPrice.Value * 100 - 100, 2))))).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Trade client

        SubscriptionOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscriptionOptions<SubscribeTradeRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<ExchangeEvent<IEnumerable<SharedTrade>>> handler, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, update.Data.Details.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Book Ticker client

        SubscriptionOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscriptionOptions<SubscribeBookTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<ExchangeEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false);
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<ExchangeEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
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
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToPartialOrderBookUpdates100MilisecondAsync(symbol, request.Limit ?? 20, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Balance client
        SubscriptionOptions<SubscribeBalancesRequest> IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscriptionOptions<SubscribeBalancesRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<ExchangeEvent<IEnumerable<SharedBalance>>> handler, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, request, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToAccountUpdatesAsync(
                update => handler(update.AsExchangeEvent<IEnumerable<SharedBalance>>(Exchange, new[] { new SharedBalance(update.Data.Asset, update.Data.Available ?? 0, update.Data.Balance ?? 0) })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Spot Order client

        SubscriptionOptions<SubscribeSpotOrderRequest> ISpotOrderSocketClient.SubscribeSpotOrderOptions { get; } = new SubscriptionOptions<SubscribeSpotOrderRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<ExchangeEvent<IEnumerable<SharedSpotOrder>>> handler, CancellationToken ct)
        {
            var validationError = ((ISpotOrderSocketClient)this).SubscribeSpotOrderOptions.ValidateRequest(Exchange, request, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);
            var result = await SubscribeToOrderUpdatesAsync(null,
                update => handler(update.AsExchangeEvent<IEnumerable<SharedSpotOrder>>(Exchange, new[] { ParseOrder(update.Data) })),
                update => handler(update.AsExchangeEvent<IEnumerable<SharedSpotOrder>>(Exchange, new[] { ParseOrder(update.Data) })),
                update => handler(update.AsExchangeEvent<IEnumerable<SharedSpotOrder>>(Exchange, new[] { ParseOrder(update.Data) })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region User Trade client
        SubscriptionOptions<SubscribeUserTradeRequest> IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new SubscriptionOptions<SubscribeUserTradeRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<ExchangeEvent<IEnumerable<SharedUserTrade>>> handler, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, request, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToOrderDetailsUpdatesAsync(
                null,
                update => handler(update.AsExchangeEvent<IEnumerable<SharedUserTrade>>(Exchange, new[] {
                    new SharedUserTrade(
                        update.Data.Symbol,
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

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

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
                    LastTrade = new SharedUserTrade(matchUpdate.Symbol, matchUpdate.OrderId.ToString(), matchUpdate.TradeId.ToString(), matchUpdate.TradeQuantity, matchUpdate.TradePrice, matchUpdate.TradeTime)
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
