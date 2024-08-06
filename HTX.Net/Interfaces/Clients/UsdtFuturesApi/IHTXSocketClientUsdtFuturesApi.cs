using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Models.Socket;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Usdt futures streams
    /// </summary>
    public interface IHTXSocketClientUsdtFuturesApi : ISocketApiClient
    {
        /// <summary>
        /// Subscribe to basis updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7d374-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="priceType">Price type</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBasisUpdatesAsync(string contractCode, KlineInterval period, string priceType, Action<DataEvent<HTXUsdtMarginSwapBasisUpdate>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to best offer updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7c802-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string contractCode, Action<DataEvent<HTXBestOfferUpdate>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to estimated funding rate kline updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7d138-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to incremental order book updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7c51d-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="snapshot">Snapshot or incremental</param>
        /// <param name="limit">Depth</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string contractCode, bool snapshot, int limit, Action<DataEvent<HTXIncrementalOrderBookUpdate>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to index kline updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7cc15-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to kline updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7bf6d-77b5-11ed-9966-0242ac110003" /></para >
        /// </summary >
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXSwapKline>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7d626-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7c385-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="mergeStep">Merge step</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string contractCode, int mergeStep, Action<DataEvent<HTXOrderBookUpdate>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to premium index kline updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7cecd-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPremiumIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to symbol ticker updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7c694-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string contractCode, Action<DataEvent<HTXSymbolTickUpdate>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to symbol trade updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7cab7-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string contractCode, Action<DataEvent<HTXUsdtMarginSwapTradesUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to system status updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7e8d8-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSystemStatusUpdatesAsync(Action<DataEvent<HTXStatusUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order updates
        /// <para><a href="https://www.htx.com/en-in/opend/newApiPages/?id=8cb7d8d9-77b5-11ed-9966-0242ac110003" /></para>
        /// <para><a href="https://www.htx.com/en-in/opend/newApiPages/?id=8cb7da4b-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="mode">Margin mode</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(MarginMode mode, Action<DataEvent<HTXUsdtMarginSwapOrderUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to isolated margin balance updates
        /// <para><a href="https://www.htx.com/en-in/opend/newApiPages/?id=8cb7dbbb-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginBalanceUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedBalanceUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to cross margin balance updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7dcca-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginBalanceUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossBalanceUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to isolated margin position updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7dbbb-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginPositionUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedPositionUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to cross margin position updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7df0f-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginPositionUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossPositionUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to isolated margin user trades
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7e05a-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginUserTradeUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedTradeUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to cross margin user trades
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7e155-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginUserTradeUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossTradeUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to liquidation order updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7e25d-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapLiquidationUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7e3b0-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(Action<DataEvent<IEnumerable<HTXUsdtMarginSwapFundingRateUpdate>>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to contract info updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7e49a-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToContractUpdatesAsync(Action<DataEvent<IEnumerable<HTXUsdtMarginSwapContractUpdate>>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to contract element updates. Only changed properties are send after the initial update, other properties will be `null`.
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-18ef15d1f28" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToContractElementsUpdatesAsync(Action<DataEvent<IEnumerable<HTXUsdtMarginSwapContractElementsUpdate>>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to isolated margin trigger order updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7e5fc-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginTriggerOrderUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedTriggerOrderUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to cross margin trigger order updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7e753-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginTriggerOrderUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossTriggerOrderUpdate>> onData, CancellationToken ct = default);
    }
}