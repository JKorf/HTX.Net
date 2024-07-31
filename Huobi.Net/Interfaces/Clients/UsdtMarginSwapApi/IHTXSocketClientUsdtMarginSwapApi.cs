using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Models.Socket;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HTX.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Usdt margin swap streams
    /// </summary>
    public interface IHTXSocketClientUsdtMarginSwapApi : ISocketApiClient
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
        Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string contractCode, bool snapshot, int limit, Action<DataEvent<HTXUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default);
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
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default);
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
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string contractCode, int mergeStep, Action<DataEvent<HTXUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default);
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
    }
}