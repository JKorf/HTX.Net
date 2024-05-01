using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Huobi usdt margin swap exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IHuobiRestClientUsdtMarginSwapApiExchangeData
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
        Task<WebCallResult<IEnumerable<HuobiBasisData>>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = null, CancellationToken ct = default);
        /// <summary>
        /// Get the best current offer
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-bbo-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="type">Type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSwapBestOffer>>> GetBestOfferAsync(string? contractCode = null, BusinessType? type = null, CancellationToken ct = default);
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
        Task<WebCallResult<IEnumerable<HuobiContractInfo>>> GetContractInfoAsync(string? contractCode = null, MarginMode? supportMarginMode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin adjust factor info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-tiered-adjustment-factor" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="asset">Asset</param>
        /// <param name="contractType">Type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiCrossSwapAdjustFactorInfo>>> GetCrossMarginAdjustFactorInfoAsync(string? contractCode = null, string? asset = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin trade status
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-trade-state" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Asset</param>
        /// <param name="contractType">Type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiCrossMarginTradeStatus>>> GetCrossMarginTradeStatusAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin transfer status
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-transfer-state" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiCrossMarginTransferStatus>>> GetCrossMarginTransferStatusAsync(string? marginAccount = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross tiered margin info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-tiered-margin" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiTieredCrossMarginInfo>>> GetCrossTieredMarginInfoAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Get estimated funding rate kliens
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-estimated-funding-rate-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiKline>>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
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
        Task<WebCallResult<IEnumerable<HuobiEstimatedSettlementPrice>>> GetEstimatedSettlementPriceAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get funding rate
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-funding-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get funding rates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-a-batch-of-funding-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiFundingRate>>> GetFundingRatesAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get historical funding rates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-historical-funding-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);
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
        Task<WebCallResult<HuobiSettlementPage>> GetHistoricalSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get insurance fund history
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-history-records-of-insurance-fund-balance" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin adjust factor info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-tiered-adjustment-factor" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSwapAdjustFactorInfo>>> GetIsolatedMarginAdjustFactorInfoAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin status
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-system-status" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiContractStatus>>> GetIsolatedStatusAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin tier info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-tiered-margin" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiTieredMarginInfo>>> GetIsolatedMarginTieredInfoAsync(string? contractCode = null, CancellationToken ct = default);
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
        Task<WebCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string contractCode, KlineInterval interval, int? limit = null, DateTime? from = null, DateTime? to = null, CancellationToken ct = default);
        /// <summary>
        /// Get last trades
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-the-last-trade-of-a-contract" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiLastTrade>> GetLastTradesAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
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
        Task<WebCallResult<HuobiLiquidationOrderPage>> GetLiquidationOrdersAsync(int createDate, LiquidationTradeType tradeType, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get market data
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-data-overview" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiMarketData>> GetMarketDataAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get market datas
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-a-batch-of-market-data-overview" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiMarketData>>> GetMarketDatasAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
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
        Task<WebCallResult<HuobiOpenInterestValue>> GetOpenInterestAsync(InterestPeriod period, Unit unit, string? contractCode = null, string? symbol = null, ContractType? type = null, int? limit = null, CancellationToken ct = default);
        /// <summary>
        /// Get order book
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-depth" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="step">Merge step</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOrderBook>> GetOrderBookAsync(string contractCode, string step, CancellationToken ct = default);
        /// <summary>
        /// Get premium index klines
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-premium-index-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="interval">Interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiKline>>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get recent trades
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-a-batch-of-trade-records-of-a-contract" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiTrade>>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default);
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
        Task<WebCallResult<IEnumerable<HuobiSwapIndex>>> GetSwapIndexPriceAsync(string? contractCode = null, CancellationToken ct = default);
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
        Task<WebCallResult<IEnumerable<HuobiOpenInterest>>> GetSwapOpenInterestAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
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
        Task<WebCallResult<IEnumerable<HuobiPriceLimitation>>> GetSwapPriceLimitationAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap risk info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-contract-insurance-fund-balance-and-estimated-clawback-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSwapRiskInfo>>> GetSwapRiskInfoAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
    }
}