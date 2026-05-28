using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;
using HTX.Net.Objects.Models.UsdtFuturesV5;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    /// <inheritdoc />
    internal class HTXRestClientUsdtFuturesV5ApiTrading : IHTXRestClientUsdtFuturesV5ApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientUsdtFuturesV5Api _baseClient;

        internal HTXRestClientUsdtFuturesV5ApiTrading(HTXRestClientUsdtFuturesV5Api baseClient)
        {
            _baseClient = baseClient;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderIdV5>> PlaceOrderAsync(string contractCode, MarginMode marginMode, OrderSide side, OrderTypeV5 type, decimal quantity, FuturesPositionSide? positionSide = null, decimal? price = null, PriceMatch? priceMatch = null, string? clientOrderId = null, bool? reduceOnly = null, TimeInForce? timeInForce = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderTypeV5? takeProfitType = null, TriggerPriceType? takeProfitTriggerPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderTypeV5? stopLossType = null, TriggerPriceType? stopLossTriggerPriceType = null, bool? priceProtect = null, SelfMatchPrevent? selfMatchPrevent = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "contract_code", contractCode },
                { "margin_mode", EnumConverter.GetString(marginMode) },
                { "side", EnumConverter.GetString(side) },
                { "type", EnumConverter.GetString(type) },
                { "volume", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("position_side", EnumConverter.GetString(positionSide));
            parameters.AddOptionalParameter("price_match", EnumConverter.GetString(priceMatch));
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduce_only", reduceOnly == null ? null : reduceOnly.Value ? 1 : 0);
            parameters.AddOptionalParameter("time_in_force", EnumConverter.GetString(timeInForce));
            parameters.AddOptionalParameter("tp_trigger_price", takeProfitTriggerPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("tp_order_price", takeProfitOrderPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("tp_type", EnumConverter.GetString(takeProfitType));
            parameters.AddOptionalParameter("tp_trigger_price_type", EnumConverter.GetString(takeProfitTriggerPriceType));
            parameters.AddOptionalParameter("sl_trigger_price", stopLossTriggerPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("sl_order_price", stopLossOrderPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("sl_type", EnumConverter.GetString(stopLossType));
            parameters.AddOptionalParameter("sl_trigger_price_type", EnumConverter.GetString(stopLossTriggerPriceType));
            parameters.AddOptionalParameter("price_protect", priceProtect);
            parameters.AddOptionalParameter("self_match_prevent", EnumConverter.GetString(selfMatchPrevent));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v5/trade/order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXOrderIdV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderIdV5>> CancelOrderAsync(string contractCode, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v5/trade/cancel_order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXOrderIdV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderV5>> GetOrderAsync(string contractCode, MarginMode? marginMode = null, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("margin_mode", EnumConverter.GetString(marginMode));
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/trade/order", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXOrderV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderV5[]>> GetOpenOrdersAsync(string? contractCode = null, MarginMode? marginMode = null, string? orderId = null, string? clientOrderId = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("margin_mode", EnumConverter.GetString(marginMode));
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("direct", EnumConverter.GetString(direction));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/trade/order/opens", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXOrderV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Details

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderTradeV5[]>> GetOrderDetailsAsync(string contractCode, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalMillisecondsString("start_time", startTime);
            parameters.AddOptionalMillisecondsString("end_time", endTime);
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("direct", EnumConverter.GetString(direction));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/trade/order/details", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXOrderTradeV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderV5[]>> GetOrderHistoryAsync(string contractCode, MarginMode marginMode, IEnumerable<OrderStatusV5>? states = null, OrderTypeV5? type = null, PriceMatch? priceMatch = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "contract_code", contractCode },
                { "margin_mode", EnumConverter.GetString(marginMode) }
            };
            parameters.AddOptionalParameter("state", states == null ? null : string.Join(",", states.Select(EnumConverter.GetString)));
            parameters.AddOptionalParameter("type", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("price_match", EnumConverter.GetString(priceMatch));
            parameters.AddOptionalMillisecondsString("start_time", startTime);
            parameters.AddOptionalMillisecondsString("end_time", endTime);
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("direct", EnumConverter.GetString(direction));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/trade/order/history", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXOrderV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Algo Order

        /// <inheritdoc />
        public async Task<WebCallResult<HTXAlgoOrderIdV5>> PlaceAlgoOrderAsync(string contractCode, AlgoOrderType type, FuturesPositionSide positionSide, OrderSide side, MarginMode marginMode, string? algoClientOrderId = null, decimal? quantity = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderTypeV5? takeProfitType = null, TriggerPriceType? takeProfitTriggerPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderTypeV5? stopLossType = null, TriggerPriceType? stopLossTriggerPriceType = null, decimal? price = null, PriceMatch? priceType = null, decimal? triggerPrice = null, TriggerPriceType? triggerPriceType = null, decimal? activationPrice = null, PriceMatch? orderPriceType = null, decimal? callbackRate = null, bool? reduceOnly = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "contract_code", contractCode },
                { "algo_type", EnumConverter.GetString(type) },
                { "position_side", EnumConverter.GetString(positionSide) },
                { "side", EnumConverter.GetString(side) },
                { "margin_mode", EnumConverter.GetString(marginMode) }
            };
            parameters.AddOptionalParameter("algo_client_order_id", algoClientOrderId);
            parameters.AddOptionalParameter("volume", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("tp_trigger_price", takeProfitTriggerPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("tp_order_price", takeProfitOrderPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("tp_type", EnumConverter.GetString(takeProfitType));
            parameters.AddOptionalParameter("tp_trigger_price_type", EnumConverter.GetString(takeProfitTriggerPriceType));
            parameters.AddOptionalParameter("sl_trigger_price", stopLossTriggerPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("sl_order_price", stopLossOrderPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("sl_type", EnumConverter.GetString(stopLossType));
            parameters.AddOptionalParameter("sl_trigger_price_type", EnumConverter.GetString(stopLossTriggerPriceType));
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("price_type", EnumConverter.GetString(priceType));
            parameters.AddOptionalParameter("trigger_price", triggerPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("trigger_price_type", EnumConverter.GetString(triggerPriceType));
            parameters.AddOptionalParameter("active_price", activationPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("order_price_type", EnumConverter.GetString(orderPriceType));
            parameters.AddOptionalParameter("callback_rate", callbackRate?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduce_only", reduceOnly == null ? null : reduceOnly.Value ? 1 : 0);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v5/algo/order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXAlgoOrderIdV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Algo Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXAlgoOrderV5[]>> GetOpenAlgoOrdersAsync(AlgoOrderType type, string? contractCode = null, string? algoId = null, string? algoClientOrderId = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "algo_type", EnumConverter.GetString(type) }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("algo_id", algoId);
            parameters.AddOptionalParameter("algo_client_order_id", algoClientOrderId);
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("direct", EnumConverter.GetString(direction));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/algo/order/opens", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXAlgoOrderV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Algo Order History

        /// <inheritdoc />
        public async Task<WebCallResult<HTXAlgoOrderV5[]>> GetAlgoOrderHistoryAsync(AlgoOrderType type, string? contractCode = null, MarginMode? marginMode = null, IEnumerable<AlgoOrderStatus>? states = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "algo_type", EnumConverter.GetString(type) }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("margin_mode", EnumConverter.GetString(marginMode));
            parameters.AddOptionalParameter("state", states == null ? null : string.Join(",", states.Select(EnumConverter.GetString)));
            parameters.AddOptionalMillisecondsString("start_time", startTime);
            parameters.AddOptionalMillisecondsString("end_time", endTime);
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("direct", EnumConverter.GetString(direction));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/algo/order/history", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXAlgoOrderV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Algo Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXAlgoOrderIdV5>> CancelAlgoOrdersAsync(string contractCode, string? algoId = null, string? algoClientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("algo_id", algoId);
            parameters.AddOptionalParameter("algo_client_order_id", algoClientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v5/algo/cancel_orders", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXAlgoOrderIdV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
