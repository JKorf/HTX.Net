using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class HTXRestClientSpotApiTrading : IHTXRestClientSpotApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientSpotApi _baseClient;

        internal HTXRestClientSpotApiTrading(HTXRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PlaceOrderAsync(
            long accountId,
            string symbol,
            Enums.OrderSide side,
            Enums.OrderType type,
            decimal quantity,
            decimal? price = null,
            string? clientOrderId = null,
            SourceType? source = null,
            decimal? stopPrice = null, 
            Operator? stopOperator = null,
            CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            var orderType = EnumConverter.GetString(side) + "-" + EnumConverter.GetString(type);

            var parameters = new ParameterCollection()
            {
                { "account-id", accountId },
                { "symbol", symbol },
                { "type", orderType }
            };
            parameters.AddString("amount", quantity);

            clientOrderId = LibraryHelpers.ApplyBrokerId(
                clientOrderId,
                LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                64,
                _baseClient.ClientOptions.AllowAppendingClientOrderId);

            parameters.AddOptionalParameter("client-order-id", clientOrderId);
            parameters.AddOptionalString("stop-price", stopPrice);
            parameters.AddOptionalEnum("source", source);
            parameters.AddOptionalEnum("operator", stopOperator);
            parameters.AddOptionalString("price", price);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "v1/order/orders/place", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Multiple Order

        /// <inheritdoc />
        public async Task<WebCallResult<CallResult<HTXBatchPlaceResult>[]>> PlaceMultipleOrderAsync(
            IEnumerable<HTXOrderRequest> orders,
            CancellationToken ct = default)
        {
            var data = new List<ParameterCollection>();
            foreach (var order in orders)
            {
                var orderType = EnumConverter.GetString(order.Side) + "-" + EnumConverter.GetString(order.Type);

                var parameters = new ParameterCollection()
                {
                    { "account-id", order.AccountId },
                    { "symbol", order.Symbol.ToLowerInvariant() },
                    { "type", orderType }
                };
                parameters.AddString("amount", order.Quantity);
                order.ClientOrderId = LibraryHelpers.ApplyBrokerId(
                    order.ClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                    64,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

                parameters.AddOptionalParameter("client-order-id", order.ClientOrderId);
                parameters.AddOptionalString("stop-price", order.StopPrice);
                parameters.AddOptionalEnum("source", order.Source);
                parameters.AddOptionalEnum("operator", order.StopOperator);
                parameters.AddOptionalString("price", order.Price);
                data.Add(parameters);
            }

            var orderParameters = new ParameterCollection();
            orderParameters.SetBody(data.ToArray());

            var request = _definitions.GetOrCreate(HttpMethod.Post, "v1/order/batch-orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var response = await _baseClient.SendBasicAsync<HTXBatchPlaceResult[]>(request, orderParameters, ct).ConfigureAwait(false);

            if (!response.Success)
                return response.As<CallResult<HTXBatchPlaceResult>[]>(default);

            var result = new List<CallResult<HTXBatchPlaceResult>>();
            foreach (var item in response.Data)
            {
                result.Add(!string.IsNullOrEmpty(item.ErrorCode)
                    ? new CallResult<HTXBatchPlaceResult>(new ServerError(item.ErrorCode!, _baseClient.GetErrorInfo(item.ErrorCode!, item.ErrorMessage)))
                    : new CallResult<HTXBatchPlaceResult>(item));
            }

            if (result.All(x => !x.Success))
                return response.AsErrorWithData(new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, false, "All orders failed")), result.ToArray());

            return response.As(result.ToArray());
        }

        #endregion

        #region Place Margin Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderId>> PlaceMarginOrderAsync(
            long accountId,
            string symbol,
            Enums.OrderSide side,
            Enums.OrderType type,
            Enums.MarginPurpose purpose,
            SourceType source,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            decimal? borrowQuantity = null,
            decimal? price = null,
            decimal? stopPrice = null,
            Operator? stopOperator = null,
            CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            if (type == Enums.OrderType.StopLimit)
                throw new ArgumentException("Stop limit orders not supported by API");

            var orderType = EnumConverter.GetString(side) + "-" + EnumConverter.GetString(type);

            var parameters = new ParameterCollection()
            {
                { "account-id", accountId },
                { "symbol", symbol },
                { "type", orderType }
            };
            parameters.AddEnum("trade-purpose", purpose);
            parameters.AddEnum("source", source);

            parameters.AddOptionalString("amount", quantity);
            parameters.AddOptionalString("market-amount", quoteQuantity);
            parameters.AddOptionalString("borrow-amount", borrowQuantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptionalString("stop-price", stopPrice);
            parameters.AddOptionalEnum("operator", stopOperator);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "v1/order/auto/place", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<long>> CancelOrderAsync(long orderId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/order/orders/{orderId}/submitcancel", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<long>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order By Client Order Id

        /// <inheritdoc />
        public async Task<WebCallResult<long>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId = LibraryHelpers.ApplyBrokerId(
                clientOrderId,
                LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                64,
                _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new ParameterCollection()
            {
                { "client-order-id", clientOrderId }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, "v1/order/orders/submitCancelClientOrder", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOpenOrder[]>> GetOpenOrdersAsync(
            long? accountId = null,
            string? symbol = null,
            OrderSide? side = null,
            IEnumerable<OrderType>? orderTypes = null,
            string? fromId = null,
            FilterDirection? direction = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("account-id", accountId);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptional("types", orderTypes?.Any() != true ? null : string.Join(",", orderTypes.Select(EnumConverter.GetString)));
            parameters.AddOptional("from", fromId);
            parameters.AddOptionalEnum("direct", direction);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v1/order/openOrders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOpenOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Orders By Criteria

        /// <inheritdoc />
        public async Task<WebCallResult<HTXByCriteriaCancelResult>> CancelOrdersByCriteriaAsync(long? accountId = null, IEnumerable<string>? symbols = null, Enums.OrderSide? side = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("account-id", accountId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbols == null ? null : string.Join(",", symbols.Select(x => x.ToLowerInvariant())));
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalParameter("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "v1/order/orders/batchCancelOpenOrders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXByCriteriaCancelResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchCancelResult>> CancelOrdersAsync(IEnumerable<long>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default)
        {
            if (orderIds == null && clientOrderIds == null)
                throw new ArgumentException("Either orderIds or clientOrderIds should be provided");

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("order-ids", orderIds?.Select(s => s.ToString(CultureInfo.InvariantCulture)).ToArray());
            parameters.AddOptionalParameter("client-order-ids", clientOrderIds?.Select(s =>
                LibraryHelpers.ApplyBrokerId(
                    s,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                    64,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId)).ToArray());

            var request = _definitions.GetOrCreate(HttpMethod.Post, "v1/order/orders/batchcancel", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXBatchCancelResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v1/order/cancelAllOrders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<object>(request, parameters, ct).ConfigureAwait(false);
            return result.AsDataless();
        }

        #endregion
        // /v2/algo-orders/cancel-all-after

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/order/orders/{orderId}", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrder>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order By Client Order Id

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId = LibraryHelpers.ApplyBrokerId(
                clientOrderId,
                LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                64,
                _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new ParameterCollection()
            {
                { "clientOrderId", clientOrderId }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/order/orders/getClientOrder", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Trades

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderTrade[]>> GetOrderTradesAsync(long orderId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/order/orders/{orderId}/matchresults", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrderTrade[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Closed Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrder[]>> GetClosedOrdersAsync(string symbol, IEnumerable<OrderStatus>? states = null, IEnumerable<Enums.OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();

            states ??= new OrderStatus[] { OrderStatus.Filled, OrderStatus.Canceled, OrderStatus.PartiallyCanceled };

            var parameters = new ParameterCollection()
            {
                { "states", string.Join(",", states.Select(s => EnumConverter.GetString(s))) }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("start-time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end-time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("types", types == null ? null : string.Join(",", types.Select(s => EnumConverter.GetString(s))));
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalEnum("direct", direction);
            parameters.AddOptionalParameter("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/order/orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Historical Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrder[]>> GetHistoricalOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("start-time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end-time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalEnum("direct", direction);
            parameters.AddOptionalParameter("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/order/history", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderTrade[]>> GetUserTradesAsync(string? symbol = null, IEnumerable<Enums.OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("start-time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end-time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("types", types == null ? null : string.Join(",", types.Select(s => EnumConverter.GetString(s))));
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalEnum("direct", direction);
            parameters.AddOptionalParameter("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/order/matchresults", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrderTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Conditional Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPlacedConditionalOrder>> PlaceConditionalOrderAsync(
            long accountId, 
            string symbol, 
            OrderSide side,
            ConditionalOrderType type,
            decimal stopPrice,
            decimal? quantity = null, 
            decimal? price = null,
            decimal? quoteQuantity = null,
            decimal? trailingRate = null,
            TimeInForce? timeInForce = null,
            string? clientOrderId = null, 
            CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();

            if (clientOrderId != null)
            {
                clientOrderId = LibraryHelpers.ApplyBrokerId(
                    clientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                    64,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new ParameterCollection()
            {
                { "accountId", accountId },
                { "symbol", symbol },
                { "orderSide", EnumConverter.GetString(side) },
                { "orderType", EnumConverter.GetString(type) }
            };
            parameters.AddOptional("clientOrderId", clientOrderId);
            parameters.AddString("stopPrice", stopPrice);

            parameters.AddOptionalString("orderPrice", price);
            parameters.AddOptionalString("orderSize", quantity);
            parameters.AddOptionalString("orderValue", quoteQuantity);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptionalString("trailingRate", trailingRate);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v2/algo-orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXPlacedConditionalOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Conditional Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXConditionalOrderCancelResult>> CancelConditionalOrdersAsync(IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "clientOrderIds", clientOrderIds.Select(x =>
                    LibraryHelpers.ApplyBrokerId(
                        x,
                        LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                        64,
                        _baseClient.ClientOptions.AllowAppendingClientOrderId)).ToArray() }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v2/algo-orders/cancellation", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXConditionalOrderCancelResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Conditional Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXConditionalOrder[]>> GetOpenConditionalOrdersAsync(long? accountId = null, string? symbol = null, OrderSide? side = null, ConditionalOrderType? type = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("accountId", accountId);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderSide", EnumConverter.GetString(side));
            parameters.AddOptionalParameter("orderType", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("sort", sort);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("fromId", fromId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v2/algo-orders/opening", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXConditionalOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Closed Conditional Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXConditionalOrder[]>> GetClosedConditionalOrdersAsync(
            string symbol, 
            ConditionalOrderStatus status, 
            long? accountId = null, 
            OrderSide? side = null, 
            ConditionalOrderType? type = null, 
            DateTime? startTime = null,
            DateTime? endTime = null,
            string? sort = null,
            int? limit = null,
            long? fromId = null,
            CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
                { "orderStatus", EnumConverter.GetString(status) }
            };
            parameters.AddOptionalParameter("accountId", accountId);
            parameters.AddOptionalParameter("orderSide", EnumConverter.GetString(side));
            parameters.AddOptionalParameter("orderType", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("sort", sort);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("fromId", fromId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v2/algo-orders/history", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXConditionalOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Conditional Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXConditionalOrder>> GetConditionalOrderAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId = LibraryHelpers.ApplyBrokerId(
                clientOrderId,
                LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                64,
                _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new ParameterCollection()
            {
                { "clientOrderId", clientOrderId }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v2/algo-orders/specific", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXConditionalOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
