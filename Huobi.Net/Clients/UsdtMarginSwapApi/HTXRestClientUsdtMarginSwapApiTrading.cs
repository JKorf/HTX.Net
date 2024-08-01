using HTX.Net.Clients.FuturesApi;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtMarginSwapApi;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using System.Drawing;

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
            parameters.AddOptionalEnumAsInt("trade_type", tradeType);
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
            parameters.AddOptionalEnumAsInt("trade_type", tradeType);
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

        #region Get Isolated Margin User Trades

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
        public async Task<WebCallResult<HTXClosePositionResult>> CloseCrossMarginPositionAsync(OrderSide direction, string? contractCode = null, string? pair = null, ContractType? contractType = null, long? clientOrderId = null, LightningPriceType? orderPriceType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddEnum("direction", direction);
            parameters.AddOptionalEnum("contractType", contractType);
            parameters.AddOptional("client_order_id", clientOrderId);
            parameters.AddOptionalEnum("order_price_type", orderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_lightning_close_position", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXClosePositionResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Isolated Margin Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderId>> PlaceIsolatedMarginTriggerOrderAsync(string contractCode, TriggerType triggerType, decimal triggerPrice, decimal quantity, OrderSide side, Offset? offset = null, bool? reduceOnly = null, decimal? orderPrice = null, OrderPriceType? orderPriceType = null, int? leverageRate = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddEnum("trigger_type", triggerType);
            parameters.Add("trigger_price", triggerPrice);
            parameters.Add("volume", quantity);
            parameters.AddEnum("direction", side);
            parameters.AddOptionalEnum("offset", offset);
            parameters.AddOptional("reduce_only", reduceOnly);
            parameters.AddOptional("order_price", orderPrice);
            parameters.AddOptionalEnum("order_price_type", orderPriceType);
            parameters.AddOptional("lever_rate", leverageRate);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_trigger_order", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Cross Margin Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderId>> PlaceCrossMarginTriggerOrderAsync(TriggerType triggerType, decimal triggerPrice, decimal quantity, OrderSide side, string? contractCode = null, string? pair = null, ContractType? contractType = null, Offset? offset = null, bool? reduceOnly = null, decimal? orderPrice = null, OrderPriceType? orderPriceType = null, int? leverageRate = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddEnum("trigger_type", triggerType);
            parameters.Add("trigger_price", triggerPrice);
            parameters.Add("volume", quantity);
            parameters.AddEnum("direction", side);
            parameters.AddOptionalEnum("contract_type", contractType);
            parameters.AddOptionalEnum("offset", offset);
            parameters.AddOptional("pair", pair);
            parameters.AddOptional("reduce_only", reduceOnly);
            parameters.AddOptional("order_price", orderPrice);
            parameters.AddOptionalEnum("order_price_type", orderPriceType);
            parameters.AddOptional("lever_rate", leverageRate);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_trigger_order", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Isolated Margin Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelIsolatedMarginTriggerOrderAsync(string contractCode, string orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_trigger_cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Cross Margin Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelCrossMarginTriggerOrderAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            parameters.AddOptional("pair", pair);
            parameters.AddOptionalEnum("contract_type", contractType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_trigger_cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Isolated Margin Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTriggerOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddOptionalEnum("direction", side);
            parameters.AddOptionalEnum("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_trigger_cancelall", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Isolated Margin Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelAllCrossMarginTriggerOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddOptionalEnum("contract_type", contractType);
            parameters.AddOptionalEnum("direction", side);
            parameters.AddOptionalEnum("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_trigger_cancelall ", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Open Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderPage>> GetIsolatedMarginOpenTriggerOrdersAsync(string contractCode, int? page = null, int? pageSize = null, Enums.MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            parameters.AddOptionalEnumAsInt("trade_type", tradeType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_trigger_openorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Open Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossTriggerOrderPage>> GetCrossMarginOpenTriggerOrdersAsync(string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, Enums.MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            parameters.AddOptionalEnumAsInt("trade_type", tradeType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_trigger_openorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossTriggerOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Trigger Order History

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderPage>> GetIsolatedMarginTriggerOrderHistoryAsync(string contractCode, MarginTradeType tradeType, int daysPast, OrderStatusFilter? status = null, int? page = null, int? pageIndex = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddEnumAsInt("trade_type", tradeType);
            parameters.Add("create_date", daysPast);
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageIndex);
            parameters.AddOptional("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_trigger_hisorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTriggerOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Trigger Order History

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossTriggerOrderPage>> GetCrossMarginTriggerOrderHistoryAsync(MarginTradeType tradeType, int daysPast, string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderStatusFilter? status = null, int? page = null, int? pageIndex = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddOptionalEnum("contract_type", contractType);
            parameters.AddEnumAsInt("trade_type", tradeType);
            parameters.Add("create_date", daysPast);
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageIndex);
            parameters.AddOptional("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_trigger_hisorders ", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossTriggerOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Isolated Margin Tp Sl

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTpSlResult>> SetIsolatedMarginTpSlAsync(string contractCode, OrderSide side, decimal quantity, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddEnum("direction", side);
            parameters.Add("volume", quantity);
            parameters.AddOptional("tp_trigger_price", takeProfitTriggerPrice);
            parameters.AddOptional("tp_order_price", takeProfitOrderPrice);
            parameters.AddOptionalEnum("tp_order_price_type", takeProfitOrderPriceType);
            parameters.AddOptional("sl_trigger_price", stopLossTriggerPrice);
            parameters.AddOptional("sl_order_price", stopLossOrderPrice);
            parameters.AddOptionalEnum("sl_order_price_type", stopLossOrderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_tpsl_order", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTpSlResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Cross Margin Tp Sl

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTpSlResult>> SetCrossMarginTpSlAsync(OrderSide side, decimal quantity, string? contractCode = null, string? pair = null, ContractType? contractType = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddOptionalEnum("contract_type", contractType);
            parameters.AddEnum("direction", side);
            parameters.Add("volume", quantity);
            parameters.AddOptional("tp_trigger_price", takeProfitTriggerPrice);
            parameters.AddOptional("tp_order_price", takeProfitOrderPrice);
            parameters.AddOptionalEnum("tp_order_price_type", takeProfitOrderPriceType);
            parameters.AddOptional("sl_trigger_price", stopLossTriggerPrice);
            parameters.AddOptional("sl_order_price", stopLossOrderPrice);
            parameters.AddOptionalEnum("sl_order_price_type", stopLossOrderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_tpsl_order", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTpSlResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Isolated Margin Tp Sl

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelIsolatedMarginTpSlAsync(string contractCode, string orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_tpsl_cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Cross Margin Tp Sl

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelCrossMarginTpSlAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null,  CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddOptionalEnum("contract_type", contractType);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_tpsl_cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Isolated Margin Tp Sl

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTpSlAsync(string contractCode, OrderSide? side = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddOptionalEnum("direction", side);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_tpsl_cancelall", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Cross Margin Tp Sl

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelAllCrossMarginTpSlAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("pair", pair);
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptionalEnum("contract_type", contractType);
            parameters.AddOptionalEnum("direction", side);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_tpsl_cancelall ", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Open Tp Sl Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTpSlOrderPage>> GetIsolatedMarginOpenTpSlOrdersAsync(string? contractCode = null, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            parameters.AddOptionalEnumAsInt("trade_type", tradeType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_tpsl_openorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTpSlOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Open Tp Sl Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossTpSlOrderPage>> GetCrossMarginOpenTpSlOrdersAsync(string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            parameters.AddOptionalEnumAsInt("trade_type", tradeType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_tpsl_openorders ", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXCrossTpSlOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Tp Sl History

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTpSlClosedOrderPage>> GetIsolatedMarginTpSlHistoryAsync(string contractCode, IEnumerable<TpSlStatus> tpSlOrderStatus, int daysPast, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.Add("status", string.Join(",", tpSlOrderStatus.Select(EnumConverter.GetString)));
            parameters.Add("create_date", daysPast);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            parameters.AddOptional("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_tpsl_hisorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTpSlClosedOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Tp Sl History

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossTpSlClosedOrderPage>> GetCrossMarginTpSlHistoryAsync(IEnumerable<TpSlStatus> tpSlOrderStatus, int daysPast, string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.Add("status", string.Join(",", tpSlOrderStatus.Select(EnumConverter.GetString)));
            parameters.Add("create_date", daysPast);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            parameters.AddOptional("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_tpsl_hisorders ", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXCrossTpSlClosedOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Position Open Tp Sl Info

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPositionOpenTpSlOrders>> GetIsolatedMarginPositionOpenTpSlInfoAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_relation_tpsl_order", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXPositionOpenTpSlOrders>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Position Open Tp Sl Info

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossPositionOpenTpSlOrders>> GetCrossMarginPositionOpenTpSlInfoAsync(long orderId, string? contractCode = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("pair", pair);
            parameters.AddOptional("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_relation_tpsl_order", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXCrossPositionOpenTpSlOrders>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Isolated Margin Trailing Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderId>> PlaceIsolatedMarginTrailingOrderAsync(string contractCode, bool reduceOnly, OrderSide side, Offset offset, int leverageRate, decimal quantity, decimal callbackRate, decimal activePrice, OrderPriceType orderPriceType, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.Add("reduce_only", reduceOnly ? 1 : 0);
            parameters.AddEnum("direction", side);
            parameters.AddEnum("offset", offset);
            parameters.Add("lever_rate", leverageRate);
            parameters.Add("volume", quantity);
            parameters.Add("callback_rate", callbackRate);
            parameters.Add("active_price", activePrice);
            parameters.AddEnum("order_price_type", orderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_track_order", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Cross Margin Trailing Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderId>> PlaceCrossMarginTrailingOrderAsync(OrderSide side, Offset offset, int leverageRate, decimal quantity, decimal callbackRate, decimal activePrice, OrderPriceType orderPriceType, string? contractCode = null, string? pair = null, ContractType? contractType = null, bool? reduceOnly = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddOptionalEnum("contract_type", contractType);
            if (reduceOnly.HasValue)
                parameters.Add("reduce_only", reduceOnly.Value ? 1 : 0);
            parameters.AddEnum("direction", side);
            parameters.AddEnum("offset", offset);
            parameters.Add("lever_rate", leverageRate);
            parameters.Add("volume", quantity);
            parameters.Add("callback_rate", callbackRate);
            parameters.Add("active_price", activePrice);
            parameters.AddEnum("order_price_type", orderPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_track_order", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Isolated Margin Trailing Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelIsolatedMarginTrailingOrderAsync(string contractCode, string orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_track_cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Cross Margin Trailing Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelCrossMarginTrailingOrderAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddOptionalEnum("contract_type", contractType);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_track_cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Isolated Margin Trailing Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTrailingOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddOptionalEnum("direction", side);
            parameters.AddOptionalEnum("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_track_cancelall", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Cross Margin Trailing Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTriggerOrderResult>> CancelAllCrossMarginTrailingOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddOptionalEnum("contract_type", contractType);
            parameters.AddOptionalEnum("direction", side);
            parameters.AddOptionalEnum("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_track_cancelall ", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTriggerOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Isolated Margin Trailing Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTrailingOrderPage>> GetOpenIsolatedMarginTrailingOrdersAsync(string contractCode, MarginTradeType? tradeType = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddOptionalEnum("trade_type", tradeType);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_track_openorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTrailingOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Cross Margin Trailing Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossTrailingOrderPage>> GetOpenCrossMarginTrailingOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, MarginTradeType? tradeType = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptionalEnum("contract_type", contractType);
            parameters.AddOptional("pair", pair);
            parameters.AddOptionalEnum("trade_type", tradeType);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_track_openorders ", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXCrossTrailingOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Isolated Margin Trailing Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTrailingClosedOrderPage>> GetClosedIsolatedMarginTrailingOrdersAsync(string contractCode, IEnumerable<TpSlStatus> tpSlOrderStatus, MarginTradeType tradeType, int daysPast, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.Add("status", string.Join(",", tpSlOrderStatus.Select(EnumConverter.GetString)));
            parameters.AddEnumAsInt("trade_type", tradeType);
            parameters.Add("create_date", daysPast);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            parameters.AddOptional("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_track_hisorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXTrailingClosedOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Cross Margin Trailing Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossTrailingClosedOrderPage>> GetClosedCrossMarginTrailingOrdersAsync(IEnumerable<TpSlStatus> tpSlOrderStatus, MarginTradeType tradeType, int daysPast, string? contractCode = null, string? pair = null, ContractType? contractType = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddOptionalEnum("contract_type", contractType);
            parameters.Add("status", string.Join(",", tpSlOrderStatus.Select(EnumConverter.GetString)));
            parameters.AddEnumAsInt("trade_type", tradeType);
            parameters.Add("create_date", daysPast);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            parameters.AddOptional("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_track_hisorders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXCrossTrailingClosedOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
