using HTX.Net.Clients.FuturesApi;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtMarginSwapApi;
using HTX.Net.Objects.Models.UsdtMarginSwap;

namespace HTX.Net.Clients.UsdtMarginSwapApi
{
    /// <inheritdoc />
    internal class HTXRestClientUsdtMarginSwapApiTrading : IHTXRestClientUsdtMarginSwapApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientUsdtMarginSwapApi _baseClient;

        internal HTXRestClientUsdtMarginSwapApiTrading(HTXRestClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Cancel Orders After

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCancelAfter>> CancelOrdersAfterAsync(bool enable, TimeSpan? timeout = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("on_off", enable);
            parameters.AddOptional("time_out", (int?)timeout?.TotalMilliseconds);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/linear-cancel-after", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXCancelAfter>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Isolated Margin Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPlacedOrderId>> PlaceIsolatedMarginOrderAsync(
            string contractCode,
            decimal quantity,
            OrderSide side,
            int leverageRate,
            decimal? price = null,
            Offset? offset = null,
            OrderPriceType? orderPriceType = null,
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
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "volume", quantity.ToString(CultureInfo.InvariantCulture) },
                { "direction", EnumConverter.GetString(side) },
                { "lever_rate", leverageRate },
                { "channel_code", _baseClient._brokerId }
            };
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            parameters.AddOptionalParameter("order_price_type", EnumConverter.GetString(orderPriceType));
            parameters.AddOptionalParameter("tp_trigger_price", takeProfitTriggerPrice);
            parameters.AddOptionalParameter("tp_order_price", takeProfitOrderPrice);
            parameters.AddOptionalParameter("tp_order_price_type", EnumConverter.GetString(takeProfitOrderPriceType));
            parameters.AddOptionalParameter("sl_trigger_price", stopLossTriggerPrice);
            parameters.AddOptionalParameter("sl_order_price", stopLossOrderPrice);
            parameters.AddOptionalParameter("sl_order_price_type", EnumConverter.GetString(stopLossOrderPriceType));
            parameters.AddOptionalParameter("reduce_only", reduceOnly == null ? null : reduceOnly.Value ? "1" : "0");
            parameters.AddOptionalParameter("client_order_id", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_order", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXPlacedOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Cross Margin Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPlacedOrderId>> PlaceCrossMarginOrderAsync(
            decimal quantity,
            OrderSide side,
            int leverageRate,
            string? contractCode = null,
            string? symbol = null,
            ContractType? contractType = null,
            decimal? price = null,
            Offset? offset = null,
            OrderPriceType? orderPriceType = null,
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
            var parameters = new ParameterCollection()
            {
                { "volume", quantity.ToString(CultureInfo.InvariantCulture) },
                { "direction", EnumConverter.GetString(side) },
                { "lever_rate", leverageRate },
                { "channel_code", _baseClient._brokerId }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            parameters.AddOptionalParameter("order_price_type", EnumConverter.GetString(orderPriceType));
            parameters.AddOptionalParameter("tp_trigger_price", takeProfitTriggerPrice);
            parameters.AddOptionalParameter("tp_order_price", takeProfitOrderPrice);
            parameters.AddOptionalParameter("tp_order_price_type", EnumConverter.GetString(takeProfitOrderPriceType));
            parameters.AddOptionalParameter("sl_trigger_price", stopLossTriggerPrice);
            parameters.AddOptionalParameter("sl_order_price", stopLossOrderPrice);
            parameters.AddOptionalParameter("sl_order_price_type", EnumConverter.GetString(stopLossOrderPriceType));
            parameters.AddOptionalParameter("reduce_only", reduceOnly == null ? null : reduceOnly.Value ? "1" : "0");
            parameters.AddOptionalParameter("client_order_id", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_order", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXPlacedOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /linear-swap-api/v1/swap_batchorder
        // /linear-swap-api/v1/swap_cross_batchorder


        #region Cancel Isolated Margin Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchResult>> CancelIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Cross Margin Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchResult>> CancelCrossMarginOrderAsync(long? orderId = null, long? clientOrderId = null, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Isolated Margin Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchResult>> CancelIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderId, IEnumerable<long> clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "order_id", string.Join(",", orderId) },
                { "client_order_id", string.Join(",", clientOrderId) }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Cross Margin Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchResult>> CancelCrossMarginOrdersAsync(IEnumerable<long> orderId, IEnumerable<long> clientOrderId, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "order_id", string.Join(",", orderId) },
                { "client_order_id", string.Join(",", clientOrderId) }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Isolated Margin Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchResult>> CancelAllIsolatedMarginOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("direction", EnumConverter.GetString(side));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cancelall", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Cross Margin Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchResult>> CancelAllCrossMarginOrdersAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("direction", EnumConverter.GetString(side));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_cancelall", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXBatchResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Isolated Margin Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<HTXIsolatedMarginLeverageRate>> SetIsolatedMarginLeverageAsync(string contractCode, int leverageRate, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "lever_rate", leverageRate },
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_switch_lever_rate", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginLeverageRate>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Cross Margin Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginLeverageRate>> SetCrossMarginLeverageAsync(int leverageRate, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "lever_rate", leverageRate },
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_switch_lever_rate", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginLeverageRate>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Order

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginOrder>>> GetIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_order_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXIsolatedMarginOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Order

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossMarginOrder>>> GetCrossMarginOrderAsync(string? contractCode = null, string? symbol = null, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_order_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXCrossMarginOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginOrder>>> GetIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            if (orderIds?.Any() == true)
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIds));
            if (clientOrderIds?.Any() == true)
                parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIds));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_order_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXIsolatedMarginOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossMarginOrder>>> GetCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            if (orderIds?.Any() == true)
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIds));
            if (clientOrderIds?.Any() == true)
                parameters.AddOptionalParameter("client_order_id", clientOrderIds);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_order_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXCrossMarginOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Order Details

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMarginOrderDetails>> GetIsolatedMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "order_id", orderId }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_order_detail", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXMarginOrderDetails>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Order Details

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMarginOrderDetails>> GetCrossMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "order_id", orderId }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_order_detail", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXMarginOrderDetails>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXIsolatedMarginOrderPage>> GetIsolatedMarginOpenOrdersAsync(string contractCode, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            parameters.AddOptionalParameter("trade_type", EnumConverter.GetString(tradeType));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_openorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginOrderPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginOrderPage>> GetCrossMarginOpenOrdersAsync(string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            parameters.AddOptionalParameter("trade_type", EnumConverter.GetString(tradeType));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_openorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginOrderPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Closed Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginOrder>>> GetIsolatedMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, bool? allOrders = null, IEnumerable<OrderStatusFilter>? status = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contractCode);
            parameters.AddEnumAsInt("trade_type", tradeType);
            if (allOrders.HasValue)
                parameters.AddOptionalString("type", allOrders.Value ? 1 : 2);
            parameters.AddOptional("status", status?.Any() == true ? string.Join(",", status.Select(EnumConverter.GetString)) : null);
            parameters.AddOptionalMilliseconds("start_time", startTime);
            parameters.AddOptionalMilliseconds("end_time", endTime);
            parameters.AddOptionalEnum("direct", direction);
            parameters.AddOptional("from_id", fromId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v3/swap_hisorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<IEnumerable<HTXIsolatedMarginOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Closed Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossMarginOrder>>> GetCrossMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, string? pair = null, bool? allOrders = null, IEnumerable<OrderStatusFilter>? status = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contractCode);
            parameters.AddEnumAsInt("trade_type", tradeType);
            if (allOrders.HasValue)
                parameters.AddOptionalString("type", allOrders.Value ? 1 : 2);
            parameters.AddOptional("status", status?.Any() == true ? string.Join(",", status.Select(EnumConverter.GetString)) : null);
            parameters.AddOptionalMilliseconds("start_time", startTime);
            parameters.AddOptionalMilliseconds("end_time", endTime);
            parameters.AddOptionalEnum("direct", direction);
            parameters.AddOptional("from_id", fromId);
            parameters.AddOptional("pair", pair);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v3/swap_hisorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<IEnumerable<HTXCrossMarginOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        // /linear-swap-api/v3/swap_hisorders_exact
        // /linear-swap-api/v3/swap_cross_hisorders_exact

        #region Get Cross Margin User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<HTXIsolatedMarginUserTradePage>> GetIsolatedMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
#warning Update to V3
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_matchresults", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginUserTradePage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginUserTradePage>> GetCrossMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
#warning Update to V3
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_matchresults", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginUserTradePage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /linear-swap-api/v3/swap_matchresults_exact
        // /linear-swap-api/v3/swap_cross_matchresults_exact

        #region Close Isolated Margin Position

        /// <inheritdoc />
        public async Task<WebCallResult<HTXClosePositionResult>> CloseIsolatedMarginPositionAsync(string contractCode, OrderSide direction, long? clientOrderId = null, LightningPriceType? orderPriceType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddEnum("direction", direction);
            parameters.AddOptional("client_order_id", clientOrderId);
            parameters.AddOptionalEnum("order_price_type", orderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_lightning_close_position", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXClosePositionResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close Cross Margin Position

        /// <inheritdoc />
        public async Task<WebCallResult<HTXClosePositionResult>> CloseCrossMarginPositionAsync(string contractCode, string pair, ContractType? contractType, OrderSide direction, long? clientOrderId = null, LightningPriceType? orderPriceType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.AddEnum("direction", direction);
            parameters.AddEnum("contractType", contractType);
            parameters.AddOptional("client_order_id", clientOrderId);
            parameters.AddOptionalEnum("order_price_type", orderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_lightning_close_position", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXClosePositionResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
