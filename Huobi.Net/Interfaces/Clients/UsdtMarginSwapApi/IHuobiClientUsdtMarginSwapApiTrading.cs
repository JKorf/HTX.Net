using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Huobi usdt margin swap trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IHuobiClientUsdtMarginSwapApiTrading
    {
        Task<WebCallResult<HuobiBatchResult>> CancelAllCrossMarginOrdersAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiBatchResult>> CancelAllIsolatedMarginOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiBatchResult>> CancelCrossMarginOrderAsync(long? orderId = null, long? clientOrderId = null, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiBatchResult>> CancelCrossMarginOrderAsync(IEnumerable<long> orderId, IEnumerable<long> clientOrderId, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiBatchResult>> CancelIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiBatchResult>> CancelIsolatedMarginOrderAsync(string contractCode, IEnumerable<long> orderId, IEnumerable<long> clientOrderId, CancellationToken ct = default);
        Task<WebCallResult<HuobiCrossMarginLeverageRate>> ChangeCrossMarginLeverageAsync(int leverageRate, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiIsolatedMarginLeverageRate>> ChangeIsolatedMarginLeverageAsync(string contractCode, int leverageRate, CancellationToken ct = default);
        Task<WebCallResult<HuobiCrossMarginOrderPage>> GetCrossMarginClosedOrdersAsync(MarginTradeType tradeType, bool allOrders, int daysInHistory, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiCrossMarginOrderPage>> GetCrossMarginOpenOrdersAsync(string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiCrossMarginOrder>>> GetCrossMarginOrderAsync(string? contractCode = null, string? symbol = null, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiMarginOrderDetails>> GetCrossMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiCrossMarginOrder>>> GetCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? symbol = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiCrossMarginUserTradePage>> GetCrossMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiIsolatedMarginOrderPage>> GetIsolatedMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, bool allOrders, int daysInHistory, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiIsolatedMarginOrderPage>> GetIsolatedMarginOpenOrdersAsync(string contractCode, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiIsolatedMarginOrder>>> GetIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiMarginOrderDetails>> GetIsolatedMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiIsolatedMarginOrder>>> GetIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, CancellationToken ct = default);
        Task<WebCallResult<HuobiIsolatedMarginUserTradePage>> GetIsolatedMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiPlacedOrderId>> PlaceCrossMarginOrderAsync(decimal quantity, OrderSide side, decimal leverageRate, string? contractCode = null, string? symbol = null, ContractType? contractType = null, decimal? price = null, Offset? offset = null, OrderPriceType? orderPriceType = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, bool? reduceOnly = null, long? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiPlacedOrderId>> PlaceIsolatedMarginOrderAsync(string contractCode, decimal quantity, OrderSide side, decimal leverageRate, decimal? price = null, Offset? offset = null, OrderPriceType? orderPriceType = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, bool? reduceOnly = null, long? clientOrderId = null, CancellationToken ct = default);
    }
}