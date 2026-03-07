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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8147e-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /index/market/history/linear_swap_basis
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="interval">["<c>period</c>"] Kline interval</param>
        /// <param name="limit">["<c>size</c>"] Limit</param>
        /// <param name="basisPriceType">["<c>basis_price_type</c>"] Price type (open, close, high, low, average)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBasisData[]>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = null, CancellationToken ct = default);
        /// <summary>
        /// Get the current best bid/ask values
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8098e-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-ex/market/bbo
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="type">["<c>business_type</c>"] Type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSwapBookTicker[]>> GetBookTickerAsync(string? contractCode = null, BusinessType? type = null, CancellationToken ct = default);
        /// <summary>
        /// Get contract info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb802c2-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_contract_info
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="supportMarginMode">["<c>support_margin_mode</c>"] Support margin mode</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="businessType">["<c>business_type</c>"] Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXContractInfo[]>> GetContractsAsync(string? contractCode = null, MarginMode? supportMarginMode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin adjust factor info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7fc0d-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_cross_adjustfactor
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="asset">["<c>pair</c>"] Asset, for example `ETH`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Type</param>
        /// <param name="businessType">["<c>business_type</c>"] Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossSwapAdjustFactorInfo[]>> GetCrossMarginAdjustFactorInfoAsync(string? contractCode = null, string? asset = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin trade status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb841ac-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_cross_trade_state
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Type</param>
        /// <param name="businessType">["<c>business_type</c>"] Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossMarginTradeStatus[]>> GetCrossMarginTradeStatusAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin transfer status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89abf-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_cross_transfer_state
        /// </para>
        /// </summary>
        /// <param name="marginAccount">["<c>margin_account</c>"] Margin account, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossMarginTransferStatus[]>> GetCrossMarginTransferStatusAsync(string? marginAccount = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross tiered margin info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f7a9-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_cross_ladder_margin
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="businessType">["<c>business_type</c>"] Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTieredCrossMarginInfo[]>> GetCrossTieredMarginInfoAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get estimated funding rate klines
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb813aa-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /index/market/history/linear_swap_estimated_rate_kline
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="interval">["<c>period</c>"] Kline interval</param>
        /// <param name="limit">["<c>size</c>"] Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXKline[]>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get estimated settlement price
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f9d4-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_estimated_settlement_price
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="businessType">["<c>business_type</c>"] Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXEstimatedSettlementPrice[]>> GetEstimatedSettlementPriceAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get funding rate
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7ec03-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_funding_rate
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get funding rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7ed6a-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_batch_funding_rate
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXFundingRate[]>> GetFundingRatesAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get historical funding rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7ee4a-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_historical_funding_rate
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get historical settlement records
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f323-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_settlement_records
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSettlementPage>> GetHistoricalSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get insurance fund history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7fd58-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_insurance_fund
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get the total amount of risk funds for all current business lines, priced in USDT.
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-192412eef1f" /><br />
        /// Endpoint:<br />
        /// GET /v1/insurance_fund_info
        /// </para>
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<HTXTotalInsuranceInfo>> GetInsuranceFundInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin adjust factor info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7fb2c-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_adjustfactor
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSwapAdjustFactorInfo[]>> GetIsolatedMarginAdjustFactorInfoAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f665-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_api_state
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXContractStatus[]>> GetIsolatedMarginStatusAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin tier info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f887-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_ladder_margin
        /// </para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTieredMarginInfo[]>> GetIsolatedMarginTieredInfoAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get klines
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80aca-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-ex/market/history/kline
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="interval">["<c>period</c>"] Kline interval</param>
        /// <param name="limit">["<c>size</c>"] Limit</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXKline[]>> GetKlinesAsync(string contractCode, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
        /// <summary>
        /// Get last trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80f4c-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-ex/market/trade
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="businessType">["<c>business_type</c>"] Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXLastTrade>> GetLastTradesAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get liquidation orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f19e-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v3/swap_liquidation_orders
        /// </para>
        /// </summary>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="contractCode">["<c>contract</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="direction">["<c>direct</c>"] Result direction</param>
        /// <param name="fromId">["<c>from_id</c>"] Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXLiquidationOrder[]>> GetLiquidationOrdersAsync(string contractCode,
            LiquidationTradeType tradeType,
            string? pair = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            FilterDirection? direction = null,
            long? fromId = null,
            CancellationToken ct = default);
        /// <summary>
        /// Get ticker
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80ce4-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-ex/market/detail/merged
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTicker>> GetTickerAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get tickers
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80df2-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/linear-swap-ex/market/detail/batch_merged
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="businessType">["<c>business_type</c>"] Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXListTicker[]>> GetTickersAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get open interest
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8117d-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_his_open_interest
        /// </para>
        /// </summary>
        /// <param name="period">["<c>period</c>"] Period</param>
        /// <param name="unit">["<c>amount_type</c>"] Unit</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="symbol">["<c>pair</c>"] Symbol</param>
        /// <param name="type">["<c>contract_type</c>"] Type</param>
        /// <param name="limit">["<c>size</c>"] Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOpenInterestValue>> GetOpenInterestHistoryAsync(InterestPeriod period, Unit unit, string? contractCode = null, string? symbol = null, ContractType? type = null, int? limit = null, CancellationToken ct = default);
        /// <summary>
        /// Get order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb808ad-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-ex/market/depth
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="mergeStep">["<c>type</c>"] Merge step</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderBook>> GetOrderBookAsync(string contractCode, int? mergeStep = null, CancellationToken ct = default);
        /// <summary>
        /// Get premium index klines
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb81255-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /index/market/history/linear_swap_premium_index_kline
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="interval">["<c>period</c>"] Interval</param>
        /// <param name="limit">["<c>size</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXKline[]>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get recent trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb81024-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-ex/market/history/trade
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="limit">["<c>size</c>"] Max number of results to return</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTrade[]>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80500-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/timestamp
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);
        /// <summary>
        /// Get swap index price
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80424-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_index
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSwapIndex[]>> GetSwapIndexPriceAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap open interest
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80166-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_open_interest
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="businessType">["<c>business_type</c>"] Business tpye</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOpenInterest[]>> GetSwapOpenInterestAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap price limitation
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80013-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_price_limit
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract tpye</param>
        /// <param name="businessType">["<c>business_type</c>"] Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXPriceLimitation[]>> GetSwapPriceLimitationAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap risk info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7feba-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_risk_info
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="businessType">["<c>business_type</c>"] Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSwapRiskInfo[]>> GetSwapRiskInfoAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// Get top trader account sentiment
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f487-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_elite_account_ratio
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="period">["<c>period</c>"] Period</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXAccountSentiment>> GetTopTraderAccountSentimentAsync(string contractCode, Period period, CancellationToken ct = default);

        /// <summary>
        /// Get top trader position sentiment
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb7f568-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_elite_position_ratio
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="period">["<c>period</c>"] Period</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXAccountSentiment>> GetTopTraderPositionSentimentAsync(string contractCode, Period period, CancellationToken ct = default);

        /// <summary>
        /// Get contract elements and info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-18bd764260c" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_query_elements
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Filter by contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXContractElements[]>> GetContractElementsAsync(string contractCode, CancellationToken ct = default);

        /// <summary>
        /// Get mark price klines
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb80ba5-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /index/market/history/linear_swap_mark_price_kline
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="klineInterval">["<c>period</c>"] Kline interval</param>
        /// <param name="limit">["<c>size</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXKline[]>> GetMarkPriceKlinesAsync(string contractCode, KlineInterval klineInterval, int limit, CancellationToken ct = default);

    }
}
