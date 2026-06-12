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
        public async Task<HttpResult<HTXOrderIdV5>> PlaceOrderAsync(string contractCode, MarginMode marginMode, OrderSide side, OrderTypeV5 type, decimal quantity, FuturesPositionSide? positionSide = null, decimal? price = null, PriceMatch? priceMatch = null, string? clientOrderId = null, bool? reduceOnly = null, TimeInForce? timeInForce = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderTypeV5? takeProfitType = null, TriggerPriceType? takeProfitTriggerPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderTypeV5? stopLossType = null, TriggerPriceType? stopLossTriggerPriceType = null, bool? priceProtect = null, SelfMatchPrevent? selfMatchPrevent = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "margin_mode", EnumConverter.GetString(marginMode) },
                { "side", EnumConverter.GetString(side) },
                { "type", EnumConverter.GetString(type) },
                { "volume", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.Add("position_side", positionSide);
            parameters.Add("price_match", priceMatch);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("price", price);
            parameters.Add("reduce_only", reduceOnly == null ? null : reduceOnly.Value ? 1 : 0);
            parameters.Add("time_in_force", timeInForce);
            parameters.Add("tp_trigger_price", takeProfitTriggerPrice);
            parameters.Add("tp_order_price", takeProfitOrderPrice);
            parameters.Add("tp_type", takeProfitType);
            parameters.Add("tp_trigger_price_type", takeProfitTriggerPriceType);
            parameters.Add("sl_trigger_price", stopLossTriggerPrice);
            parameters.Add("sl_order_price", stopLossOrderPrice);
            parameters.Add("sl_type", stopLossType);
            parameters.Add("sl_trigger_price_type", stopLossTriggerPriceType);
            parameters.Add("price_protect", priceProtect);
            parameters.Add("self_match_prevent", selfMatchPrevent);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v5/trade/order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXOrderIdV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderIdV5>> CancelOrderAsync(string contractCode, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v5/trade/cancel_order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXOrderIdV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderV5>> GetOrderAsync(string contractCode, MarginMode? marginMode = null, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("margin_mode", marginMode);
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/trade/order", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXOrderV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderV5[]>> GetOpenOrdersAsync(string? contractCode = null, MarginMode? marginMode = null, string? orderId = null, string? clientOrderId = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("margin_mode", marginMode);
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("from", fromId);
            parameters.Add("limit", limit);
            parameters.Add("direct", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/trade/order/opens", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXOrderV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Details

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderTradeV5[]>> GetOrderDetailsAsync(string contractCode, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("order_id", orderId);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("from", fromId);
            parameters.Add("limit", limit);
            parameters.Add("direct", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/trade/order/details", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXOrderTradeV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderV5[]>> GetOrderHistoryAsync(string contractCode, MarginMode marginMode, IEnumerable<OrderStatusV5>? states = null, OrderTypeV5? type = null, PriceMatch? priceMatch = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
            };
            parameters.Add("margin_mode", marginMode);

            parameters.Add("state", states == null ? null : string.Join(",", states.Select(EnumConverter.GetString)));
            parameters.Add("type", type);
            parameters.Add("price_match", priceMatch);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("from", fromId);
            parameters.Add("limit", limit);
            parameters.Add("direct", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/trade/order/history", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXOrderV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Algo Order

        /// <inheritdoc />
        public async Task<HttpResult<HTXAlgoOrderIdV5>> PlaceAlgoOrderAsync(string contractCode, AlgoOrderType type, FuturesPositionSide positionSide, OrderSide side, MarginMode marginMode, string? algoClientOrderId = null, decimal? quantity = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderTypeV5? takeProfitType = null, TriggerPriceType? takeProfitTriggerPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderTypeV5? stopLossType = null, TriggerPriceType? stopLossTriggerPriceType = null, decimal? price = null, PriceMatch? priceType = null, decimal? triggerPrice = null, TriggerPriceType? triggerPriceType = null, decimal? activationPrice = null, PriceMatch? orderPriceType = null, decimal? callbackRate = null, bool? reduceOnly = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
            };
            parameters.Add("algo_type", type);
            parameters.Add("position_side", positionSide);
            parameters.Add("side", side);
            parameters.Add("margin_mode", marginMode);

            parameters.Add("algo_client_order_id", algoClientOrderId);
            parameters.Add("volume", quantity);
            parameters.Add("tp_trigger_price", takeProfitTriggerPrice);
            parameters.Add("tp_order_price", takeProfitOrderPrice);
            parameters.Add("tp_type", takeProfitType);
            parameters.Add("tp_trigger_price_type", takeProfitTriggerPriceType);
            parameters.Add("sl_trigger_price", stopLossTriggerPrice);
            parameters.Add("sl_order_price", stopLossOrderPrice);
            parameters.Add("sl_type", stopLossType);
            parameters.Add("sl_trigger_price_type", stopLossTriggerPriceType);
            parameters.Add("price", price);
            parameters.Add("price_type", priceType);
            parameters.Add("trigger_price", triggerPrice);
            parameters.Add("trigger_price_type", triggerPriceType);
            parameters.Add("active_price", activationPrice);
            parameters.Add("order_price_type", orderPriceType);
            parameters.Add("callback_rate", callbackRate);
            parameters.Add("reduce_only", reduceOnly == null ? null : reduceOnly.Value ? 1 : 0);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v5/algo/order", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXAlgoOrderIdV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Algo Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXAlgoOrderV5[]>> GetOpenAlgoOrdersAsync(AlgoOrderType type, string? contractCode = null, string? algoId = null, string? algoClientOrderId = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("algo_type", type);

            parameters.Add("contract_code", contractCode);
            parameters.Add("algo_id", algoId);
            parameters.Add("algo_client_order_id", algoClientOrderId);
            parameters.Add("from", fromId);
            parameters.Add("limit", limit);
            parameters.Add("direct", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/algo/order/opens", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXAlgoOrderV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Algo Order History

        /// <inheritdoc />
        public async Task<HttpResult<HTXAlgoOrderV5[]>> GetAlgoOrderHistoryAsync(AlgoOrderType type, string? contractCode = null, MarginMode? marginMode = null, IEnumerable<AlgoOrderStatus>? states = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("algo_type", type);

            parameters.Add("contract_code", contractCode);
            parameters.Add("margin_mode", marginMode);
            parameters.Add("state", states == null ? null : string.Join(",", states.Select(EnumConverter.GetString)));
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("from", fromId);
            parameters.Add("limit", limit);
            parameters.Add("direct", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/algo/order/history", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXAlgoOrderV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Algo Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXAlgoOrderIdV5>> CancelAlgoOrdersAsync(string contractCode, string? algoId = null, string? algoClientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("algo_id", algoId);
            parameters.Add("algo_client_order_id", algoClientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v5/algo/cancel_orders", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXAlgoOrderIdV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
