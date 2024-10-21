using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Models.UsdtMarginSwap;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// HTX usdt futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IHTXRestClientUsdtFuturesApiExchangeData
    {
        /// <summary>
        /// Get basis data
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8147e-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="basisPriceType">Price type (open, close, high, low, average)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXBasisData>>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = null, CancellationToken ct = default);
        /// <summary>
        /// Get the current best bid/ask values
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8098e-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="type">Type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXSwapBookTicker>>> GetBookTickerAsync(string? contractCode = null, BusinessType? type = null, CancellationToken ct = default);
        /// <summary>
        /// Get contract info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb802c2-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="supportMarginMode">Support margin mode</param>
        /// <param name="pair">Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXContractInfo>>> GetContractsAsync(string? contractCode = null, MarginMode? supportMarginMode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin adjust factor info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7fc0d-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="contractType">Type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXCrossSwapAdjustFactorInfo>>> GetCrossMarginAdjustFactorInfoAsync(string? contractCode = null, string? asset = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin trade status
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb841ac-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">Type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXCrossMarginTradeStatus>>> GetCrossMarginTradeStatusAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin transfer status
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89abf-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXCrossMarginTransferStatus>>> GetCrossMarginTransferStatusAsync(string? marginAccount = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross tiered margin info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f7a9-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXTieredCrossMarginInfo>>> GetCrossTieredMarginInfoAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get estimated funding rate klines
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb813aa-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXKline>>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get estimated settlement price
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f9d4-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXEstimatedSettlementPrice>>> GetEstimatedSettlementPriceAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get funding rate
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7ec03-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get funding rates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7ed6a-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXFundingRate>>> GetFundingRatesAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get historical funding rates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7ee4a-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get historical settlement records
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f323-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSettlementPage>> GetHistoricalSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get insurance fund history
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7fd58-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get the total amount of risk funds for all current business lines, priced in USDT.
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-192412eef1f" /></para>
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<HTXTotalInsuranceInfo>> GetInsuranceFundInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin adjust factor info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7fb2c-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXSwapAdjustFactorInfo>>> GetIsolatedMarginAdjustFactorInfoAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin status
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f665-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXContractStatus>>> GetIsolatedMarginStatusAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin tier info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f887-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXTieredMarginInfo>>> GetIsolatedMarginTieredInfoAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get klines
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80aca-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXKline>>> GetKlinesAsync(string contractCode, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
        /// <summary>
        /// Get last trades
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80f4c-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXLastTrade>> GetLastTradesAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get liquidation orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f19e-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">Pair, for example `ETH-USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="direction">Result direction</param>
        /// <param name="fromId">Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXLiquidationOrder>>> GetLiquidationOrdersAsync(string contractCode,
            LiquidationTradeType tradeType,
            string? pair = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            FilterDirection? direction = null,
            long? fromId = null,
            CancellationToken ct = default);
        /// <summary>
        /// Get ticker
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80ce4-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTicker>> GetTickerAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get tickers
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80df2-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXListTicker>>> GetTickersAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get open interest
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8117d-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="period">Period</param>
        /// <param name="unit">Unit</param>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="type">Type</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOpenInterestValue>> GetOpenInterestHistoryAsync(InterestPeriod period, Unit unit, string? contractCode = null, string? symbol = null, ContractType? type = null, int? limit = null, CancellationToken ct = default);
        /// <summary>
        /// Get order book
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb808ad-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="mergeStep">Merge step</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderBook>> GetOrderBookAsync(string contractCode, int? mergeStep = null, CancellationToken ct = default);
        /// <summary>
        /// Get premium index klines
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb81255-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="interval">Interval</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXKline>>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get recent trades
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb81024-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="limit">Max number of results to return</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXTrade>>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get server time
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80500-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);
        /// <summary>
        /// Get swap index price
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80424-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXSwapIndex>>> GetSwapIndexPriceAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap open interest
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80166-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business tpye</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXOpenInterest>>> GetSwapOpenInterestAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap price limitation
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80013-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">Contract tpye</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXPriceLimitation>>> GetSwapPriceLimitationAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap risk info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7feba-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXSwapRiskInfo>>> GetSwapRiskInfoAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// Get top trader account sentiment
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f487-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="period">Period</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXAccountSentiment>> GetTopTraderAccountSentimentAsync(string contractCode, Period period, CancellationToken ct = default);

        /// <summary>
        /// Get top trader position sentiment
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f568-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="period">Period</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXAccountSentiment>> GetTopTraderPositionSentimentAsync(string contractCode, Period period, CancellationToken ct = default);

        /// <summary>
        /// Get contract elements and info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-18bd764260c" /></para>
        /// </summary>
        /// <param name="contractCode">Filter by contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXContractElements>>> GetContractElementsAsync(string contractCode, CancellationToken ct = default);

        /// <summary>
        /// Get mark price klines
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80ba5-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="klineInterval">Kline interval</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXKline>>> GetMarkPriceKlinesAsync(string contractCode, KlineInterval klineInterval, int limit, CancellationToken ct = default);

    }
}