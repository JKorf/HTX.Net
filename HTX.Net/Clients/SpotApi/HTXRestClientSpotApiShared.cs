using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Models.Rest;
using HTX.Net.Enums;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXRestClientSpotApi : IHTXRestClientSpotApiShared
    {
        private long? _accountId;

        public string Exchange => HTXExchange.ExchangeName;

        #region Kline client
        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(false, false)
        {
            MaxTotalDataPoints = 2000
        };

        async Task<ExchangeWebResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, validationError);

            // Determine the amount of data points we need to match the requested time
            var apiLimit = 2000;
            int limit = request.Filter?.Limit ?? apiLimit;
            if (request.Filter?.StartTime.HasValue == true)
                limit = (int)Math.Ceiling((DateTime.UtcNow - request.Filter.StartTime!.Value).TotalSeconds / (int)request.Interval);

            if (limit > apiLimit)
            {
                // Not available via the API
                var cutoff = DateTime.UtcNow.AddSeconds(-(int)request.Interval * apiLimit);
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError($"Time filter outside of supported range. Can only request the most recent {apiLimit} klines i.e. data later than {cutoff} at this interval"));
            }

            // Pagination not supported, no time filter available

            // Get data
            var result = await ExchangeData.GetKlinesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)),
                interval,
                limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, default);

            // Filter the data based on requested timestamps
            var data = result.Data;
            if (request.Filter?.StartTime.HasValue == true)
                data = data.Where(d => d.OpenTime >= request.Filter.StartTime.Value);
            if (request.Filter?.EndTime.HasValue == true)
                data = data.Where(d => d.OpenTime < request.Filter.EndTime.Value);
            data = data.Reverse();
            if (request.Filter?.Limit.HasValue == true)
                data = data.Take(request.Filter.Limit.Value);

            return result.AsExchangeResult(Exchange, data.Select(x => new SharedKline(x.OpenTime, x.ClosePrice!.Value, x.HighPrice!.Value, x.LowPrice!.Value, x.OpenPrice!.Value, x.Volume!.Value)));
        }
        #endregion

        #region Spot Symbol client
        EndpointOptions ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new EndpointOptions("GetSpotSymbolsRequest", false);

        async Task<ExchangeWebResult<IEnumerable<SharedSpotSymbol>>> ISpotSymbolRestClient.GetSpotSymbolsAsync(ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(Exchange, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotSymbol>>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedSpotSymbol>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Name)
            {
                QuantityDecimals = (int)s.QuantityPrecision,
                PriceDecimals = (int)s.PricePrecision
            }));
        }

        #endregion

        #region Ticker client

        EndpointOptions ISpotTickerRestClient.GetSpotTickersOptions { get; } = new EndpointOptions("GetTickersRequest", false);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotTicker>>> ISpotTickerRestClient.GetSpotTickersAsync(ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickersOptions.ValidateRequest(Exchange, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotTicker>>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(
                ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedSpotTicker>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Ticks.Select(x => new SharedSpotTicker(x.Symbol, x.LastTradePrice, x.HighPrice ?? 0, x.LowPrice ?? 0, x.Volume ?? 0)));
        }

        EndpointOptions<GetTickerRequest> ISpotTickerRestClient.GetSpotTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickerOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await ExchangeData.GetTickerAsync(
                symbol,
                ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedSpotTicker(symbol, result.Data.ClosePrice ?? 0, result.Data.HighPrice ?? 0, result.Data.LowPrice ?? 0, result.Data.Volume ?? 0));
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(2000, false);
        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedTrade>>(Exchange, validationError);

            var result = await ExchangeData.GetTradeHistoryAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.SelectMany(x => x.Details.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp))));
        }

        #endregion

        #region Balance client
        EndpointOptions IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions("GetBalancesRequest", true);

        async Task<ExchangeWebResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedBalance>>(Exchange, validationError);

            var accountId = await GetAccountId(exchangeParameters, ct).ConfigureAwait(false);
            if (accountId == null)
                return new ExchangeWebResult<IEnumerable<SharedBalance>>(Exchange, new ServerError("Failed to retrieve account id"));

            var result = await Account.GetBalancesAsync(accountId.Value, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, default);

            var resp = new List<SharedBalance>();
            foreach(var balance in result.Data)
            {
                var asset = resp.SingleOrDefault(x => x.Asset == balance.Asset);
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

            return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, resp);
        }

        #endregion

        #region Spot Order client

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(
            new[]
            {
                SharedOrderType.Limit,
                SharedOrderType.Market,
                SharedOrderType.LimitMaker
            },
            new[]
            {
                SharedTimeInForce.GoodTillCanceled,
                SharedTimeInForce.ImmediateOrCancel,
                SharedTimeInForce.FillOrKill
            },
            new SharedQuantitySupport(
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.QuoteAssetQuantity,
                SharedQuantityType.BaseAssetQuantity));

        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).PlaceSpotOrderOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var accountId = await GetAccountId(exchangeParameters, ct).ConfigureAwait(false);
            if (accountId == null)
                return new ExchangeWebResult<SharedId>(Exchange, new ServerError("Failed to retrieve account id"));

            var quantity = request.Quantity ?? 0;
            if (request.OrderType == SharedOrderType.Market && request.Side == SharedOrderSide.Buy)
                quantity = request.QuoteQuantity ?? 0;

            var result = await Trading.PlaceOrderAsync(
                accountId.Value,
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                quantity,
                request.Price,
                request.ClientOrderId).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedId(result.Data.ToString()));
        }

        EndpointOptions<GetOrderRequest> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.GetOrderAsync(orderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedSpotOrder(
                order.Data.Symbol,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.Type),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                Fee = order.Data.Fee,
                Price = order.Data.Price,
                Quantity = order.Data.Type == OrderType.Market && order.Data.Side == OrderSide.Buy ? null : order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                QuoteQuantity = order.Data.Type == OrderType.Market && order.Data.Side == OrderSide.Buy ? order.Data.Quantity : null,
                QuoteQuantityFilled = order.Data.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(order.Data.Type)
            });
        }

        EndpointOptions<GetOpenOrdersRequest> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotOrder>>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, ApiType.Spot));
            var order = await Trading.GetOpenOrdersAsync(symbol: symbol).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, default);

            return order.AsExchangeResult(Exchange, order.Data.Select(x => new SharedSpotOrder(
                x.Symbol,
                x.Id.ToString(),
                ParseOrderType(x.Type),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                Fee = x.Fee,
                Price = x.Price,
                Quantity = x.Type == OrderType.Market && x.Side == OrderSide.Buy ? null : x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.Type == OrderType.Market && x.Side == OrderSide.Buy ? x.Quantity : null,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(x.Type)
            }));
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(true, true);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotOrder>>(Exchange, validationError);

#warning pagination?

            var order = await Trading.GetClosedOrdersAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)),
                startTime: request.Filter?.StartTime,
                endTime: request.Filter?.EndTime,
                limit: request.Filter?.Limit).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, default);

            return order.AsExchangeResult(Exchange, order.Data.Select(x => new SharedSpotOrder(
                x.Symbol,
                x.Id.ToString(),
                ParseOrderType(x.Type),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                Fee = x.Fee,
                Price = x.Price,
                Quantity = x.Type == OrderType.Market && x.Side == OrderSide.Buy ? null : x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.Type == OrderType.Market && x.Side == OrderSide.Buy ? x.Quantity : null,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(x.Type)
            }));
        }

        EndpointOptions<GetOrderTradesRequest> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.GetOrderTradesAsync(orderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            return order.AsExchangeResult(Exchange, order.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.Role == OrderRole.Taker ? SharedRole.Taker : SharedRole.Maker
            }));
        }

        PaginatedEndpointOptions<GetUserTradesRequest> ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(true, true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotUserTradesOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            // Determine page token
            long? fromId = null;
            if (pageToken is FromIdToken fromIdToken)
                fromId = long.Parse(fromIdToken.FromToken);

            // Get data
            var order = await Trading.GetUserTradesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)),
                startTime: request.Filter?.StartTime,
                endTime: request.Filter?.EndTime,
                fromId: fromId,
                limit: request.Filter?.Limit).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            // Get next token
            FromIdToken? nextToken = null;
            if (order.Data.Count() == (request.Filter?.Limit ?? 100))
                nextToken = new FromIdToken(order.Data.Max(o => o.TradeId).ToString());

            return order.AsExchangeResult(Exchange, order.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.Role == OrderRole.Taker ? SharedRole.Taker: SharedRole.Maker
            }), nextToken);
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.CancelOrderAsync(orderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedId(order.Data.ToString()));
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Submitted || status == OrderStatus.PreSubmitted || status == OrderStatus.Created || status == OrderStatus.PartiallyFilled) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.PartiallyCanceled || status == OrderStatus.Rejected) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
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

        #region Asset client
        EndpointOptions<GetAssetRequest> IAssetsRestClient.GetAssetOptions { get; } = new EndpointOptions<GetAssetRequest>(false);
        async Task<ExchangeWebResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetsAndNetworksAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset>(Exchange, default);

            var asset = assets.Data.SingleOrDefault();
            if (asset == null)
                return assets.AsExchangeError<SharedAsset>(Exchange, new ServerError("Asset not found"));

            return assets.AsExchangeResult(Exchange, new SharedAsset(asset.Asset.ToUpperInvariant())
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
                })
            });
        }

        EndpointOptions IAssetsRestClient.GetAssetsOptions { get; } = new EndpointOptions("GetAssetsRequest", false);
        async Task<ExchangeWebResult<IEnumerable<SharedAsset>>> IAssetsRestClient.GetAssetsAsync(ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetsOptions.ValidateRequest(Exchange, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedAsset>>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetsAndNetworksAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<IEnumerable<SharedAsset>>(Exchange, default);

            return assets.AsExchangeResult(Exchange, assets.Data.Select(x => new SharedAsset(x.Asset.ToUpperInvariant())
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
                })
            }));
        }

        #endregion

        #region Deposit client

        EndpointOptions<GetDepositAddressesRequest> IDepositRestClient.GetDepositAddressesOptions { get; } = new EndpointOptions<GetDepositAddressesRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedDepositAddress>>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositAddressesOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedDepositAddress>>(Exchange, validationError);

            var depositAddresses = await Account.GetDepositAddressesAsync(request.Asset).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<IEnumerable<SharedDepositAddress>>(Exchange, default);

            return depositAddresses.AsExchangeResult<IEnumerable<SharedDepositAddress>>(Exchange, depositAddresses.Data.Where(x => request.Network == null ? true : x.Network == request.Network).Select(x => new SharedDepositAddress(x.Asset.ToUpperInvariant(), x.Address)
            {
                Tag = x.AddressTag
            }
            ));
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(true, false);
        async Task<ExchangeWebResult<IEnumerable<SharedDeposit>>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositsOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedDeposit>>(Exchange, validationError);

            // Determine page token
            long? from = null;
            if (pageToken is FromIdToken idToken)
                from = long.Parse(idToken.FromToken);

            // Get data
            var deposits = await Account.GetWithdrawDepositHistoryAsync(
                WithdrawDepositType.Deposit,
                request.Asset,
                from: from,
                size: request.Filter?.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!deposits)
                return deposits.AsExchangeResult<IEnumerable<SharedDeposit>>(Exchange, default);

            // Determine next token
            FromIdToken? nextToken = null;
            if (deposits.Data.Count() == (request.Filter?.Limit ?? 100))
                nextToken = new FromIdToken(deposits.Data.Min(x => x.Id).ToString());

            return deposits.AsExchangeResult(Exchange, deposits.Data.Select(x => new SharedDeposit(x.Asset!.ToUpperInvariant(), x.Quantity, x.Status == WithdrawDepositStatus.Confirmed, x.CreateTime)
            {
                Network = x.Network,
                TransactionId = x.TransactionHash,
                Tag = x.AddressTag
            }), nextToken);
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(new[] { 5, 10, 20 }, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)),
                0,
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(true, false);
        async Task<ExchangeWebResult<IEnumerable<SharedWithdrawal>>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IWithdrawalRestClient)this).GetWithdrawalsOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedWithdrawal>>(Exchange, validationError);

            // Determine page token
            long? from = null;
            if (pageToken is FromIdToken idToken)
                from = long.Parse(idToken.FromToken);

            // Get data
            var deposits = await Account.GetWithdrawDepositHistoryAsync(
                WithdrawDepositType.Withdraw,
                request.Asset,
                from: from,
                size: request.Filter?.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!deposits)
                return deposits.AsExchangeResult<IEnumerable<SharedWithdrawal>>(Exchange, default);

            // Determine next token
            FromIdToken? nextToken = null;
            if (deposits.Data.Count() == (request.Filter?.Limit ?? 100))
                nextToken = new FromIdToken(deposits.Data.Min(x => x.Id).ToString());

            return deposits.AsExchangeResult(Exchange, deposits.Data.Select(x => new SharedWithdrawal(x.Asset!.ToUpperInvariant(), x.Address!, x.Quantity, x.Status == WithdrawDepositStatus.Confirmed, x.CreateTime)
            {
                Network = x.Network,
                TransactionId = x.TransactionHash,
                Tag = x.AddressTag,
                Fee = x.Fee
            }), nextToken);
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions();

        async Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.ValidateRequest(Exchange, request, exchangeParameters);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var fee = exchangeParameters?.GetValue<decimal?>(Exchange, "fee");
            if (fee == null)
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError("HTX requires withdrawal fee parameter. Please pass it as exchangeParameter `fee`"));

            // Get data
            var withdrawal = await Account.WithdrawAsync(
                asset: request.Asset,
                fee: fee.Value,
                address: request.Address,
                quantity: request.Quantity,
                network: request.Network,
                addressTag: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal)
                return withdrawal.AsExchangeResult<SharedId>(Exchange, default);

            return withdrawal.AsExchangeResult(Exchange, new SharedId(withdrawal.Data.ToString()));
        }

        #endregion

        private async ValueTask<long?> GetAccountId(ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var accountId = exchangeParameters?.GetValue<long?>(Exchange, "accountId");
            if (accountId != null)
                return accountId;

            if (_accountId != null)
                return _accountId;

            var accounts = await Account.GetAccountsAsync(ct).ConfigureAwait(false);
            if (!accounts)
                return default;

            _accountId = accounts.Data.First(f => f.Type == Enums.AccountType.Spot).Id;
            return _accountId;
        }
    }
}
