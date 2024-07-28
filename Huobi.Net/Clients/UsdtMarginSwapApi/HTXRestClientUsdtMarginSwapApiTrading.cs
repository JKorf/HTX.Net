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

            return await _baseClient.SendHTXRequest<HTXPlacedOrderId>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

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

            return await _baseClient.SendHTXRequest<HTXPlacedOrderId>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchResult>> CancelIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHTXRequest<HTXBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchResult>> CancelIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderId, IEnumerable<long> clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "order_id", string.Join(",", orderId) },
                { "client_order_id", string.Join(",", clientOrderId) }
            };
            return await _baseClient.SendHTXRequest<HTXBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchResult>> CancelCrossMarginOrderAsync(long? orderId = null, long? clientOrderId = null, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHTXRequest<HTXBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

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
            return await _baseClient.SendHTXRequest<HTXBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchResult>> CancelAllIsolatedMarginOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("direction", EnumConverter.GetString(side));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            return await _baseClient.SendHTXRequest<HTXBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cancelall"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBatchResult>> CancelAllCrossMarginOrdersAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("direction", EnumConverter.GetString(side));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            return await _baseClient.SendHTXRequest<HTXBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_cancelall"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXIsolatedMarginLeverageRate>> ChangeIsolatedMarginLeverageAsync(string contractCode, int leverageRate, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "lever_rate", leverageRate },
            };
            return await _baseClient.SendHTXRequest<HTXIsolatedMarginLeverageRate>(_baseClient.GetUrl("/linear-swap-api/v1/swap_switch_lever_rate"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginLeverageRate>> ChangeCrossMarginLeverageAsync(int leverageRate, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "lever_rate", leverageRate },
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));

            return await _baseClient.SendHTXRequest<HTXCrossMarginLeverageRate>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_switch_lever_rate"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginOrder>>> GetIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXIsolatedMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginOrder>>> GetIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", string.Join(",", orderIds));
            parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIds));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXIsolatedMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossMarginOrder>>> GetCrossMarginOrderAsync(string? contractCode = null, string? symbol = null, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXCrossMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossMarginOrder>>> GetCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("order_id", string.Join(",", orderIds));
            parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIds));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXCrossMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMarginOrderDetails>> GetIsolatedMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "order_id", orderId }
            };
            return await _baseClient.SendHTXRequest<HTXMarginOrderDetails>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order_detail"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMarginOrderDetails>> GetCrossMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "order_id", orderId }
            };
            return await _baseClient.SendHTXRequest<HTXMarginOrderDetails>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order_detail"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

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
            return await _baseClient.SendHTXRequest<HTXIsolatedMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_openorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginOrderPage>> GetCrossMarginOpenOrdersAsync(string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            parameters.AddOptionalParameter("trade_type", EnumConverter.GetString(tradeType));
            return await _baseClient.SendHTXRequest<HTXCrossMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_openorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXIsolatedMarginOrderPage>> GetIsolatedMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, bool allOrders, int daysInHistory, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "type", allOrders ? "1": "2" },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            return await _baseClient.SendHTXRequest<HTXIsolatedMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_hisorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginOrderPage>> GetCrossMarginClosedOrdersAsync(MarginTradeType tradeType, bool allOrders, int daysInHistory, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "type", allOrders ? "1": "2" },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            return await _baseClient.SendHTXRequest<HTXCrossMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_hisorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXIsolatedMarginUserTradePage>> GetIsolatedMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHTXRequest<HTXIsolatedMarginUserTradePage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_matchresults"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginUserTradePage>> GetCrossMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHTXRequest<HTXCrossMarginUserTradePage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_matchresults"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
    }
}
