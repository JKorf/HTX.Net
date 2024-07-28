using CryptoExchange.Net.Objects;
using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HTX.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// HTX usdt margin swap exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IHTXRestClientUsdtMarginSwapApiExchangeData
    {
        /// <summary>
        /// Get basis data
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-basis-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="basisPriceType">Price type (open, close, high, low, average)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXBasisData>>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = null, CancellationToken ct = default);
        /// <summary>
        /// Get the best current offer
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-bbo-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="type">Type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXSwapBestOffer>>> GetBestOfferAsync(string? contractCode = null, BusinessType? type = null, CancellationToken ct = default);
        /// <summary>
        /// Get contract info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-info" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="supportMarginMode">Support margin mode</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXContractInfo>>> GetContractInfoAsync(string? contractCode = null, MarginMode? supportMarginMode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin adjust factor info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-tiered-adjustment-factor" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="asset">Asset</param>
        /// <param name="contractType">Type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXCrossSwapAdjustFactorInfo>>> GetCrossMarginAdjustFactorInfoAsync(string? contractCode = null, string? asset = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin trade status
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-trade-state" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Asset</param>
        /// <param name="contractType">Type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXCrossMarginTradeStatus>>> GetCrossMarginTradeStatusAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin transfer status
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-transfer-state" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXCrossMarginTransferStatus>>> GetCrossMarginTransferStatusAsync(string? marginAccount = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross tiered margin info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-tiered-margin" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXTieredCrossMarginInfo>>> GetCrossTieredMarginInfoAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Get estimated funding rate kliens
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-estimated-funding-rate-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXKline>>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get estimated settlement price
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-the-estimated-settlement-price" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXEstimatedSettlementPrice>>> GetEstimatedSettlementPriceAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get funding rate
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-funding-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get funding rates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-a-batch-of-funding-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXFundingRate>>> GetFundingRatesAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get historical funding rates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-historical-funding-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get historical settlement records
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-historical-settlement-records-of-the-platform-interface" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSettlementPage>> GetHistoricalSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get insurance fund history
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-history-records-of-insurance-fund-balance" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin adjust factor info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-tiered-adjustment-factor" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXSwapAdjustFactorInfo>>> GetIsolatedMarginAdjustFactorInfoAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin status
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-system-status" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXContractStatus>>> GetIsolatedStatusAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin tier info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-tiered-margin" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXTieredMarginInfo>>> GetIsolatedMarginTieredInfoAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get klines
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="from">Filter by start time</param>
        /// <param name="to">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXKline>>> GetKlinesAsync(string contractCode, KlineInterval interval, int? limit = null, DateTime? from = null, DateTime? to = null, CancellationToken ct = default);
        /// <summary>
        /// Get last trades
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-the-last-trade-of-a-contract" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXLastTrade>> GetLastTradesAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get liquidation orders
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-liquidation-orders" /></para>
        /// </summary>
        /// <param name="createDate">Create date</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXLiquidationOrderPage>> GetLiquidationOrdersAsync(int createDate, LiquidationTradeType tradeType, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get market data
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-data-overview" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXMarketData>> GetMarketDataAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get market datas
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-a-batch-of-market-data-overview" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXMarketData>>> GetMarketDatasAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get open interest
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-open-interest" /></para>
        /// </summary>
        /// <param name="period">Period</param>
        /// <param name="unit">Unit</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="type">Type</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOpenInterestValue>> GetOpenInterestAsync(InterestPeriod period, Unit unit, string? contractCode = null, string? symbol = null, ContractType? type = null, int? limit = null, CancellationToken ct = default);
        /// <summary>
        /// Get order book
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-depth" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="step">Merge step</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderBook>> GetOrderBookAsync(string contractCode, string step, CancellationToken ct = default);
        /// <summary>
        /// Get premium index klines
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-premium-index-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="interval">Interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXKline>>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get recent trades
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-a-batch-of-trade-records-of-a-contract" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXTrade>>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get server time
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#get-current-system-timestamp" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);
        /// <summary>
        /// Get swap index price
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-index-price-information" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXSwapIndex>>> GetSwapIndexPriceAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap open interest
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-swap-open-interest-information" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business tpye</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXOpenInterest>>> GetSwapOpenInterestAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap price limitation
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-price-limitation" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract tpye</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXPriceLimitation>>> GetSwapPriceLimitationAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap risk info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-contract-insurance-fund-balance-and-estimated-clawback-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXSwapRiskInfo>>> GetSwapRiskInfoAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
    }
}