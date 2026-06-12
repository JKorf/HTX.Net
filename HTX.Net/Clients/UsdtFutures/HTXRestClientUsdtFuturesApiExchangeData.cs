using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Models.UsdtMarginSwap;

namespace HTX.Net.Clients.UsdtFutures
{
    /// <inheritdoc />
    internal class HTXRestClientUsdtFuturesApiExchangeData : IHTXRestClientUsdtFuturesApiExchangeData
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientUsdtFuturesApi _baseClient;

        internal HTXRestClientUsdtFuturesApiExchangeData(HTXRestClientUsdtFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/timestamp", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<DateTime>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<HttpResult<HTXFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings) {
                { "contract_code", contractCode }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_funding_rate", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXFundingRate>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Rates

        /// <inheritdoc />
        public async Task<HttpResult<HTXFundingRate[]>> GetFundingRatesAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_batch_funding_rate", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXFundingRate[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Historical Funding Rates

        /// <inheritdoc />
        public async Task<HttpResult<HTXFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_historical_funding_rate", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXFundingRatePage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Liquidation Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXLiquidationOrder[]>> GetLiquidationOrdersAsync(
            string contractCode,
            LiquidationTradeType tradeType,
            string? symbol = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            FilterDirection? direction = null,
            long? fromId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
            };
            parameters.Add("pair", symbol);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("direct", direction);
            parameters.Add("from_id", fromId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v3/swap_liquidation_orders", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendAsync<HTXLiquidationOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Historical Settlement Records

        /// <inheritdoc />
        public async Task<HttpResult<HTXSettlementPage>> GetHistoricalSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("start_time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.Add("end_time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_settlement_records", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXSettlementPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Top Trader Account Sentiment

        /// <inheritdoc />
        public async Task<HttpResult<HTXAccountSentiment>> GetTopTraderAccountSentimentAsync(string contractCode, Period period, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("period", period);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_elite_account_ratio", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXAccountSentiment>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Top Trader Position Sentiment

        /// <inheritdoc />
        public async Task<HttpResult<HTXAccountSentiment>> GetTopTraderPositionSentimentAsync(string contractCode, Period period, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("period", period);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_elite_position_ratio", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXAccountSentiment>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Status

        /// <inheritdoc />
        public async Task<HttpResult<HTXContractStatus[]>> GetIsolatedMarginStatusAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_api_state", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXContractStatus[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Tiered Margin Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXTieredCrossMarginInfo[]>> GetCrossTieredMarginInfoAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            parameters.Add("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_ladder_margin", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXTieredCrossMarginInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Tiered Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXTieredMarginInfo[]>> GetIsolatedMarginTieredInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_ladder_margin", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXTieredMarginInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Estimated Setllement Price

        /// <inheritdoc />
        public async Task<HttpResult<HTXEstimatedSettlementPrice[]>> GetEstimatedSettlementPriceAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            parameters.Add("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_estimated_settlement_price", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXEstimatedSettlementPrice[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Adjust Factor Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXSwapAdjustFactorInfo[]>> GetIsolatedMarginAdjustFactorInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_adjustfactor", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXSwapAdjustFactorInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Adjust Factor Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossSwapAdjustFactorInfo[]>> GetCrossMarginAdjustFactorInfoAsync(string? contractCode = null, string? asset = null, ContractType? type = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", asset);
            parameters.Add("contract_type", EnumConverter.GetString(type));
            parameters.Add("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_adjustfactor", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXCrossSwapAdjustFactorInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Insurance Fund History

        /// <inheritdoc />
        public async Task<HttpResult<HTXInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-api/v1/swap_insurance_fund", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXInsuranceInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Insurance Fund Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXTotalInsuranceInfo>> GetInsuranceFundInfoAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v1/insurance_fund_info", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXTotalInsuranceInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Swap Risk Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXSwapRiskInfo[]>> GetSwapRiskInfoAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_risk_info", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXSwapRiskInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Swap Price Limitation

        /// <inheritdoc />
        public async Task<HttpResult<HTXPriceLimitation[]>> GetSwapPriceLimitationAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            parameters.Add("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-api/v1/swap_price_limit", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXPriceLimitation[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Swap Open Interest

        /// <inheritdoc />
        public async Task<HttpResult<HTXOpenInterest[]>> GetSwapOpenInterestAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            parameters.Add("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-api/v1/swap_open_interest", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXOpenInterest[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Contracts

        /// <inheritdoc />
        public async Task<HttpResult<HTXContractInfo[]>> GetContractsAsync(string? contractCode = null, MarginMode? supportMarginMode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("support_margin_mode", supportMarginMode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            parameters.Add("business_type", EnumConverter.GetString(businessType));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-api/v1/swap_contract_info", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXContractInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Swap Index Price

        /// <inheritdoc />
        public async Task<HttpResult<HTXSwapIndex[]>> GetSwapIndexPriceAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-api/v1/swap_index", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXSwapIndex[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Contract Elements

        /// <inheritdoc />
        public async Task<HttpResult<HTXContractElements[]>> GetContractElementsAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_query_elements", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXContractElements[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderBook>> GetOrderBookAsync(string contractCode, int? mergeStep = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "type", "step" + (mergeStep ?? 0) },
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-ex/market/depth", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXOrderBook>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Book Ticker

        /// <inheritdoc />
        public async Task<HttpResult<HTXSwapBookTicker[]>> GetBookTickerAsync(string? contractCode = null, BusinessType? type = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("business_type", EnumConverter.GetString(type));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-ex/market/bbo", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXSwapBookTicker[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<HTXKline[]>> GetKlinesAsync(string contractCode, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            if (startTime == null && endTime == null && limit == null)
                limit = 100; // Limit is required if no time is given

            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) }
            };
            parameters.Add("size", limit);
            parameters.Add("from", DateTimeConverter.ConvertToSeconds(startTime));
            parameters.Add("to", DateTimeConverter.ConvertToSeconds(endTime));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-ex/market/history/kline", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Mark Price Klines

        /// <inheritdoc />
        public async Task<HttpResult<HTXKline[]>> GetMarkPriceKlinesAsync(string contractCode, KlineInterval klineInterval, int limit, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("period", klineInterval);
            parameters.Add("size", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/index/market/history/linear_swap_mark_price_kline", HTXExchange.RateLimiter.PublicMarket, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Ticker

        /// <inheritdoc />
        public async Task<HttpResult<HTXTicker>> GetTickerAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-ex/market/detail/merged", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXTicker>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<HttpResult<HTXListTicker[]>> GetTickersAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/linear-swap-ex/market/detail/batch_merged", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXListTicker[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Last Trade

        /// <inheritdoc />
        public async Task<HttpResult<HTXLastTrade>> GetLastTradesAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-ex/market/trade", HTXExchange.RateLimiter.PublicMarket, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXLastTradeWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<HTXLastTrade>(result);

            return HttpResult.Ok(result, result.Data.Data.First());
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<HttpResult<HTXTrade[]>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "size", limit }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-ex/market/history/trade", HTXExchange.RateLimiter.PublicMarket, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXTradeWrapper[]>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<HTXTrade[]>(result);

            return HttpResult.Ok(result, result.Data.SelectMany(d => d.Data).ToArray());
        }

        #endregion

        #region Get Open Interest

        /// <inheritdoc />
        public async Task<HttpResult<HTXOpenInterestValue>> GetOpenInterestHistoryAsync(InterestPeriod period, HTX.Net.Enums.Unit unit, string? contractCode = null, string? symbol = null, ContractType? type = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "period", EnumConverter.GetString(period) },
                { "amount_type", EnumConverter.GetString(unit) },
            };
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("size", limit);
            parameters.Add("contract_type", EnumConverter.GetString(type));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_his_open_interest", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXOpenInterestValue>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Premium Index Klines

        /// <inheritdoc />
        public async Task<HttpResult<HTXKline[]>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/index/market/history/linear_swap_premium_index_kline", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Estimated Funding Rate Klines

        /// <inheritdoc />
        public async Task<HttpResult<HTXKline[]>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "index/market/history/linear_swap_estimated_rate_kline", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Basis Data

        /// <inheritdoc />
        public async Task<HttpResult<HTXBasisData[]>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            parameters.Add("basis_price_type", basisPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/index/market/history/linear_swap_basis", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXBasisData[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Trade Status 

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginTradeStatus[]>> GetCrossMarginTradeStatusAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            parameters.Add("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_trade_state", HTXExchange.RateLimiter.UsdtRead, 1, false);
            return await _baseClient.SendBasicAsync<HTXCrossMarginTradeStatus[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Transfer Status 

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginTransferStatus[]>> GetCrossMarginTransferStatusAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("margin_account", marginAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_transfer_state", HTXExchange.RateLimiter.UsdtRead, 1, false);
            return await _baseClient.SendBasicAsync<HTXCrossMarginTransferStatus[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
