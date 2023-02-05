using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Huobi.Net.Clients.FuturesApi;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginSwapApiTrading : IHuobiClientUsdtMarginSwapApiTrading
    {
        private readonly HuobiClientUsdtMarginSwapApi _baseClient;

        internal HuobiClientUsdtMarginSwapApiTrading(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiPlacedOrderId>> PlaceIsolatedMarginOrderAsync(
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
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "volume", quantity.ToString(CultureInfo.InvariantCulture) },
                { "direction", EnumConverter.GetString(side) },
                { "lever_rate", leverageRate }
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

            return await _baseClient.SendHuobiRequest<HuobiPlacedOrderId>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiPlacedOrderId>> PlaceCrossMarginOrderAsync(
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
            var parameters = new Dictionary<string, object>()
            {
                { "volume", quantity.ToString(CultureInfo.InvariantCulture) },
                { "direction", EnumConverter.GetString(side) },
                { "lever_rate", leverageRate }
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

            return await _baseClient.SendHuobiRequest<HuobiPlacedOrderId>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderId, IEnumerable<long> clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "order_id", string.Join(",", orderId) },
                { "client_order_id", string.Join(",", clientOrderId) }
            };
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelCrossMarginOrderAsync(long? orderId = null, long? clientOrderId = null, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelCrossMarginOrdersAsync(IEnumerable<long> orderId, IEnumerable<long> clientOrderId, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "order_id", string.Join(",", orderId) },
                { "client_order_id", string.Join(",", clientOrderId) }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelAllIsolatedMarginOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("direction", EnumConverter.GetString(side));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cancelall"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelAllCrossMarginOrdersAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("direction", EnumConverter.GetString(side));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_cancelall"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiIsolatedMarginLeverageRate>> ChangeIsolatedMarginLeverageAsync(string contractCode, int leverageRate, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "lever_rate", leverageRate },
            };
            return await _baseClient.SendHuobiRequest<HuobiIsolatedMarginLeverageRate>(_baseClient.GetUrl("/linear-swap-api/v1/swap_switch_lever_rate"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiCrossMarginLeverageRate>> ChangeCrossMarginLeverageAsync(int leverageRate, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "lever_rate", leverageRate },
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));

            return await _baseClient.SendHuobiRequest<HuobiCrossMarginLeverageRate>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_switch_lever_rate"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginOrder>>> GetIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginOrder>>> GetIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", string.Join(",", orderIds));
            parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIds));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiCrossMarginOrder>>> GetCrossMarginOrderAsync(string? contractCode = null, string? symbol = null, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiCrossMarginOrder>>> GetCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("order_id", string.Join(",", orderIds));
            parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIds));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiMarginOrderDetails>> GetIsolatedMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "order_id", orderId }
            };
            return await _baseClient.SendHuobiRequest<HuobiMarginOrderDetails>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order_detail"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiMarginOrderDetails>> GetCrossMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "order_id", orderId }
            };
            return await _baseClient.SendHuobiRequest<HuobiMarginOrderDetails>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order_detail"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiIsolatedMarginOrderPage>> GetIsolatedMarginOpenOrdersAsync(string contractCode, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            parameters.AddOptionalParameter("trade_type", EnumConverter.GetString(tradeType));
            return await _baseClient.SendHuobiRequest<HuobiIsolatedMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_openorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiCrossMarginOrderPage>> GetCrossMarginOpenOrdersAsync(string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            parameters.AddOptionalParameter("trade_type", EnumConverter.GetString(tradeType));
            return await _baseClient.SendHuobiRequest<HuobiCrossMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_openorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiIsolatedMarginOrderPage>> GetIsolatedMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, bool allOrders, int daysInHistory, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
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
            return await _baseClient.SendHuobiRequest<HuobiIsolatedMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_hisorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiCrossMarginOrderPage>> GetCrossMarginClosedOrdersAsync(MarginTradeType tradeType, bool allOrders, int daysInHistory, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
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
            return await _baseClient.SendHuobiRequest<HuobiCrossMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_hisorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiIsolatedMarginUserTradePage>> GetIsolatedMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiIsolatedMarginUserTradePage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_matchresults"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiCrossMarginUserTradePage>> GetCrossMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiCrossMarginUserTradePage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_matchresults"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
    }
}
