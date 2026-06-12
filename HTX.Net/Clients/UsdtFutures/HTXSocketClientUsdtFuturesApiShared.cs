using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Enums;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXSocketClientUsdtFuturesApi : IHTXSocketClientUsdtFuturesApiShared
    {
        private const string _topicId = "HTXFutures";
        private const string _exchangeName = "HTX";
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.PerpetualLinear, TradingMode.DeliveryLinear };

        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(this);

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Ticker client
        SubscribeTickerOptions ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName);
        async Task<WebSocketResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.ToType(new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, update.Data.ClosePrice, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0, update.Data.Volume ?? 0, update.Data.OpenPrice == null ? null : Math.Round((update.Data.ClosePrice ?? 0) / update.Data.OpenPrice.Value * 100 - 100, 2))
            {
                QuoteVolume = update.Data.QuoteVolume
            })), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Trade client

        SubscribeTradeOptions ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.ToType(update.Data.Trades.Select(x =>
            new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray())), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Book Ticker client

        SubscribeBookTickerOptions IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.ToType(new SharedBookTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, update.Data.Ask.Price, update.Data.Ask.Quantity, update.Data.Bid.Price, update.Data.Bid.Quantity))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false,
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
        async Task<WebSocketResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            var validationError = SharedClient.SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.ToType(
                new SharedKline(request.Symbol, symbol, update.Data.OpenTime, update.Data.ClosePrice ?? 0, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0, update.Data.OpenPrice ?? 0, update.Data.Volume ?? 0))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 150 });
        async Task<WebSocketResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToOrderBookUpdatesAsync(symbol, 0, update => handler(update.ToType(new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Balance client
        SubscribeBalanceOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<WebSocketResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await SubscribeToCrossMarginBalanceUpdatesAsync(
                    update => handler(update.ToType<SharedBalance[]>(update.Data.Data.Select(x => new SharedBalance(x.MarginAsset, x.WithdrawAvailable, x.MarginBalance) ).ToArray())),
                    ct: ct).ConfigureAwait(false);

                return result;
            }
            else
            {
                var result = await SubscribeToIsolatedMarginBalanceUpdatesAsync(
                    update => handler(update.ToType<SharedBalance[]>(update.Data.Data.Select(x => new SharedBalance("USDT", x.WithdrawAvailable, x.MarginBalance) { IsolatedMarginSymbol = x.MarginAccount }).ToArray())),
                    ct: ct).ConfigureAwait(false);

                return result;
            }

        }
        #endregion

        #region Futures Order client

        SubscribeFuturesOrderOptions IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            var result = await SubscribeToOrderUpdatesAsync(marginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated,
                update => {
                    var lastTrade = update.Data.Trade?.OrderByDescending(x => x.TradeId).FirstOrDefault();
                    handler(update.ToType<SharedFuturesOrder[]>(new[] {
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.ContractCode),
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
                        Fee = Math.Abs(update.Data.Fee),
                        FeeAsset = update.Data.FeeAsset,
                        LastTrade = update.Data.Trade?.Any() != true ? null : new SharedUserTrade(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.ContractCode), update.Data.ContractCode, update.Data.OrderIdStr, lastTrade!.TradeId.ToString(), update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, lastTrade.Quantity, lastTrade.Price, update.Data.Timestamp)
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
        SubscribeUserTradeOptions IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<WebSocketResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await SubscribeToCrossMarginUserTradeUpdatesAsync(
                update => {
                    handler(update.ToType<SharedUserTrade[]>(update.Data.Trades.Select(x =>
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
                return result;
            }
            else
            {
                var result = await SubscribeToIsolatedMarginUserTradeUpdatesAsync(
                update => {
                    handler(update.ToType<SharedUserTrade[]>(update.Data.Trades.Select(x =>
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
                return result;
            }
        }
        #endregion

        #region Position client
        SubscribePositionOptions IPositionSocketClient.SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<WebSocketResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await SubscribeToCrossMarginPositionUpdatesAsync(
                update => handler(update.ToType(update.Data.Data.Select(x => new SharedPosition(
                    ExchangeSymbolCache.ParseSymbol(_topicId, x.ContractCode), x.ContractCode, x.Quantity, update.Data.Timestamp)
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
                var result = await SubscribeToIsolatedMarginPositionUpdatesAsync(
                update => handler(update.ToType(update.Data.Data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, x.ContractCode), x.ContractCode, x.Quantity, update.Data.Timestamp)
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
