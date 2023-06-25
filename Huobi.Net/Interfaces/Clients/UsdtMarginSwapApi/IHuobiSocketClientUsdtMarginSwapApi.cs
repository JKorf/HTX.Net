using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Usdt margin swap streams
    /// </summary>
    public interface IHuobiSocketClientUsdtMarginSwapApi : ISocketApiClient
    {
        /// <summary>
        /// Subscribe to basis updates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-basis-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="priceType">Price type</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBasisUpdatesAsync(string contractCode, KlineInterval period, string priceType, Action<DataEvent<HuobiUsdtMarginSwapBasisUpdate>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to best offer updates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-market-bbo-data-push" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string contractCode, Action<DataEvent<HuobiBestOfferUpdate>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to estimated funding rate kline updates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-estimated-funding-rate-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to incremental order book updates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-incremental-market-depth-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="snapshot">Snapshot or incremental</param>
        /// <param name="limit">Depth</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string contractCode, bool snapshot, int limit, Action<DataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to index kline updates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-index-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to kline updates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-kline-data-of-mark-price" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-market-depth-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="mergeStep">Merge step</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string contractCode, int mergeStep, Action<DataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to premium index kline updates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-premium-index-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="period">Period</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPremiumIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to symbol ticker updates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-market-detail-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string contractCode, Action<DataEvent<HuobiSymbolTickUpdate>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to symbol trade updates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-trade-detail-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string contractCode, Action<DataEvent<HuobiUsdtMarginSwapTradesUpdate>> onData, CancellationToken ct = default);
    }
}