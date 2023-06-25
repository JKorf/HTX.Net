using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Net.Objects.Models;
using CryptoExchange.Net.Converters;
using Huobi.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.CommonObjects;

namespace Huobi.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class HuobiRestClientSpotApiTrading : IHuobiClientSpotApiTrading
    {
        private const string PlaceOrderEndpoint = "order/orders/place";
        private const string OpenOrdersEndpoint = "order/openOrders";
        private const string OrdersEndpoint = "order/orders";
        private const string CancelOrderEndpoint = "order/orders/{}/submitcancel";
        private const string CancelOrderByClientOrderIdEndpoint = "order/orders/submitCancelClientOrder";
        private const string CancelOrdersByCriteriaEndpoint = "order/orders/batchCancelOpenOrders";
        private const string CancelOrdersEndpoint = "order/orders/batchcancel";
        private const string OrderInfoEndpoint = "order/orders/{}";
        private const string ClientOrderInfoEndpoint = "order/orders/getClientOrder";
        private const string OrderTradesEndpoint = "order/orders/{}/matchresults";
        private const string SymbolTradesEndpoint = "order/matchresults";
        private const string HistoryOrdersEndpoint = "order/history";

        private readonly HuobiRestClientSpotApi _baseClient;

        internal HuobiRestClientSpotApiTrading(HuobiRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PlaceOrderAsync(long accountId, string symbol, Enums.OrderSide side, Enums.OrderType type, decimal quantity, decimal? price = null, string? clientOrderId = null, SourceType? source = null, decimal? stopPrice = null, Operator? stopOperator = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            if (type == Enums.OrderType.StopLimit)
                throw new ArgumentException("Stop limit orders not supported by API");

            var orderType = JsonConvert.SerializeObject(side, new OrderSideConverter(false)) + "-" + JsonConvert.SerializeObject(type, new OrderTypeConverter(false));

            var parameters = new Dictionary<string, object>
            {
                { "account-id", accountId },
                { "amount", quantity },
                { "symbol", symbol },
                { "type", orderType }
            };

            parameters.AddOptionalParameter("client-order-id", clientOrderId);
            parameters.AddOptionalParameter("source", source == null ? null : JsonConvert.SerializeObject(source, new SourceTypeConverter(false)));
            parameters.AddOptionalParameter("stop-price", stopPrice);
            parameters.AddOptionalParameter("operator", stopOperator == null ? null : JsonConvert.SerializeObject(stopOperator, new OperatorConverter(false)));

            // If precision of the symbol = 1 (eg has to use whole amounts, 1,2,3 etc) Huobi doesn't except the .0 postfix (1.0) for amount
            // Issue at the Huobi side
            if (quantity % 1 == 0)
                parameters["amount"] = quantity.ToString(CultureInfo.InvariantCulture);

            parameters.AddOptionalParameter("price", price);
            var result = await _baseClient.SendHuobiRequest<long>(_baseClient.GetUrl(PlaceOrderEndpoint, "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderPlaced(new OrderId { SourceObject = result.Data, Id = result.Data.ToString(CultureInfo.InvariantCulture) });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiOpenOrder>>> GetOpenOrdersAsync(long? accountId = null, string? symbol = null, Enums.OrderSide? side = null, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol?.ValidateHuobiSymbol();

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("account-id", accountId);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("size", limit);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiOpenOrder>>(_baseClient.GetUrl(OpenOrdersEndpoint, "1"), HttpMethod.Get, ct, parameters, true, weight: 2).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> CancelOrderAsync(long orderId, CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiRequest<long>(_baseClient.GetUrl(CancelOrderEndpoint.FillPathParameters(orderId.ToString(CultureInfo.InvariantCulture)), "1"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { SourceObject = result.Data, Id = result.Data.ToString(CultureInfo.InvariantCulture) });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "client-order-id", clientOrderId }
            };

            return await _baseClient.SendHuobiRequest<long>(_baseClient.GetUrl(CancelOrderByClientOrderIdEndpoint, "1"), HttpMethod.Post, ct, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchCancelResult>> CancelOrdersAsync(IEnumerable<long>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default)
        {
            if (orderIds == null && clientOrderIds == null)
                throw new ArgumentException("Either orderIds or clientOrderIds should be provided");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("order-ids", orderIds?.Select(s => s.ToString(CultureInfo.InvariantCulture)));
            parameters.AddOptionalParameter("client-order-ids", clientOrderIds?.Select(s => s.ToString(CultureInfo.InvariantCulture)));

            return await _baseClient.SendHuobiRequest<HuobiBatchCancelResult>(_baseClient.GetUrl(CancelOrdersEndpoint, "1"), HttpMethod.Post, ct, parameters, true, weight: 2).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiByCriteriaCancelResult>> CancelOrdersByCriteriaAsync(long? accountId = null, IEnumerable<string>? symbols = null, Enums.OrderSide? side = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("account-id", accountId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbols == null ? null : string.Join(",", symbols));
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("size", limit);

            return await _baseClient.SendHuobiRequest<HuobiByCriteriaCancelResult>(_baseClient.GetUrl(CancelOrdersByCriteriaEndpoint, "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<HuobiOrder>(_baseClient.GetUrl(OrderInfoEndpoint.FillPathParameters(orderId.ToString(CultureInfo.InvariantCulture)), "1"), HttpMethod.Get, ct, signed: true, weight: 2).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "clientOrderId", clientOrderId }
            };

            return await _baseClient.SendHuobiRequest<HuobiOrder>(_baseClient.GetUrl(ClientOrderInfoEndpoint, "1"), HttpMethod.Get, ct, parameters: parameters, signed: true, weight: 2).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiOrderTrade>>> GetOrderTradesAsync(long orderId, CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiOrderTrade>>(_baseClient.GetUrl(OrderTradesEndpoint.FillPathParameters(orderId.ToString(CultureInfo.InvariantCulture)), "1"), HttpMethod.Get, ct, signed: true, weight: 2).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiOrder>>> GetClosedOrdersAsync(string symbol, IEnumerable<OrderState>? states = null, IEnumerable<Enums.OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();

            states ??= new OrderState[] { OrderState.Filled, OrderState.Canceled, OrderState.PartiallyCanceled };

            var stateConverter = new OrderStateConverter(false);
            var typeConverter = new OrderTypeConverter(false);
            var parameters = new Dictionary<string, object>
            {
                { "states", string.Join(",", states.Select(s => JsonConvert.SerializeObject(s, stateConverter))) }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("start-date", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end-date", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("types", types == null ? null : string.Join(",", types.Select(s => JsonConvert.SerializeObject(s, typeConverter))));
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("direct", direction == null ? null : JsonConvert.SerializeObject(direction, new FilterDirectionConverter(false)));
            parameters.AddOptionalParameter("size", limit);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiOrder>>(_baseClient.GetUrl(OrdersEndpoint, "1"), HttpMethod.Get, ct, parameters, true, weight: 2).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiOrderTrade>>> GetUserTradesAsync(IEnumerable<OrderState>? states = null, string? symbol = null, IEnumerable<Enums.OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol?.ValidateHuobiSymbol();
            var stateConverter = new OrderStateConverter(false);
            var typeConverter = new OrderTypeConverter(false);
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("states", states == null ? null : string.Join(",", states.Select(s => JsonConvert.SerializeObject(s, stateConverter))));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("start-time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end-time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("types", types == null ? null : string.Join(",", types.Select(s => JsonConvert.SerializeObject(s, typeConverter))));
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("direct", direction == null ? null : JsonConvert.SerializeObject(direction, new FilterDirectionConverter(false)));
            parameters.AddOptionalParameter("size", limit);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiOrderTrade>>(_baseClient.GetUrl(SymbolTradesEndpoint, "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiOrder>>> GetHistoricalOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol?.ValidateHuobiSymbol();
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("start-time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end-time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("direct", direction == null ? null : JsonConvert.SerializeObject(direction, new FilterDirectionConverter(false)));
            parameters.AddOptionalParameter("size", limit);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiOrder>>(_baseClient.GetUrl(HistoryOrdersEndpoint, "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result)
                return result.AsError<IEnumerable<HuobiOrder>>(result.Error!);

            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiPlacedConditionalOrder>> PlaceConditionalOrderAsync(
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
            var parameters = new Dictionary<string, object>
            {
                { "accountId", accountId },
                { "symbol", symbol },
                { "orderSide", EnumConverter.GetString(side) },
                { "orderType", EnumConverter.GetString(type) },
                { "clientOrderId", clientOrderId ?? Guid.NewGuid().ToString() },
                { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) },
            };

            parameters.AddOptionalParameter("orderPrice", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("orderSize", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("orderValue", quoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", EnumConverter.GetString(timeInForce));
            parameters.AddOptionalParameter("trailingRate", trailingRate?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendHuobiV2Request<HuobiPlacedConditionalOrder>(_baseClient.GetUrl("algo-orders", "2"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiConditionalOrderCancelResult>> CancelConditionalOrdersAsync(IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "clientOrderIds", clientOrderIds }
            };

            return await _baseClient.SendHuobiV2Request<HuobiConditionalOrderCancelResult>(_baseClient.GetUrl("algo-orders/cancellation", "2"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiConditionalOrder>>> GetOpenConditionalOrdersAsync(long? accountId = null, string? symbol = null, OrderSide? side = null, ConditionalOrderType? type = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("accountId", accountId);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderSide", EnumConverter.GetString(side));
            parameters.AddOptionalParameter("orderType", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("sort", sort);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("fromId", fromId);
            return await _baseClient.SendHuobiV2Request<IEnumerable<HuobiConditionalOrder>>(_baseClient.GetUrl("algo-orders/opening", "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiConditionalOrder>>> GetClosedConditionalOrdersAsync(
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
            var parameters = new Dictionary<string, object>()
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
            return await _baseClient.SendHuobiV2Request<IEnumerable<HuobiConditionalOrder>>(_baseClient.GetUrl("algo-orders/history", "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiConditionalOrder>> GetConditionalOrderAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "clientOrderId", clientOrderId }
            };
            return await _baseClient.SendHuobiV2Request<HuobiConditionalOrder>(_baseClient.GetUrl("algo-orders/specific", "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
    }
}
