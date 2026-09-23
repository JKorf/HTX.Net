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
        public async Task<HttpResult<long>> PlaceOrderAsync(
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

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "account-id", accountId },
                { "symbol", symbol },
                { "type", orderType }
            };
            parameters.Add("amount", quantity);

            clientOrderId = LibraryHelpers.ApplyBrokerId(
                clientOrderId,
                LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                64,
                _baseClient.ClientOptions.AllowAppendingClientOrderId);

            parameters.Add("client-order-id", clientOrderId);
            parameters.Add("stop-price", stopPrice);
            parameters.Add("source", source);
            parameters.Add("operator", stopOperator);
            parameters.Add("price", price);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "v1/order/orders/place", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Place Multiple Order

        /// <inheritdoc />
        public async Task<HttpResult<CallResult<HTXBatchPlaceResult>[]>> PlaceMultipleOrderAsync(
            IEnumerable<HTXOrderRequest> orders,
            CancellationToken ct = default)
        {
            var data = new List<Parameters>();
            foreach (var order in orders)
            {
                var orderType = EnumConverter.GetString(order.Side) + "-" + EnumConverter.GetString(order.Type);

                var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
                {
                    { "account-id", order.AccountId },
                    { "symbol", order.Symbol.ToLowerInvariant() },
                    { "type", orderType }
                };
                parameters.Add("amount", order.Quantity);
                order.ClientOrderId = LibraryHelpers.ApplyBrokerId(
                    order.ClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                    64,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

                parameters.Add("client-order-id", order.ClientOrderId);
                parameters.Add("stop-price", order.StopPrice);
                parameters.Add("source", order.Source);
                parameters.Add("operator", order.StopOperator);
                parameters.Add("price", order.Price);
                data.Add(parameters);
            }

            var orderParameters = new Parameters(data.ToArray(), HTXExchange._spotParameterSerializationSettings);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "v1/order/batch-orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var response = await _baseClient.SendBasicAsync<HTXBatchPlaceResult[]>(request, orderParameters, ct).ConfigureAwait(false);

            if (!response.Success)
                return HttpResult.Fail<CallResult<HTXBatchPlaceResult>[]>(response);

            var result = new List<CallResult<HTXBatchPlaceResult>>();
            foreach (var item in response.Data)
            {
                result.Add(!string.IsNullOrEmpty(item.ErrorCode)
                    ? CallResult.Fail<HTXBatchPlaceResult>(new ServerError(item.ErrorCode!, _baseClient.GetErrorInfo(item.ErrorCode!, item.ErrorMessage)))
                    : CallResult.Ok(item));
            }

            if (result.All(x => !x.Success))
                return HttpResult.Fail(response, new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, false, "All orders failed")), result.ToArray());

            return HttpResult.Ok(response, result.ToArray());
        }

        #endregion

        #region Place Margin Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderId>> PlaceMarginOrderAsync(
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

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "account-id", accountId },
                { "symbol", symbol },
                { "type", orderType }
            };
            parameters.Add("trade-purpose", purpose);
            parameters.Add("source", source);

            parameters.Add("amount", quantity);
            parameters.Add("market-amount", quoteQuantity);
            parameters.Add("borrow-amount", borrowQuantity);
            parameters.Add("price", price);
            parameters.Add("stop-price", stopPrice);
            parameters.Add("operator", stopOperator);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "v1/order/auto/place", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<long>> CancelOrderAsync(long orderId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"v1/order/orders/{orderId}/submitcancel", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<long?>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Cancel Order By Client Order Id

        /// <inheritdoc />
        public async Task<HttpResult<long>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId = LibraryHelpers.ApplyBrokerId(
                clientOrderId,
                LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                64,
                _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "client-order-id", clientOrderId }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "v1/order/orders/submitCancelClientOrder", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXOpenOrder[]>> GetOpenOrdersAsync(
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

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("account-id", accountId);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side);
            parameters.Add("size", limit);
            parameters.Add("types", orderTypes?.Any() != true ? null : string.Join(",", orderTypes.Select(EnumConverter.GetString)));
            parameters.Add("from", fromId);
            parameters.Add("direct", direction);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v1/order/openOrders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOpenOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Orders By Criteria

        /// <inheritdoc />
        public async Task<HttpResult<HTXByCriteriaCancelResult>> CancelOrdersByCriteriaAsync(long? accountId = null, IEnumerable<string>? symbols = null, Enums.OrderSide? side = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("account-id", accountId?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("symbol", symbols == null ? null : string.Join(",", symbols.Select(x => x.ToLowerInvariant())));
            parameters.Add("side", side);
            parameters.Add("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "v1/order/orders/batchCancelOpenOrders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXByCriteriaCancelResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXBatchCancelResult>> CancelOrdersAsync(IEnumerable<long>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default)
        {
            if (orderIds == null && clientOrderIds == null)
                throw new ArgumentException("Either orderIds or clientOrderIds should be provided");

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.AddArray("order-ids", orderIds?.Select(s => s.ToString(CultureInfo.InvariantCulture)).ToArray());
            parameters.AddArray("client-order-ids", clientOrderIds?.Select(s =>
                LibraryHelpers.ApplyBrokerId(
                    s,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                    64,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId)).ToArray());

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "v1/order/orders/batchcancel", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXBatchCancelResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v1/order/cancelAllOrders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<object>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
        // /v2/algo-orders/cancel-all-after

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/order/orders/{orderId}", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrder>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order By Client Order Id

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId = LibraryHelpers.ApplyBrokerId(
                clientOrderId,
                LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                64,
                _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "clientOrderId", clientOrderId }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/order/orders/getClientOrder", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Trades

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderTrade[]>> GetOrderTradesAsync(long orderId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/order/orders/{orderId}/matchresults", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrderTrade[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Closed Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrder[]>> GetClosedOrdersAsync(string symbol, IEnumerable<OrderStatus>? states = null, IEnumerable<Enums.OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();

            states ??= new OrderStatus[] { OrderStatus.Filled, OrderStatus.Canceled, OrderStatus.PartiallyCanceled };

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "states", string.Join(",", states.Select(s => EnumConverter.GetString(s))) }
            };
            parameters.Add("symbol", symbol);
            parameters.Add("start-time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.Add("end-time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.Add("types", types == null ? null : string.Join(",", types.Select(s => EnumConverter.GetString(s))));
            parameters.Add("from", fromId);
            parameters.Add("direct", direction);
            parameters.Add("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/order/orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Historical Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrder[]>> GetHistoricalOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("start-time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.Add("end-time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.Add("direct", direction);
            parameters.Add("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/order/history", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderTrade[]>> GetUserTradesAsync(string? symbol = null, IEnumerable<Enums.OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("start-time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.Add("end-time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.Add("types", types == null ? null : string.Join(",", types.Select(s => EnumConverter.GetString(s))));
            parameters.Add("from", fromId);
            parameters.Add("direct", direction);
            parameters.Add("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/order/matchresults", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXOrderTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Conditional Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXPlacedConditionalOrder>> PlaceConditionalOrderAsync(
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

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "accountId", accountId },
                { "symbol", symbol },
                { "orderSide", EnumConverter.GetString(side) },
                { "orderType", EnumConverter.GetString(type) }
            };
            parameters.Add("clientOrderId", clientOrderId);
            parameters.Add("stopPrice", stopPrice);

            parameters.Add("orderPrice", price);
            parameters.Add("orderSize", quantity);
            parameters.Add("orderValue", quoteQuantity);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("trailingRate", trailingRate);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"v2/algo-orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXPlacedConditionalOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Conditional Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXConditionalOrderCancelResult>> CancelConditionalOrdersAsync(IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "clientOrderIds", clientOrderIds.Select(x =>
                    LibraryHelpers.ApplyBrokerId(
                        x,
                        LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                        64,
                        _baseClient.ClientOptions.AllowAppendingClientOrderId)).ToArray() }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"v2/algo-orders/cancellation", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXConditionalOrderCancelResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Conditional Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXConditionalOrder[]>> GetOpenConditionalOrdersAsync(long? accountId = null, string? symbol = null, OrderSide? side = null, ConditionalOrderType? type = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("accountId", accountId);
            parameters.Add("symbol", symbol);
            parameters.Add("orderSide", EnumConverter.GetString(side));
            parameters.Add("orderType", EnumConverter.GetString(type));
            parameters.Add("sort", sort);
            parameters.Add("limit", limit);
            parameters.Add("fromId", fromId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v2/algo-orders/opening", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXConditionalOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Closed Conditional Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXConditionalOrder[]>> GetClosedConditionalOrdersAsync(
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

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol },
                { "orderStatus", EnumConverter.GetString(status) }
            };
            parameters.Add("accountId", accountId);
            parameters.Add("orderSide", EnumConverter.GetString(side));
            parameters.Add("orderType", EnumConverter.GetString(type));
            parameters.Add("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.Add("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.Add("sort", sort);
            parameters.Add("limit", limit);
            parameters.Add("fromId", fromId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v2/algo-orders/history", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXConditionalOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Conditional Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXConditionalOrder>> GetConditionalOrderAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId = LibraryHelpers.ApplyBrokerId(
                clientOrderId,
                LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange),
                64,
                _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "clientOrderId", clientOrderId }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v2/algo-orders/specific", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXConditionalOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
