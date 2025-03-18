using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Enums;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXSocketClientUsdtFuturesApi : IHTXSocketClientUsdtFuturesApiShared
    {
        private const string _topicId = "HTXFutures";
        public string Exchange => HTXExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.PerpetualLinear, TradingMode.DeliveryLinear };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Ticker client
        EndpointOptions<SubscribeTickerRequest> ITickerSocketClient.SubscribeTickerOptions { get; } = new EndpointOptions<SubscribeTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, update.Data.ClosePrice, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0, update.Data.Volume ?? 0, update.Data.OpenPrice == null ? null : Math.Round((update.Data.ClosePrice ?? 0) / update.Data.OpenPrice.Value * 100 - 100, 2)))), ct).ConfigureAwait(false);

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
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, update.Data.Trades.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)
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
            var result = await SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, update.Data.Ask.Price, update.Data.Ask.Quantity, update.Data.Bid.Price, update.Data.Bid.Quantity))), ct).ConfigureAwait(false);

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
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(false, new[] { 150 });
        async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<ExchangeEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToOrderBookUpdatesAsync(symbol, 0, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Balance client
        EndpointOptions<SubscribeBalancesRequest> IBalanceSocketClient.SubscribeBalanceOptions { get; } = new EndpointOptions<SubscribeBalancesRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<ExchangeEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await SubscribeToCrossMarginBalanceUpdatesAsync(
                    update => handler(update.AsExchangeEvent<SharedBalance[]>(Exchange, update.Data.Data.Select(x => new SharedBalance(x.MarginAsset, x.MarginBalance - x.MarginFrozen, x.MarginBalance) ).ToArray())),
                    ct: ct).ConfigureAwait(false);

                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }
            else
            {
                var result = await SubscribeToIsolatedMarginBalanceUpdatesAsync(
                    update => handler(update.AsExchangeEvent<SharedBalance[]>(Exchange, update.Data.Data.Select(x => new SharedBalance(x.Asset, x.MarginBalance - x.MarginFrozen, x.MarginBalance) { IsolatedMarginSymbol = x.MarginAccount }).ToArray())),
                    ct: ct).ConfigureAwait(false);

                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }

        }
        #endregion

        #region Futures Order client

        EndpointOptions<SubscribeFuturesOrderRequest> IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new EndpointOptions<SubscribeFuturesOrderRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<ExchangeEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderSocketClient)this).SubscribeFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            var result = await SubscribeToOrderUpdatesAsync(marginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated,
                update => {
                    var lastTrade = update.Data.Trade?.OrderByDescending(x => x.TradeId).FirstOrDefault();
                    handler(update.AsExchangeEvent<SharedFuturesOrder[]>(Exchange, new[] {
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.ContractCode),
                        update.Data.ContractCode,
                        update.Data.OrderId.ToString(),
                        ParseOrderType(update.Data.OrderPriceType),
                        update.Data.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.OrderStatus),
                        update.Data.CreatedAt)
                    {
                        ClientOrderId = update.Data.ClientOrderId.ToString(),
                        AveragePrice = update.Data.AveragePrice,
                        OrderPrice = update.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: update.Data.Quantity),
                        QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: update.Data.ValueFilled, contractQuantity: update.Data.QuantityFilled),
                        TimeInForce = ParseTimeInForce(update.Data.OrderPriceType),
                        UpdateTime = update.Data.Timestamp,
                        PositionSide = ParsePositionSide(update.Data.Offset, update.Data.OrderSide),
                        ReduceOnly = update.Data.ReduceOnly,
                        Fee = update.Data.Fee,
                        FeeAsset = update.Data.FeeAsset,
                        LastTrade = update.Data.Trade?.Any() != true ? null : new SharedUserTrade(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.ContractCode), update.Data.ContractCode, update.Data.OrderIdStr, lastTrade!.TradeId.ToString(), update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, lastTrade.Quantity, lastTrade.Price, update.Data.Timestamp)
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
        EndpointOptions<SubscribeUserTradeRequest> IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new EndpointOptions<SubscribeUserTradeRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<ExchangeEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await SubscribeToCrossMarginUserTradeUpdatesAsync(
                update => {
                    handler(update.AsExchangeEvent<SharedUserTrade[]>(Exchange, update.Data.Trades.Select(x =>
                                    new SharedUserTrade(
                                        ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.ContractCode),
                                        update.Data.ContractCode,
                                        update.Data.OrderId.ToString(),
                                        x.ToString(),
                                        update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                        x.Quantity,
                                        x.Price,
                                        x.CreateTime)
                                    {
                                        Role = x.Role == Enums.OrderRole.Taker ? SharedRole.Taker : SharedRole.Maker,
                                    }
                    ).ToArray()));
                },
                ct: ct).ConfigureAwait(false);
                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }
            else
            {
                var result = await SubscribeToIsolatedMarginUserTradeUpdatesAsync(
                update => {
                    handler(update.AsExchangeEvent<SharedUserTrade[]>(Exchange, update.Data.Trades.Select(x =>
                                    new SharedUserTrade(
                                        ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.ContractCode),
                                        update.Data.ContractCode,
                                        update.Data.OrderId.ToString(),
                                        x.ToString(),
                                        update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                        x.Quantity,
                                        x.Price,
                                        x.CreateTime)
                                    {
                                        Role = x.Role == Enums.OrderRole.Taker ? SharedRole.Taker : SharedRole.Maker,
                                    }
                    ).ToArray()));
                },
                ct: ct).ConfigureAwait(false);
                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }
        }
        #endregion

        #region Position client
        EndpointOptions<SubscribePositionRequest> IPositionSocketClient.SubscribePositionOptions { get; } = new EndpointOptions<SubscribePositionRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<ExchangeEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = ((IPositionSocketClient)this).SubscribePositionOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await SubscribeToCrossMarginPositionUpdatesAsync(
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Data.Select(x => new SharedPosition(
                    ExchangeSymbolCache.ParseSymbol(_topicId, x.ContractCode), x.ContractCode, x.Quantity, update.Data.Timestamp)
                {
                    AverageOpenPrice = x.PositionPrice,
                    PositionSide = x.OrderSide == Enums.OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long,
                    Leverage = x.LeverageRate,
                    UnrealizedPnl = x.UnrealizedPnl
                }).ToArray())),
                ct: ct).ConfigureAwait(false);
                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }
            else
            {
                var result = await SubscribeToIsolatedMarginPositionUpdatesAsync(
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, x.ContractCode), x.ContractCode, x.Quantity, update.Data.Timestamp)
                {
                    AverageOpenPrice = x.PositionPrice,
                    PositionSide = x.OrderSide == Enums.OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long,
                    Leverage = x.LeverageRate,
                    UnrealizedPnl = x.UnrealizedPnl
                }).ToArray())),
                ct: ct).ConfigureAwait(false);
                return new ExchangeResult<UpdateSubscription>(Exchange, result);
            }
        }

        #endregion
    }
}
