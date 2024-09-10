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
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Enums;
using CryptoExchange.Net.SharedApis.Interfaces.Socket.Futures;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXSocketClientUsdtFuturesApi : IHTXSocketClientUsdtFuturesApiShared
    {
        public string Exchange => HTXExchange.ExchangeName;
        public ApiType[] SupportedApiTypes { get; } = new[] { ApiType.PerpetualLinear, ApiType.DeliveryLinear };

        #region Ticker client
        SubscriptionOptions<SubscribeTickerRequest> ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscriptionOptions<SubscribeTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(symbol, update.Data.ClosePrice, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0, update.Data.Volume ?? 0)))).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Trade client

        SubscriptionOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscriptionOptions<SubscribeTradeRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<ExchangeEvent<IEnumerable<SharedTrade>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, update.Data.Trades.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Book Ticker client

        SubscriptionOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscriptionOptions<SubscribeBookTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<ExchangeEvent<SharedBookTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(update.Data.Ask.Price, update.Data.Ask.Quantity, update.Data.Bid.Price, update.Data.Bid.Quantity))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false);
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<ExchangeEvent<SharedKline>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.AsExchangeEvent(Exchange, new SharedKline(update.Data.OpenTime, update.Data.ClosePrice ?? 0, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0, update.Data.OpenPrice ?? 0, update.Data.Volume ?? 0))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(false, new[] { 0 });
        async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<ExchangeEvent<SharedOrderBook>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

#warning which limit does this use?
            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToOrderBookUpdatesAsync(symbol, 0, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Balance client
        SubscriptionOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscriptionOptions("SubscribeBalanceRequest", false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedBalance>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await SubscribeToCrossMarginBalanceUpdatesAsync(
                    update => handler(update.AsExchangeEvent<IEnumerable<SharedBalance>>(Exchange, update.Data.Data.Select(x => new SharedBalance(x.MarginAsset, x.MarginBalance, x.MarginBalance + x.MarginFrozen) ))),
                    ct: ct).ConfigureAwait(false);

                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }
            else
            {
#warning This uses Asset instead of MarginAsset for cross, correct?
                var result = await SubscribeToIsolatedMarginBalanceUpdatesAsync(
                    update => handler(update.AsExchangeEvent<IEnumerable<SharedBalance>>(Exchange, update.Data.Data.Select(x => new SharedBalance(x.Asset, x.MarginBalance, x.MarginBalance + x.MarginFrozen)))),
                    ct: ct).ConfigureAwait(false);

                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }

        }
        #endregion

        #region Futures Order client

        SubscriptionOptions IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new SubscriptionOptions("SubscribeFuturesOrderRequest", false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedFuturesOrder>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderSocketClient)this).SubscribeFuturesOrderOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);
            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            var result = await SubscribeToOrderUpdatesAsync(marginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated,
                update => {
                    var lastTrade = update.Data.Trade?.OrderByDescending(x => x.TradeId).FirstOrDefault();
                    handler(update.AsExchangeEvent<IEnumerable<SharedFuturesOrder>>(Exchange, new[] {
                    new SharedFuturesOrder(
                        update.Data.ContractCode,
                        update.Data.OrderId.ToString(),
                        ParseOrderType(update.Data.OrderPriceType),
                        update.Data.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.OrderStatus),
                        update.Data.CreatedAt)
                    {
                        ClientOrderId = update.Data.ClientOrderId.ToString(),
                        AveragePrice = update.Data.AveragePrice,
                        Price = update.Data.Price,
                        Quantity = update.Data.Quantity,
                        QuantityFilled = update.Data.QuantityFilled,
                        QuoteQuantityFilled = update.Data.ValueFilled,
                        TimeInForce = ParseTimeInForce(update.Data.OrderPriceType),
                        UpdateTime = update.Data.Timestamp,
                        PositionSide = ParsePositionSide(update.Data.Offset, update.Data.OrderSide),
                        ReduceOnly = update.Data.ReduceOnly,
                        Fee = update.Data.Fee,
                        FeeAsset = update.Data.FeeAsset,
                        LastTrade = update.Data.Trade?.Any() != true ? null : new SharedUserTrade(update.Data.Symbol, update.Data.OrderIdStr, lastTrade.TradeId.ToString(), lastTrade.Quantity, lastTrade.Price, update.Data.Timestamp)
                        {
                            Fee = lastTrade.Fee,
                            FeeAsset = lastTrade.FeeAsset,
                            Role = lastTrade.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                        }
                    } }));
                },
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatusFilter status)
        {
            if (status == OrderStatusFilter.Submitted || status == OrderStatusFilter.ReadyToPlace || status == OrderStatusFilter.PartiallyMatched) return SharedOrderStatus.Open;
            if (status == OrderStatusFilter.Canceled || status == OrderStatusFilter.Canceling || status == OrderStatusFilter.PartiallyCanceled) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(OrderPriceType type)
        {
            if (type == OrderPriceType.Market) return SharedOrderType.Market;
            if (type == OrderPriceType.Limit) return SharedOrderType.Limit;
            if (type == OrderPriceType.PostOnly) return SharedOrderType.LimitMaker;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(OrderPriceType tif)
        {
            if (tif == OrderPriceType.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == OrderPriceType.FillOrKill) return SharedTimeInForce.FillOrKill;

            return null;
        }

        private SharedPositionSide? ParsePositionSide(Offset offset, OrderSide side)
        {
            if (offset == Offset.Both)
                return null;

            if (offset == Offset.Open)
            {
                if (side == OrderSide.Buy) return SharedPositionSide.Long;
                return SharedPositionSide.Short;
            }

            if (side == OrderSide.Sell) return SharedPositionSide.Long;
            return SharedPositionSide.Short;
        }
        #endregion

        #region User Trade client
        SubscriptionOptions IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new SubscriptionOptions("SubscribeUserTradeRequest", false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedUserTrade>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await SubscribeToCrossMarginUserTradeUpdatesAsync(
                update => {
#warning correct..?
                    var lastTrade = update.Data.Trades.Last();
                    handler(update.AsExchangeEvent<IEnumerable<SharedUserTrade>>(Exchange, new[] {
                                    new SharedUserTrade(
                                        update.Data.Symbol,
                                        update.Data.OrderId.ToString(),
                                        lastTrade.ToString(),
                                        lastTrade.Quantity,
                                        lastTrade.Price,
                                        lastTrade.CreateTime)
                                    {
                                        Role = lastTrade.Role == Enums.OrderRole.Taker ? SharedRole.Taker : SharedRole.Maker,
                                    }
                    }));
                },
                ct: ct).ConfigureAwait(false);
                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }
            else
            {
                var result = await SubscribeToIsolatedMarginUserTradeUpdatesAsync(
                update => {
#warning correct..?
                    var lastTrade = update.Data.Trades.Last();
                    handler(update.AsExchangeEvent<IEnumerable<SharedUserTrade>>(Exchange, new[] {
                                    new SharedUserTrade(
                                        update.Data.Symbol,
                                        update.Data.OrderId.ToString(),
                                        lastTrade.ToString(),
                                        lastTrade.Quantity,
                                        lastTrade.Price,
                                        lastTrade.CreateTime)
                                    {
                                        Role = lastTrade.Role == Enums.OrderRole.Taker ? SharedRole.Taker : SharedRole.Maker,
                                    }
                    }));
                },
                ct: ct).ConfigureAwait(false);
                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }
        }
        #endregion

        #region Position client
        SubscriptionOptions IPositionSocketClient.SubscribePositionOptions { get; } = new SubscriptionOptions("SubscribePositionRequest", true);
        async Task<ExchangeResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedPosition>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await SubscribeToCrossMarginPositionUpdatesAsync(
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Data.Select(x => new SharedPosition(x.Symbol, x.Quantity, update.Data.Timestamp)
                {
                    AverageEntryPrice = x.PositionPrice,
                    PositionSide = x.OrderSide == Enums.OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long,
                    Leverage = x.LeverageRate,
                    UnrealizedPnl = x.UnrealizedPnl
                }))),
                ct: ct).ConfigureAwait(false);
                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }
            else
            {
                var result = await SubscribeToIsolatedMarginPositionUpdatesAsync(
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Data.Select(x => new SharedPosition(x.Symbol, x.Quantity, update.Data.Timestamp)
                {
                    AverageEntryPrice = x.PositionPrice,
                    PositionSide = x.OrderSide == Enums.OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long,
                    Leverage = x.LeverageRate,
                    UnrealizedPnl = x.UnrealizedPnl
                }))),
                ct: ct).ConfigureAwait(false);
                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }
        }

        #endregion
    }
}
