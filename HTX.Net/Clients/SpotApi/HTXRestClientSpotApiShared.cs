using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXRestClientSpotApi : IHTXRestClientSpotApiShared
    {
        private const string _topicId = "HTXSpot";
        private const string _exchangeName = "HTX";

        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot };

        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(this);

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Kline client
        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, true, true, false, 2000, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth);

        async Task<HttpResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            var validationError = SharedClient.GetKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedKline[]>(Exchange, validationError);

            var apiLimit = 2000;
            int limit = request.Limit ?? apiLimit;
            if (request.StartTime.HasValue == true)
                limit = (int)Math.Ceiling((DateTime.UtcNow - request.StartTime!.Value).TotalSeconds / (int)request.Interval);

            var direction = request.Direction ?? DataDirection.Descending;
            if (limit > apiLimit)
            {
                // Not available via the API
                var cutoff = DateTime.UtcNow.AddSeconds(-(int)request.Interval * apiLimit);
                return HttpResult.Fail<SharedKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Limit), $"Time filter outside of supported range. Can only request the most recent {apiLimit} klines i.e. data later than {cutoff} at this interval"));
            }

            // Pagination not supported, no time filter available

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetKlinesAsync(
                symbol,
                interval,
                limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedKline[]>(result);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice!.Value, x.HighPrice!.Value, x.LowPrice!.Value, x.OpenPrice!.Value, x.Volume!.Value))
                    .ToArray());
        }
        #endregion

        #region Spot Symbol client
        GetSpotSymbolsOptions ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new GetSpotSymbolsOptions(_exchangeName, false);

        async Task<HttpResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolConfigAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotSymbol[]>(result);

            var response = HttpResult.Ok(result, result.Data.Select(s => new SharedSpotSymbol(
                s.BaseAsset.ToUpperInvariant(),
                s.QuoteAsset.ToUpperInvariant(),
                s.Symbol,
                s.Status == SymbolStatus.Online)
            {
                QuantityDecimals = s.QuantityPrecision,
                PriceDecimals = s.PricePrecision,
                MinNotionalValue = s.MinOrderValue,
                MinTradeQuantity = s.MinOrderQuantity,
                MaxTradeQuantity = s.MaxOrderQuantity
            }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, response.Data!);
            return response;
        }

        async Task<ExchangeCallResult<SharedSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, baseAsset));
        }

        async Task<ExchangeCallResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbol));
        }

        async Task<ExchangeCallResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbolName));
        }
        #endregion

        #region Ticker client

        GetSpotTickersOptions ISpotTickerRestClient.GetSpotTickersOptions { get; } = new GetSpotTickersOptions(_exchangeName);
        async Task<HttpResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(
                ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Ticks.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.ClosePrice, x.HighPrice, x.LowPrice, x.Volume ?? 0, x.OpenPrice == null || x.OpenPrice == 0 ? null : Math.Round(((x.ClosePrice ?? 0) / x.OpenPrice.Value) * 100 - 100, 2))
            {
                QuoteVolume = x.QuoteVolume
            }).ToArray());
        }

        GetSpotTickerOptions ISpotTickerRestClient.GetSpotTickerOptions { get; } = new GetSpotTickerOptions(_exchangeName);
        async Task<HttpResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTickerAsync(
                symbol,
                ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker>(result);

            return HttpResult.Ok(result, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, result.Data.ClosePrice, result.Data.HighPrice, result.Data.LowPrice, result.Data.Volume ?? 0, result.Data.OpenPrice == null || result.Data.OpenPrice == 0 ? null : Math.Round((result.Data.ClosePrice ?? 0) / result.Data.OpenPrice.Value * 100 - 100, 2))
            {
                QuoteVolume = result.Data.QuoteVolume
            });
        }

        #endregion

        #region Book Ticker client

        GetBookTickerOptions IBookTickerRestClient.GetBookTickerOptions { get; } = new GetBookTickerOptions(_exchangeName, false);
        async Task<HttpResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = await ExchangeData.GetOrderBookAsync(symbol, 0, 5, ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, symbol),
                symbol,
                resultTicker.Data.Asks[0].Price,
                resultTicker.Data.Asks[0].Quantity,
                resultTicker.Data.Bids[0].Price,
                resultTicker.Data.Bids[0].Quantity));
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 2000, false);
        async Task<HttpResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetRecentTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTradeHistoryAsync(
                symbol,
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            return HttpResult.Ok(result, result.Data.SelectMany(x => x.Details.Select(x => 
            new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            })).ToArray());
        }

        #endregion

        #region Balance client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Spot)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(long), "Account id of the user", 123123123L)
            }
        };

        async Task<HttpResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            var accountId = ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "AccountId");
            var result = await Account.GetBalancesAsync(accountId, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedBalance[]>(result);

            var resp = new List<SharedBalance>();
            foreach(var balance in result.Data)
            {
                var asset = resp.SingleOrDefault(x => x.Asset == balance.Asset.ToUpperInvariant());
                if (asset == null)
                {
                    asset = new SharedBalance(balance.Asset.ToUpperInvariant(), 0, 0);
                    resp.Add(asset);
                }

                if (balance.Type == Enums.BalanceType.Trade)
                {
                    asset.Available = balance.Balance;
                    asset.Total += balance.Balance;
                }
                else if (balance.Type == Enums.BalanceType.Frozen)
                {
                    asset.Total += balance.Balance;
                }
            }

            return HttpResult.Ok(result, resp.ToArray());
        }

        #endregion

        #region Spot Order client


        SharedFeeDeductionType ISpotOrderRestClient.SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        SharedFeeAssetType ISpotOrderRestClient.SpotFeeAssetType => SharedFeeAssetType.OutputAsset;
        SharedOrderType[] ISpotOrderRestClient.SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        SharedTimeInForce[] ISpotOrderRestClient.SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        SharedQuantitySupport ISpotOrderRestClient.SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.QuoteAsset,
                SharedQuantityType.BaseAsset);

        string ISpotOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(long), "The id of the account", 123123123L)
            }
        };
        async Task<HttpResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var accountId = ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "AccountId");
            var quantity = request.Quantity?.QuantityInBaseAsset ?? 0;
            if (request.OrderType == SharedOrderType.Market && request.Side == SharedOrderSide.Buy)
                quantity = request.Quantity?.QuantityInQuoteAsset ?? 0;

            var result = await Trading.PlaceOrderAsync(
                accountId,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                quantity,
                request.Price,
                request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.ToString()));
        }

        GetSpotOrderOptions ISpotOrderRestClient.GetSpotOrderOptions { get; } = new GetSpotOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedSpotOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest), "Invalid order id"));

            var order = await Trading.GetOrderAsync(orderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder>(order);

            return HttpResult.Ok(order, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol), 
                order.Data.Symbol,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.Type),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                Fee = order.Data.Fee,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.Type == OrderType.Market && order.Data.Side == OrderSide.Buy ? null : order.Data.Quantity, order.Data.Type == OrderType.Market && order.Data.Side == OrderSide.Buy ? order.Data.Quantity : null),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.Type),
                IsTriggerOrder = order.Data.Type == OrderType.StopLimit
            });
        }

        GetOpenSpotOrdersOptions ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new GetOpenSpotOrdersOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOpenSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var order = await Trading.GetOpenOrdersAsync(symbol: symbol).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(order);

            return HttpResult.Ok(order, order.Data.Select(x => new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                x.Symbol,
                x.Id.ToString(),
                ParseOrderType(x.Type),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                Fee = x.Fee,
                OrderPrice = x.Price,
                OrderQuantity = new SharedOrderQuantity(x.Type == OrderType.Market && x.Side == OrderSide.Buy ? null : x.Quantity, x.Type == OrderType.Market && x.Side == OrderSide.Buy ? x.Quantity : null),
                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(x.Type),
                IsTriggerOrder = x.Type == OrderType.StopLimit
            }).ToArray());
        }

        GetSpotClosedOrdersOptions ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new GetSpotClosedOrdersOptions(_exchangeName, false, true, true, 100)
        {
            MaxAge = TimeSpan.FromDays(178)
        };
        async Task<HttpResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetClosedSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(2));

            var result = await Trading.GetClosedOrdersAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.CreateTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(2),
                     TimeSpan.FromDays(178));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => new SharedSpotOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                        x.Symbol,
                        x.Id.ToString(),
                        ParseOrderType(x.Type),
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x.Status),
                        x.CreateTime)
                    {
                        ClientOrderId = x.ClientOrderId,
                        Fee = x.Fee,
                        OrderPrice = x.Price,
                        OrderQuantity = new SharedOrderQuantity(x.Type == OrderType.Market && x.Side == OrderSide.Buy ? null : x.Quantity, x.Type == OrderType.Market && x.Side == OrderSide.Buy ? x.Quantity : null),
                        QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                        TimeInForce = ParseTimeInForce(x.Type),
                        IsTriggerOrder = x.Type == OrderType.StopLimit
                    }).ToArray(), nextPageRequest);
        }

        GetSpotOrderTradesOptions ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new GetSpotOrderTradesOptions(_exchangeName, true);
        async Task<HttpResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest), "Invalid order id"));

            var order = await Trading.GetOrderTradesAsync(orderId, ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedUserTrade[]>(order);

            return HttpResult.Ok(order, order.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.Role == OrderRole.Taker ? SharedRole.Taker : SharedRole.Maker
            }).ToArray());
        }

        GetSpotUserTradesOptions ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new GetSpotUserTradesOptions(_exchangeName, false, true, true, 500)
        {
            MaxAge = TimeSpan.FromDays(118)
        };
        async Task<HttpResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotUserTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(2));

            // Get data
            var result = await Trading.GetUserTradesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            // Get next token
            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Timestamp)),
                     result.Data.Length,
                     result.Data.Select(x => x.Timestamp),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(2),
                     TimeSpan.FromDays(118));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.TradeId.ToString(),
                            x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            x.Quantity,
                            x.Price,
                            x.Timestamp)
                        {
                            Fee = x.Fee,
                            FeeAsset = x.FeeAsset,
                            Role = x.Role == OrderRole.Taker ? SharedRole.Taker: SharedRole.Maker
                        })
                    .ToArray(), nextPageRequest);
        }

        CancelSpotOrderOptions ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new CancelSpotOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(orderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.ToString()));
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Submitted || status == OrderStatus.PreSubmitted || status == OrderStatus.Created || status == OrderStatus.PartiallyFilled) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.PartiallyCanceled || status == OrderStatus.Rejected) return SharedOrderStatus.Canceled;
            if (status == OrderStatus.Filled) return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;
            if (type == OrderType.LimitMaker) return SharedOrderType.LimitMaker;
            if (type == OrderType.Limit || type == OrderType.FillOrKillLimit || type == OrderType.IOC) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(OrderType tif)
        {
            if (tif == OrderType.IOC) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == OrderType.FillOrKillLimit) return SharedTimeInForce.FillOrKill;
            if (tif == OrderType.Limit) return SharedTimeInForce.GoodTillCanceled;
            if (tif == OrderType.LimitMaker) return SharedTimeInForce.GoodTillCanceled;

            return null;
        }

        private OrderType GetPlaceOrderType(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (type == SharedOrderType.Limit && (tif == null || tif == SharedTimeInForce.GoodTillCanceled)) return OrderType.Limit;
            if (type == SharedOrderType.Limit && tif == SharedTimeInForce.ImmediateOrCancel) return OrderType.IOC;
            if (type == SharedOrderType.Limit && tif == SharedTimeInForce.FillOrKill) return OrderType.FillOrKillLimit;
            if (type == SharedOrderType.LimitMaker) return OrderType.LimitMaker;
            if (type == SharedOrderType.Market) return OrderType.Market;

            throw new ArgumentException($"The combination of order type `{type}` and time in force `{tif}` in invalid");
        }

        #endregion

        #region Spot Client Id Order Client

        GetSpotOrderByClientOrderIdOptions ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdOptions { get; } = new GetSpotOrderByClientOrderIdOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotOrder>> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            var order = await Trading.GetOrderByClientOrderIdAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder>(order);

            return HttpResult.Ok(order, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.Type),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                Fee = order.Data.Fee,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.Type == OrderType.Market && order.Data.Side == OrderSide.Buy ? null : order.Data.Quantity, order.Data.Type == OrderType.Market && order.Data.Side == OrderSide.Buy ? order.Data.Quantity : null),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.Type),
                IsTriggerOrder = order.Data.Type == OrderType.StopLimit
            });
        }

        CancelSpotOrderByClientOrderIdOptions ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdOptions { get; } = new CancelSpotOrderByClientOrderIdOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderByClientOrderIdAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.ToString()));
        }
        #endregion

        #region Asset client
        GetAssetOptions IAssetsRestClient.GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, false);
        async Task<HttpResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetsAndNetworksAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset>(assets);

            var asset = assets.Data.SingleOrDefault();
            if (asset == null)
                return HttpResult.Fail<SharedAsset>(assets, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, "Asset not found")));

            return HttpResult.Ok(assets, new SharedAsset(asset.Asset.ToUpperInvariant())
            {
                Networks = asset.Networks.Select(x => new SharedAssetNetwork(x.Network)
                {
                    FullName = x.DisplayName,
                    MinConfirmations = x.NumOfConfirmations,
                    DepositEnabled = x.DepositStatus == NetworkStatus.Allowed,
                    MinWithdrawQuantity = x.MinWithdrawQuantity,
                    MaxWithdrawQuantity = x.MaxWithdrawQuantity,
                    WithdrawEnabled = x.WithdrawStatus == NetworkStatus.Allowed,
                    WithdrawFee = x.FixedWithdrawFee
                }).ToArray()
            });
        }

        GetAssetsOptions IAssetsRestClient.GetAssetsOptions { get; } = new GetAssetsOptions(_exchangeName, false);
        async Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetsAndNetworksAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset[]>(assets);

            return HttpResult.Ok(assets, assets.Data.Select(x => new SharedAsset(x.Asset.ToUpperInvariant())
            {
                Networks = x.Networks.Select(x => new SharedAssetNetwork(x.Network)
                {
                    FullName = x.DisplayName,
                    MinConfirmations = x.NumOfConfirmations,
                    DepositEnabled = x.DepositStatus == NetworkStatus.Allowed,
                    MinWithdrawQuantity = x.MinWithdrawQuantity,
                    MaxWithdrawQuantity = x.MaxWithdrawQuantity,
                    WithdrawEnabled = x.WithdrawStatus == NetworkStatus.Allowed,
                    WithdrawFee = x.FixedWithdrawFee
                }).ToArray()
            }).ToArray());
        }

        #endregion

        #region Deposit client

        GetDepositAddressesOptions IDepositRestClient.GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true);
        async Task<HttpResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await Account.GetDepositAddressesAsync(request.Asset).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, depositAddresses.Data.Where(x => request.Network == null ? true : x.Network == request.Network).Select(x => new SharedDepositAddress(x.Asset.ToUpperInvariant(), x.Address)
            {
                Network = x.Network,
                TagOrMemo = x.AddressTag
            }
            ).ToArray());
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(_exchangeName, true, true, true, 50);
        async Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetDepositsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            int limit = request.Limit ?? 50;
            var direction = request.Direction ?? DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Account.GetWithdrawDepositHistoryAsync(
                WithdrawDepositType.Deposit,
                request.Asset,
                from: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                size: pageParams.Limit,
                direction: FilterDirection.Next,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedDeposit[]>(result);

            // Get next token
            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromId(result.Data.Min(x => x.Id) - 1),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedDeposit(
                            x.Asset!.ToUpperInvariant(),
                            x.Quantity, 
                            x.Status == WithdrawDepositStatus.Safe,
                            x.CreateTime,
                            ParseTransferStatus(x.Status))
                        {
                            Id = x.Id.ToString(),
                            Network = x.Network,
                            TransactionId = x.TransactionHash,
                            Tag = x.AddressTag
                        })
                    .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus ParseTransferStatus(WithdrawDepositStatus status)
        {
            if (status == WithdrawDepositStatus.Safe)
                return SharedTransferStatus.Completed;
            if (status == WithdrawDepositStatus.Repealed
                || status == WithdrawDepositStatus.ConfirmError
                || status == WithdrawDepositStatus.WalletReject
                || status == WithdrawDepositStatus.Reject
                || status == WithdrawDepositStatus.Canceled
                || status == WithdrawDepositStatus.Failed) 
            {
                return SharedTransferStatus.Failed;
            }

            if (status == WithdrawDepositStatus.Confirming
                || status == WithdrawDepositStatus.Verifying
                || status == WithdrawDepositStatus.Submitted
                || status == WithdrawDepositStatus.WaitingTinyAmount
                || status == WithdrawDepositStatus.WalletTransfer)
            {
                return SharedTransferStatus.InProgress;
            }

            return SharedTransferStatus.InProgress;
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, new[] { 5, 10, 20 }, false);
        async Task<HttpResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                0,
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOrderBook>(result);

            return HttpResult.Ok(result, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(_exchangeName, true, true, true, 50);
        async Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetWithdrawalsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            int limit = request.Limit ?? 50;
            var direction = request.Direction ?? DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Account.GetWithdrawDepositHistoryAsync(
                WithdrawDepositType.Withdraw,
                request.Asset,
                from: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                size: pageParams.Limit,
                direction: FilterDirection.Next,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            // Get next token
            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromId(result.Data.Min(x => x.Id) - 1),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedWithdrawal(x.Asset!.ToUpperInvariant(), x.Address!, x.Quantity, x.Status == WithdrawDepositStatus.Confirmed, x.CreateTime)
                        {
                            Id = x.Id.ToString(),
                            Network = x.Network,
                            TransactionId = x.TransactionHash,
                            Tag = x.AddressTag,
                            Fee = x.Fee
                        })
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("withdrawFee", typeof(decimal), "Fee to use for the withdrawal", 0.001m)
            }
        };

        async Task<HttpResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var fee = ExchangeParameters.GetValue<decimal?>(request.ExchangeParameters, Exchange, "withdrawFee");

            // Get data
            var withdrawal = await Account.WithdrawAsync(
                asset: request.Asset,
                fee: fee!.Value,
                address: request.Address,
                quantity: request.Quantity,
                network: request.Network,
                addressTag: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.ToString()));
        }

        #endregion

        #region Fee Client
        GetFeeOptions IFeeRestClient.GetFeeOptions { get; } = new GetFeeOptions(_exchangeName, true);

        async Task<HttpResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFeeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetTradingFeesAsync(
                [request.Symbol!.GetSymbol(FormatSymbol)],
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFee>(result);

            var fees = result.Data.First();

            // Return
            return HttpResult.Ok(result, new SharedFee(fees.MakerFeeRate * 100, fees.TakerFeeRate * 100));
        }
        #endregion

        #region Spot Trigger Order Client
        PlaceSpotTriggerOrderOptions ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(long), "The id of the account", 123123123L)
            }
        };
        async Task<HttpResult<SharedId>> ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var accountId = ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "AccountId");
            var orderPrice = request.OrderPrice;
            if (request.OrderPrice == null)
            {
                // There is no stop market order in the HTX API
                var maxSlippage = 5;
                orderPrice = request.OrderSide == SharedOrderSide.Buy ? request.TriggerPrice * (1 + maxSlippage / 100m) : request.TriggerPrice * (1 - maxSlippage / 100m);
            }

            var result = await Trading.PlaceOrderAsync(
                accountId,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderSide == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                OrderType.StopLimit,
                request.Quantity.QuantityInBaseAsset ?? 0,
                orderPrice,
                clientOrderId: request.ClientOrderId,
                stopPrice: request.TriggerPrice,
                stopOperator: request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? Operator.GreaterThanOrEqual : Operator.LesserThanOrEqual,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.ToString()));
        }


        GetSpotTriggerOrderOptions ISpotTriggerOrderRestClient.GetSpotTriggerOrderOptions { get; } = new GetSpotTriggerOrderOptions(_exchangeName, true)
        {
        };
        async Task<HttpResult<SharedSpotTriggerOrder>> ISpotTriggerOrderRestClient.GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest), "Invalid order id"));

            var order = await Trading.GetOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotTriggerOrder>(order);

            return HttpResult.Ok(order, new SharedSpotTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.Id.ToString(),
                order.Data.Type == OrderType.StopLimit ? SharedOrderType.Limit : SharedOrderType.Market,
                order.Data.Side == OrderSide.Buy ? SharedTriggerOrderDirection.Enter: SharedTriggerOrderDirection.Exit,
                ParseTriggerOrderStatus(order.Data.Status),
                order.Data.StopPrice ?? 0,
                order.Data.CreateTime)
            {
                PlacedOrderId = order.Data.Id.ToString(),
                Fee = order.Data.Fee,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.Type == OrderType.Market && order.Data.Side == OrderSide.Buy ? null : order.Data.Quantity, order.Data.Type == OrderType.Market && order.Data.Side == OrderSide.Buy ? order.Data.Quantity : null),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.Type),
                ClientOrderId = order.Data.ClientOrderId
            });
        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Filled)
                return SharedTriggerOrderStatus.Filled;

            if (status == OrderStatus.Canceled || status == OrderStatus.Rejected || status == OrderStatus.PartiallyCanceled)
                return SharedTriggerOrderStatus.CanceledOrRejected;

            if (status == OrderStatus.PartiallyFilled
                || status == OrderStatus.Created
                || status == OrderStatus.PreSubmitted
                || status == OrderStatus.Submitted)
            {
                return SharedTriggerOrderStatus.Active;
            }

            return SharedTriggerOrderStatus.Unknown;
        }

        CancelSpotTriggerOrderOptions ISpotTriggerOrderRestClient.CancelSpotTriggerOrderOptions { get; } = new CancelSpotTriggerOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(
                orderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Transfer client

        TransferOptions ITransferRestClient.TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Spot,
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryInverseFutures
            ])
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "The settle asset for futures transfer", "usdt")
            }
        };
        async Task<HttpResult<SharedId>> ITransferRestClient.TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var fromType = GetTransferType(request.FromAccountType);
            var toType = GetTransferType(request.ToAccountType);
            if (fromType == null || toType == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));
            if(request.FromSymbol != null && request.ToSymbol != null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From Symbol", "Both fromSymbol and toSymbol cannot be set at the same time"));

            // Get data
            var transfer = await Account.TransferAsync(
                fromType.Value,
                toType.Value,
                request.Asset,
                request.Quantity,
                request.ToSymbol ?? request.FromSymbol ?? "USDT",
                ct: ct).ConfigureAwait(false);
            if (!transfer.Success)
                return HttpResult.Fail<SharedId>(transfer);

            return HttpResult.Ok(transfer, new SharedId(transfer.Data.ToString()));
        }

        private TransferAccount? GetTransferType(SharedAccountType type)
        {
            if (type == SharedAccountType.Spot) return TransferAccount.Spot;
            if (type == SharedAccountType.DeliveryLinearFutures || type == SharedAccountType.PerpetualLinearFutures) return TransferAccount.LinearSwap;
            if (type == SharedAccountType.PerpetualInverseFutures || type == SharedAccountType.DeliveryInverseFutures) return TransferAccount.Swap;
            return null;
        }

        #endregion
    }
}
