using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Objects.Models.UsdtMarginSwap;

namespace HTX.Net.Clients.UsdtFutures
{
    /// <inheritdoc />
    internal class HTXRestClientUsdtFuturesApiTrading : IHTXRestClientUsdtFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientUsdtFuturesApi _baseClient;

        internal HTXRestClientUsdtFuturesApiTrading(HTXRestClientUsdtFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Cancel Orders After

        /// <inheritdoc />
        public async Task<HttpResult<HTXCancelAfter>> CancelOrdersAfterAsync(bool enable, TimeSpan? timeout = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("on_off", enable);
            parameters.Add("time_out", (int?)timeout?.TotalMilliseconds);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/linear-cancel-after", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendAsync<HTXCancelAfter>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Isolated Margin Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderIds>> PlaceIsolatedMarginOrderAsync(
            string contractCode,
            long quantity,
            OrderSide side,
            int leverageRate,
            OrderPriceType orderPriceType,
            decimal? price = null,
            Offset? offset = null,
            decimal? takeProfitTriggerPrice = null,
            decimal? takeProfitOrderPrice = null,
            OrderPriceType? takeProfitOrderPriceType = null,
            decimal? stopLossTriggerPrice = null,
            decimal? stopLossOrderPrice = null,
            OrderPriceType? stopLossOrderPriceType = null,
            bool? reduceOnly = null,
            long? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "volume", quantity },
                { "direction", EnumConverter.GetString(side) },
                { "lever_rate", leverageRate },
                { "channel_code", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) },
                { "order_price_type", EnumConverter.GetString(orderPriceType) }
            };
            parameters.Add("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("offset", EnumConverter.GetString(offset));
            parameters.Add("tp_trigger_price", takeProfitTriggerPrice);
            parameters.Add("tp_order_price", takeProfitOrderPrice);
            parameters.Add("tp_order_price_type", EnumConverter.GetString(takeProfitOrderPriceType));
            parameters.Add("sl_trigger_price", stopLossTriggerPrice);
            parameters.Add("sl_order_price", stopLossOrderPrice);
            parameters.Add("sl_order_price_type", EnumConverter.GetString(stopLossOrderPriceType));
            parameters.Add("reduce_only", reduceOnly == null ? null : reduceOnly.Value ? "1" : "0");
            parameters.Add("client_order_id", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXOrderIds>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Cross Margin Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderIds>> PlaceCrossMarginOrderAsync(
            long quantity,
            OrderSide side,
            int leverageRate,
            OrderPriceType orderPriceType,
            string? contractCode = null,
            string? symbol = null,
            ContractType? contractType = null,
            decimal? price = null,
            Offset? offset = null,
            decimal? takeProfitTriggerPrice = null,
            decimal? takeProfitOrderPrice = null,
            OrderPriceType? takeProfitOrderPriceType = null,
            decimal? stopLossTriggerPrice = null,
            decimal? stopLossOrderPrice = null,
            OrderPriceType? stopLossOrderPriceType = null,
            bool? reduceOnly = null,
            long? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "volume", quantity },
                { "direction", EnumConverter.GetString(side) },
                { "lever_rate", leverageRate },
                { "channel_code", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) },
                { "order_price_type", EnumConverter.GetString(orderPriceType) }
            };
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            parameters.Add("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("offset", EnumConverter.GetString(offset));
            parameters.Add("tp_trigger_price", takeProfitTriggerPrice);
            parameters.Add("tp_order_price", takeProfitOrderPrice);
            parameters.Add("tp_order_price_type", EnumConverter.GetString(takeProfitOrderPriceType));
            parameters.Add("sl_trigger_price", stopLossTriggerPrice);
            parameters.Add("sl_order_price", stopLossOrderPrice);
            parameters.Add("sl_order_price_type", EnumConverter.GetString(stopLossOrderPriceType));
            parameters.Add("reduce_only", reduceOnly == null ? null : reduceOnly.Value ? "1" : "0");
            parameters.Add("client_order_id", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXOrderIds>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /linear-swap-api/v1/swap_batchorder
        // /linear-swap-api/v1/swap_cross_batchorder


        #region Cancel Isolated Margin Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXBatchResult>> CancelIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cancel", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Cross Margin Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXBatchResult>> CancelCrossMarginOrderAsync(long? orderId = null, long? clientOrderId = null, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_cancel", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Isolated Margin Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXBatchResult>> CancelIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderId, IEnumerable<long> clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "order_id", string.Join(",", orderId) },
                { "client_order_id", string.Join(",", clientOrderId) }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cancel", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Cross Margin Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXBatchResult>> CancelCrossMarginOrdersAsync(IEnumerable<long> orderId, IEnumerable<long> clientOrderId, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "order_id", string.Join(",", orderId) },
                { "client_order_id", string.Join(",", clientOrderId) }
            };
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_cancel", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Isolated Margin Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXBatchResult>> CancelAllIsolatedMarginOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("direction", EnumConverter.GetString(side));
            parameters.Add("offset", EnumConverter.GetString(offset));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cancelall", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Cross Margin Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXBatchResult>> CancelAllCrossMarginOrdersAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            parameters.Add("direction", EnumConverter.GetString(side));
            parameters.Add("offset", EnumConverter.GetString(offset));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_cancelall", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Isolated Margin Leverage

        /// <inheritdoc />
        public async Task<HttpResult<HTXIsolatedMarginLeverageRate>> SetIsolatedMarginLeverageAsync(string contractCode, int leverageRate, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "lever_rate", leverageRate },
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_switch_lever_rate", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginLeverageRate>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Cross Margin Leverage

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginLeverageRate>> SetCrossMarginLeverageAsync(int leverageRate, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "lever_rate", leverageRate },
            };
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_switch_lever_rate", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginLeverageRate>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXIsolatedMarginOrder[]>> GetIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_order_info", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginOrder[]>> GetCrossMarginOrderAsync(string? contractCode = null, string? symbol = null, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_order_info", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXIsolatedMarginOrder[]>> GetIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            if (orderIds?.Any() == true)
                parameters.Add("order_id", string.Join(",", orderIds));
            if (clientOrderIds?.Any() == true)
                parameters.Add("client_order_id", string.Join(",", clientOrderIds));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_order_info", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginOrder[]>> GetCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            if (orderIds?.Any() == true)
                parameters.Add("order_id", string.Join(",", orderIds));
            if (clientOrderIds?.Any() == true)
                parameters.Add("client_order_id", clientOrderIds);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_order_info", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Order Details

        /// <inheritdoc />
        public async Task<HttpResult<HTXMarginOrderDetails>> GetIsolatedMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "order_id", orderId }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_order_detail", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendBasicAsync<HTXMarginOrderDetails>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Order Details

        /// <inheritdoc />
        public async Task<HttpResult<HTXMarginOrderDetails>> GetCrossMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "order_id", orderId }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_order_detail", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendBasicAsync<HTXMarginOrderDetails>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXIsolatedMarginOrderPage>> GetIsolatedMarginOpenOrdersAsync(string contractCode, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            parameters.Add("sort_by", sortBy);
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_openorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginOrderPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginOrderPage>> GetCrossMarginOpenOrdersAsync(string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            parameters.Add("sort_by", sortBy);
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_openorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginOrderPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Closed Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXIsolatedMarginOrder[]>> GetIsolatedMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, bool allOrders, IEnumerable<OrderStatusFilter>? status = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract", contractCode);
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            parameters.Add("type", allOrders ? 1 : 2);
            if (status?.Any() == true)
                parameters.Add("status", string.Join(",", status.Select(EnumConverter.GetString)));
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("direct", direction);
            parameters.Add("from_id", fromId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v3/swap_hisorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendAsync<HTXIsolatedMarginOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Closed Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginOrder[]>> GetCrossMarginClosedOrdersAsync(
            string contractCode,
            MarginTradeType tradeType,
            bool allOrders,
            IEnumerable<OrderStatusFilter> status,
            string? pair = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            FilterDirection? direction = null,
            long? fromId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract", contractCode);
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            parameters.Add("type", allOrders ? 1 : 2);
            parameters.Add("status", status?.Any() == true ? string.Join(",", status.Select(EnumConverter.GetString)) : null);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("direct", direction);
            parameters.Add("from_id", fromId);
            parameters.Add("pair", pair);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v3/swap_cross_hisorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendAsync<HTXCrossMarginOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        // /linear-swap-api/v3/swap_hisorders_exact
        // /linear-swap-api/v3/swap_cross_hisorders_exact

        #region Get Isolated Margin User Trades

        /// <inheritdoc />
        public async Task<HttpResult<HTXIsolatedMarginUserTrade[]>> GetIsolatedMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, string? pair = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? filterDirection = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract", contractCode);
            parameters.Add("trade_type", tradeType);
            parameters.Add("pair", pair);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("direct", filterDirection);
            parameters.Add("from_id", fromId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v3/swap_matchresults", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendAsync<HTXIsolatedMarginUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin User Trades

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginUserTrade[]>> GetCrossMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, string? pair = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? filterDirection = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract", contractCode);
            parameters.Add("trade_type", tradeType);
            parameters.Add("pair", pair);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("direct", filterDirection);
            parameters.Add("from_id", fromId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v3/swap_cross_matchresults", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendAsync<HTXCrossMarginUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        // /linear-swap-api/v3/swap_matchresults_exact
        // /linear-swap-api/v3/swap_cross_matchresults_exact

        #region Close Isolated Margin Position

        /// <inheritdoc />
        public async Task<HttpResult<HTXClosePositionResult>> CloseIsolatedMarginPositionAsync(string contractCode, OrderSide direction, long? clientOrderId = null, LightningPriceType? orderPriceType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("direction", direction);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("order_price_type", orderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_lightning_close_position", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXClosePositionResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close Cross Margin Position

        /// <inheritdoc />
        public async Task<HttpResult<HTXClosePositionResult>> CloseCrossMarginPositionAsync(OrderSide direction, string? contractCode = null, string? pair = null, ContractType? contractType = null, long? clientOrderId = null, LightningPriceType? orderPriceType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("direction", direction);
            parameters.Add("contractType", contractType);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("order_price_type", orderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_lightning_close_position", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXClosePositionResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Isolated Margin Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderIds>> PlaceIsolatedMarginTriggerOrderAsync(string contractCode, TriggerType triggerType, decimal triggerPrice, decimal quantity, OrderSide side, Offset? offset = null, bool? reduceOnly = null, decimal? orderPrice = null, OrderPriceType? orderPriceType = null, int? leverageRate = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "channel_code", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
            };
            parameters.Add("contract_code", contractCode);
            parameters.Add("trigger_type", triggerType);
            parameters.Add("trigger_price", triggerPrice);
            parameters.Add("volume", quantity);
            parameters.Add("direction", side);
            parameters.Add("offset", offset);
            parameters.Add("reduce_only", reduceOnly == null ? null : reduceOnly == true ? 1 : 0);
            parameters.Add("order_price", orderPrice);
            parameters.Add("order_price_type", orderPriceType);
            parameters.Add("lever_rate", leverageRate);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_trigger_order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXOrderIds>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Cross Margin Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderIds>> PlaceCrossMarginTriggerOrderAsync(TriggerType triggerType, decimal triggerPrice, decimal quantity, OrderSide side, string? contractCode = null, string? pair = null, ContractType? contractType = null, Offset? offset = null, bool? reduceOnly = null, decimal? orderPrice = null, OrderPriceType? orderPriceType = null, int? leverageRate = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "channel_code", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
            };
            parameters.Add("contract_code", contractCode);
            parameters.Add("trigger_type", triggerType);
            parameters.Add("trigger_price", triggerPrice);
            parameters.Add("volume", quantity);
            parameters.Add("direction", side);
            parameters.Add("contract_type", contractType);
            parameters.Add("offset", offset);
            parameters.Add("pair", pair);
            parameters.Add("reduce_only", reduceOnly == null ? null : reduceOnly == true ? 1 : 0);
            parameters.Add("order_price", orderPrice);
            parameters.Add("order_price_type", orderPriceType);
            parameters.Add("lever_rate", leverageRate);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_trigger_order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXOrderIds>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Isolated Margin Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelIsolatedMarginTriggerOrderAsync(string contractCode, string orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_trigger_cancel", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result;

            if (result.Data.Errors.Any())
            {
                var error = result.Data.Errors.First();
                return HttpResult.Fail<HTXTriggerOrderResult>(result, new ServerError(error.ErrorCode, _baseClient.GetErrorInfo(error.ErrorCode, error.ErrorMessage)));
            }

            return result;
        }

        #endregion

        #region Cancel Cross Margin Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelCrossMarginTriggerOrderAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            parameters.Add("pair", pair);
            parameters.Add("contract_type", contractType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_trigger_cancel", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result;

            if (result.Data.Errors.Any())
            {
                var error = result.Data.Errors.First();
                return HttpResult.Fail<HTXTriggerOrderResult>(result, new ServerError(error.ErrorCode, _baseClient.GetErrorInfo(error.ErrorCode, error.ErrorMessage)));
            }

            return result;
        }

        #endregion

        #region Cancel All Isolated Margin Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTriggerOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("direction", side);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_trigger_cancelall", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Isolated Margin Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelAllCrossMarginTriggerOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("contract_type", contractType);
            parameters.Add("direction", side);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_trigger_cancelall", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Open Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderPage>> GetIsolatedMarginOpenTriggerOrdersAsync(string contractCode, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_trigger_openorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Open Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossTriggerOrderPage>> GetCrossMarginOpenTriggerOrdersAsync(string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_trigger_openorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossTriggerOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Trigger Order History

        /// <inheritdoc />
        public async Task<HttpResult<HTXClosedTriggerOrderPage>> GetIsolatedMarginTriggerOrderHistoryAsync(string contractCode, MarginTradeType tradeType, int daysPast, OrderStatusFilter status, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            parameters.Add("create_date", daysPast);
            parameters.Add("status", status);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            parameters.Add("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_trigger_hisorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXClosedTriggerOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Trigger Order History

        /// <inheritdoc />
        public async Task<HttpResult<HTXClosedCrossTriggerOrderPage>> GetCrossMarginTriggerOrderHistoryAsync(MarginTradeType tradeType, int daysPast, OrderStatusFilter status, string? contractCode = null, string? pair = null, ContractType? contractType = null, int? page = null, int? pageIndex = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("contract_type", contractType);
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            parameters.Add("create_date", daysPast);
            parameters.Add("status", status);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageIndex);
            parameters.Add("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_trigger_hisorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXClosedCrossTriggerOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Isolated Margin Tp Sl

        /// <inheritdoc />
        public async Task<HttpResult<HTXTpSlResult>> SetIsolatedMarginTpSlAsync(string contractCode, OrderSide side, decimal quantity, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("direction", side);
            parameters.Add("volume", quantity);
            parameters.Add("tp_trigger_price", takeProfitTriggerPrice);
            parameters.Add("tp_order_price", takeProfitOrderPrice);
            parameters.Add("tp_order_price_type", takeProfitOrderPriceType);
            parameters.Add("sl_trigger_price", stopLossTriggerPrice);
            parameters.Add("sl_order_price", stopLossOrderPrice);
            parameters.Add("sl_order_price_type", stopLossOrderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_tpsl_order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTpSlResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Cross Margin Tp Sl

        /// <inheritdoc />
        public async Task<HttpResult<HTXTpSlResult>> SetCrossMarginTpSlAsync(OrderSide side, decimal quantity, string? contractCode = null, string? pair = null, ContractType? contractType = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("contract_type", contractType);
            parameters.Add("direction", side);
            parameters.Add("volume", quantity);
            parameters.Add("tp_trigger_price", takeProfitTriggerPrice);
            parameters.Add("tp_order_price", takeProfitOrderPrice);
            parameters.Add("tp_order_price_type", takeProfitOrderPriceType);
            parameters.Add("sl_trigger_price", stopLossTriggerPrice);
            parameters.Add("sl_order_price", stopLossOrderPrice);
            parameters.Add("sl_order_price_type", stopLossOrderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_tpsl_order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTpSlResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Isolated Margin Tp Sl

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelIsolatedMarginTpSlAsync(string contractCode, string orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_tpsl_cancel", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Cross Margin Tp Sl

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelCrossMarginTpSlAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("contract_type", contractType);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_tpsl_cancel", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Isolated Margin Tp Sl

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTpSlAsync(string contractCode, OrderSide? side = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("direction", side);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_tpsl_cancelall", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Cross Margin Tp Sl

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelAllCrossMarginTpSlAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("pair", pair);
            parameters.Add("contract_code", contractCode);
            parameters.Add("contract_type", contractType);
            parameters.Add("direction", side);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_tpsl_cancelall", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Open Tp Sl Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXTpSlOrderPage>> GetIsolatedMarginOpenTpSlOrdersAsync(string? contractCode = null, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_tpsl_openorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTpSlOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Open Tp Sl Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossTpSlOrderPage>> GetCrossMarginOpenTpSlOrdersAsync(string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_tpsl_openorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossTpSlOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Tp Sl History

        /// <inheritdoc />
        public async Task<HttpResult<HTXTpSlClosedOrderPage>> GetIsolatedMarginTpSlHistoryAsync(string contractCode, IEnumerable<TpSlStatus> tpSlOrderStatus, int daysPast, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("status", string.Join(",", tpSlOrderStatus.Select(EnumConverter.GetString)));
            parameters.Add("create_date", daysPast);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            parameters.Add("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_tpsl_hisorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTpSlClosedOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Tp Sl History

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossTpSlClosedOrderPage>> GetCrossMarginTpSlHistoryAsync(IEnumerable<TpSlStatus> tpSlOrderStatus, int daysPast, string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("status", string.Join(",", tpSlOrderStatus.Select(EnumConverter.GetString)));
            parameters.Add("create_date", daysPast);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            parameters.Add("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_tpsl_hisorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossTpSlClosedOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Position Open Tp Sl Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXPositionOpenTpSlOrders>> GetIsolatedMarginPositionOpenTpSlInfoAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_relation_tpsl_order", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXPositionOpenTpSlOrders>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Position Open Tp Sl Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossPositionOpenTpSlOrders>> GetCrossMarginPositionOpenTpSlInfoAsync(long orderId, string? contractCode = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("pair", pair);
            parameters.Add("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_relation_tpsl_order", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossPositionOpenTpSlOrders>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Isolated Margin Trailing Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderIds>> PlaceIsolatedMarginTrailingOrderAsync(string contractCode, bool reduceOnly, OrderSide side, Offset offset, int leverageRate, decimal quantity, decimal callbackRate, decimal activePrice, OrderPriceType orderPriceType, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "channel_code", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
            };
            parameters.Add("contract_code", contractCode);
            parameters.Add("reduce_only", reduceOnly ? 1 : 0);
            parameters.Add("direction", side);
            parameters.Add("offset", offset);
            parameters.Add("lever_rate", leverageRate);
            parameters.Add("volume", quantity);
            parameters.Add("callback_rate", callbackRate);
            parameters.Add("active_price", activePrice);
            parameters.Add("order_price_type", orderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_track_order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXOrderIds>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Cross Margin Trailing Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderIds>> PlaceCrossMarginTrailingOrderAsync(OrderSide side, Offset offset, int leverageRate, decimal quantity, decimal callbackRate, decimal activePrice, OrderPriceType orderPriceType, string? contractCode = null, string? pair = null, ContractType? contractType = null, bool? reduceOnly = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "channel_code", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
            };
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("contract_type", contractType);
            if (reduceOnly.HasValue)
                parameters.Add("reduce_only", reduceOnly.Value ? 1 : 0);
            parameters.Add("direction", side);
            parameters.Add("offset", offset);
            parameters.Add("lever_rate", leverageRate);
            parameters.Add("volume", quantity);
            parameters.Add("callback_rate", callbackRate);
            parameters.Add("active_price", activePrice);
            parameters.Add("order_price_type", orderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_cross_track_order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXOrderIds>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Isolated Margin Trailing Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelIsolatedMarginTrailingOrderAsync(string contractCode, string orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_track_cancel", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Cross Margin Trailing Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelCrossMarginTrailingOrderAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("contract_type", contractType);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_track_cancel", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Isolated Margin Trailing Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTrailingOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("direction", side);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_track_cancelall", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Cross Margin Trailing Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXTriggerOrderResult>> CancelAllCrossMarginTrailingOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("contract_type", contractType);
            parameters.Add("direction", side);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_track_cancelall", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Isolated Margin Trailing Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXTrailingOrderPage>> GetOpenIsolatedMarginTrailingOrdersAsync(string contractCode, MarginTradeType? tradeType = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("trade_type", tradeType);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_track_openorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTrailingOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Cross Margin Trailing Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossTrailingOrderPage>> GetOpenCrossMarginTrailingOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, MarginTradeType? tradeType = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("contract_type", contractType);
            parameters.Add("pair", pair);
            parameters.Add("trade_type", tradeType);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_track_openorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossTrailingOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Isolated Margin Trailing Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXTrailingClosedOrderPage>> GetClosedIsolatedMarginTrailingOrdersAsync(string contractCode, IEnumerable<TpSlStatus> tpSlOrderStatus, MarginTradeType tradeType, int daysPast, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("status", string.Join(",", tpSlOrderStatus.Select(EnumConverter.GetString)));
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            parameters.Add("create_date", daysPast);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            parameters.Add("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_track_hisorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTrailingClosedOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Cross Margin Trailing Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossTrailingClosedOrderPage>> GetClosedCrossMarginTrailingOrdersAsync(IEnumerable<TpSlStatus> tpSlOrderStatus, MarginTradeType tradeType, int daysPast, string? contractCode = null, string? pair = null, ContractType? contractType = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("contract_type", contractType);
            parameters.Add("status", string.Join(",", tpSlOrderStatus.Select(EnumConverter.GetString)));
            parameters.Add("trade_type", tradeType, EnumSerialization.Number);
            parameters.Add("create_date", daysPast);
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            parameters.Add("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_track_hisorders", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossTrailingClosedOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
