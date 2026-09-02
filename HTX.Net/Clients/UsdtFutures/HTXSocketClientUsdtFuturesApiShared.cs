using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Enums;

namespace HTX.Net.Clients.UsdtFutures
{
    internal class HTXSocketClientUsdtFuturesSharedApi :
        SharedApiBase,
        IHTXSocketClientUsdtFuturesApiShared,
        IHTXSocketClientUsdtFuturesSharedApi
    {
        private readonly HTXSocketClientUsdtFuturesApi _api;

        private const string _topicId = "HTXFutures";
        private const string _exchangeName = "HTX";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(HTXExchange.Metadata, this);

        public HTXSocketClientUsdtFuturesSharedApi(HTXSocketClientUsdtFuturesApi api)
            : base(
                  api.Exchange,
                  [TradingMode.PerpetualLinear, TradingMode.DeliveryLinear],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeBalanceOptions,
                SubscribeFuturesOrderOptions,
                SubscribeUserTradeOptions,
                SubscribePositionOptions
                );
        }

        #region Ticker client
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
                    update.Data.ClosePrice, 
                    update.Data.HighPrice ?? 0, 
                    update.Data.LowPrice ?? 0,
                    new SharedOrderQuantity(update.Data.Volume, update.Data.TradeTurnover, update.Data.QuoteVolume), 
                    update.Data.OpenPrice == null ? null : Math.Round((update.Data.ClosePrice ?? 0) / update.Data.OpenPrice.Value * 100 - 100, 2))
            {
            })), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Trade client

        public SubscribeTradeOptions SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToTradeUpdatesAsync(symbol, update => handler(update.ToType(update.Data.Trades.Select(x =>
                new SharedTrade(request.Symbol, symbol, new SharedOrderQuantity(x.Quantity, x.TradeTurnover, x.Amount), x.Price, x.Timestamp)
                {
                    Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
                }).ToArray())), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Book Ticker client

        public SubscribeBookTickerOptions SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToBookTickerUpdatesAsync(symbol, update => handler(
                update.ToType(
                    new SharedBookTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol), 
                        symbol, 
                        update.Data.Ask.Price, 
                        new SharedOrderQuantity(contractQuantity: update.Data.Ask.Quantity),
                        update.Data.Bid.Price, 
                        new SharedOrderQuantity(contractQuantity: update.Data.Bid.Quantity)))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Kline client
        public SubscribeKlineOptions SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false,
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            var validationError = SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.ToType(
                new SharedKline(
                    request.Symbol,
                    symbol, 
                    update.Data.OpenTime, 
                    update.Data.ClosePrice ?? 0,
                    update.Data.HighPrice ?? 0,
                    update.Data.LowPrice ?? 0,
                    update.Data.OpenPrice ?? 0,
                    new SharedOrderQuantity(update.Data.Volume, update.Data.Value, update.Data.QuoteVolume)))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Order Book client
        public SubscribeOrderBookOptions SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 150 });
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToOrderBookUpdatesAsync(symbol, 0, update => handler(
                update.ToType(
                    new SharedOrderBook(SharedQuantityType.Contracts, update.Data.Version, update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Balance client
        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.SubscribeToCrossMarginBalanceUpdatesAsync(
                    update => handler(update.ToType<SharedBalance[]>(update.Data.Data.Select(x => 
                        new SharedBalance(
                            SupportedTradingModes, 
                            x.MarginAsset,
                            x.WithdrawAvailable,
                            x.MarginBalance) ).ToArray())),
                    ct: ct).ConfigureAwait(false);

                return result;
            }
            else
            {
                var result = await _api.SubscribeToIsolatedMarginBalanceUpdatesAsync(
                    update => handler(update.ToType<SharedBalance[]>(update.Data.Data.Select(x =>
                        new SharedBalance(
                            SupportedTradingModes,
                            "USDT",
                            x.WithdrawAvailable,
                            x.MarginBalance) { IsolatedMarginSymbol = x.MarginAccount }).ToArray())),
                    ct: ct).ConfigureAwait(false);

                return result;
            }

        }
        #endregion

        #region Futures Order client

        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
            => await SubscribeToFuturesOrderUpdatesAsync(request, x => handler(x.ToType<SharedFuturesOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeFuturesOrderOptions SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            var result = await _api.SubscribeToOrderUpdatesAsync(marginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated,
                update => {
                    var lastTrade = update.Data.Trade?.OrderByDescending(x => x.TradeId).FirstOrDefault();
                    handler(update.ToType<SharedFuturesOrderUpdate[]>(new[] {
                    new SharedFuturesOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.ContractCode),
                        update.Data.ContractCode,
                        update.Data.OrderId.ToString(),
                        ParseOrderType(update.Data.OrderPriceType),
                        update.Data.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.OrderStatus),
                        update.Data.CreatedAt)
                    {
                        ClientOrderId = update.Data.ClientOrderId?.ToString(),
                        AveragePrice = update.Data.AveragePrice,
                        OrderPrice = update.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: update.Data.Quantity),
                        QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: update.Data.ValueFilled, contractQuantity: update.Data.QuantityFilled),
                        TimeInForce = ParseTimeInForce(update.Data.OrderPriceType),
                        UpdateTime = update.Data.Timestamp,
                        PositionSide = ParsePositionSide(update.Data.Offset, update.Data.OrderSide),
                        ReduceOnly = update.Data.ReduceOnly,
#pragma warning disable CS0618 // Type or member is obsolete
                        Fee = Math.Abs(update.Data.Fee),
                        FeeAsset = update.Data.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                        LastTrade = update.Data.Trade?.Any() != true ? null : 
                            new SharedUserTrade(
                                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.ContractCode),
                                update.Data.ContractCode, 
                                update.Data.OrderIdStr, 
                                lastTrade!.TradeId.ToString(), 
                                update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, 
                                new SharedOrderQuantity(contractQuantity: lastTrade.Quantity), 
                                lastTrade.Price,
                                update.Data.Timestamp)
                            {
                                ClientOrderId = update.Data.ClientOrderId?.ToString(),
                                Fee = Math.Abs(lastTrade.Fee),
                                FeeAsset = lastTrade.FeeAsset,
                                Role = lastTrade.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                            }
                    } }));
                },
                ct: ct).ConfigureAwait(false);

            return result;
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatusFilter status)
        {
            if (status == OrderStatusFilter.Submitted || status == OrderStatusFilter.ReadyToPlace || status == OrderStatusFilter.PartiallyMatched) return SharedOrderStatus.Open;
            if (status == OrderStatusFilter.Canceled || status == OrderStatusFilter.Canceling || status == OrderStatusFilter.PartiallyCanceled) return SharedOrderStatus.Canceled;
            if (status == OrderStatusFilter.FullyMatched) return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
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
        public SubscribeUserTradeOptions SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.SubscribeToCrossMarginUserTradeUpdatesAsync(
                update => {
                    handler(update.ToType<SharedUserTrade[]>(update.Data.Trades.Select(x =>
                                    new SharedUserTrade(
                                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.ContractCode),
                                        update.Data.ContractCode,
                                        update.Data.OrderId.ToString(),
                                        x.ToString(),
                                        update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                        new SharedOrderQuantity(contractQuantity: x.Quantity),
                                        x.Price,
                                        x.CreateTime)
                                    {
                                        Role = x.Role == Enums.OrderRole.Taker ? SharedRole.Taker : SharedRole.Maker,
                                    }
                    ).ToArray()));
                },
                ct: ct).ConfigureAwait(false);
                return result;
            }
            else
            {
                var result = await _api.SubscribeToIsolatedMarginUserTradeUpdatesAsync(
                update => {
                    handler(update.ToType<SharedUserTrade[]>(update.Data.Trades.Select(x =>
                                    new SharedUserTrade(
                                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.ContractCode),
                                        update.Data.ContractCode,
                                        update.Data.OrderId.ToString(),
                                        x.ToString(),
                                        update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                        new SharedOrderQuantity(contractQuantity: x.Quantity),
                                        x.Price,
                                        x.CreateTime)
                                    {
                                        Role = x.Role == Enums.OrderRole.Taker ? SharedRole.Taker : SharedRole.Maker,
                                    }
                    ).ToArray()));
                },
                ct: ct).ConfigureAwait(false);
                return result;
            }
        }
        #endregion

        #region Position client
        public SubscribePositionOptions SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.SubscribeToCrossMarginPositionUpdatesAsync(
                update => handler(update.ToType(update.Data.Data.Select(x =>
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode), 
                        x.ContractCode,
                        new SharedOrderQuantity(contractQuantity: x.Quantity), 
                        update.Data.Timestamp)
                    {
                        AverageOpenPrice = x.PositionPrice,
                        PositionMode = x.PositionMode == PositionMode.SingleSide ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                        PositionSide = x.OrderSide == Enums.OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long,
                        Leverage = x.LeverageRate,
                        UnrealizedPnl = x.UnrealizedPnl
                    }).ToArray())),
                ct: ct).ConfigureAwait(false);
                return result;
            }
            else
            {
                var result = await _api.SubscribeToIsolatedMarginPositionUpdatesAsync(
                update => handler(update.ToType(update.Data.Data.Select(x =>
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode),
                        x.ContractCode,
                        new SharedOrderQuantity(contractQuantity: x.Quantity),
                        update.Data.Timestamp)
                    {
                        AverageOpenPrice = x.PositionPrice,
                        PositionMode = x.PositionMode == PositionMode.SingleSide ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                        PositionSide = x.OrderSide == Enums.OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long,
                        Leverage = x.LeverageRate,
                        UnrealizedPnl = x.UnrealizedPnl
                    }).ToArray())),
                ct: ct).ConfigureAwait(false);
                return result;
            }
        }

        #endregion
    }
}
