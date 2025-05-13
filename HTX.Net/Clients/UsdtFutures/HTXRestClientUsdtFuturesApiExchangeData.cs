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
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/timestamp", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<DateTime>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<WebCallResult<HTXFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection() {
                { "contract_code", contractCode }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_funding_rate", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXFundingRate>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Rates

        /// <inheritdoc />
        public async Task<WebCallResult<HTXFundingRate[]>> GetFundingRatesAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_batch_funding_rate", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXFundingRate[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Historical Funding Rates

        /// <inheritdoc />
        public async Task<WebCallResult<HTXFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_historical_funding_rate", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXFundingRatePage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Liquidation Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXLiquidationOrder[]>> GetLiquidationOrdersAsync(
            string contractCode,
            LiquidationTradeType tradeType,
            string? symbol = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            FilterDirection? direction = null,
            long? fromId = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
            };
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalMilliseconds("start_time", startTime);
            parameters.AddOptionalMilliseconds("end_time", endTime);
            parameters.AddOptionalEnum("direct", direction);
            parameters.AddOptional("from_id", fromId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v3/swap_liquidation_orders", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendAsync<HTXLiquidationOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Historical Settlement Records

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSettlementPage>> GetHistoricalSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("start_time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end_time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_settlement_records", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXSettlementPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Top Trader Account Sentiment

        /// <inheritdoc />
        public async Task<WebCallResult<HTXAccountSentiment>> GetTopTraderAccountSentimentAsync(string contractCode, Period period, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddEnum("period", period);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_elite_account_ratio", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXAccountSentiment>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Top Trader Position Sentiment

        /// <inheritdoc />
        public async Task<WebCallResult<HTXAccountSentiment>> GetTopTraderPositionSentimentAsync(string contractCode, Period period, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddEnum("period", period);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_elite_position_ratio", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXAccountSentiment>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Status

        /// <inheritdoc />
        public async Task<WebCallResult<HTXContractStatus[]>> GetIsolatedMarginStatusAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_api_state", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXContractStatus[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Tiered Margin Info

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTieredCrossMarginInfo[]>> GetCrossTieredMarginInfoAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_cross_ladder_margin", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXTieredCrossMarginInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Tiered Info

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTieredMarginInfo[]>> GetIsolatedMarginTieredInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_ladder_margin", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXTieredMarginInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Estimated Setllement Price

        /// <inheritdoc />
        public async Task<WebCallResult<HTXEstimatedSettlementPrice[]>> GetEstimatedSettlementPriceAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_estimated_settlement_price", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXEstimatedSettlementPrice[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Adjust Factor Info

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSwapAdjustFactorInfo[]>> GetIsolatedMarginAdjustFactorInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_adjustfactor", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXSwapAdjustFactorInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Adjust Factor Info

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossSwapAdjustFactorInfo[]>> GetCrossMarginAdjustFactorInfoAsync(string? contractCode = null, string? asset = null, ContractType? type = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", asset);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_cross_adjustfactor", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXCrossSwapAdjustFactorInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Insurance Fund History

        /// <inheritdoc />
        public async Task<WebCallResult<HTXInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "linear-swap-api/v1/swap_insurance_fund", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXInsuranceInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Insurance Fund Info

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTotalInsuranceInfo>> GetInsuranceFundInfoAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v1/insurance_fund_info", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXTotalInsuranceInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Swap Risk Info

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSwapRiskInfo[]>> GetSwapRiskInfoAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_risk_info", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXSwapRiskInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Swap Price Limitation

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPriceLimitation[]>> GetSwapPriceLimitationAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "linear-swap-api/v1/swap_price_limit", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXPriceLimitation[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Swap Open Interest

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOpenInterest[]>> GetSwapOpenInterestAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "linear-swap-api/v1/swap_open_interest", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXOpenInterest[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Contracts

        /// <inheritdoc />
        public async Task<WebCallResult<HTXContractInfo[]>> GetContractsAsync(string? contractCode = null, MarginMode? supportMarginMode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("support_margin_mode", supportMarginMode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "linear-swap-api/v1/swap_contract_info", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXContractInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Swap Index Price

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSwapIndex[]>> GetSwapIndexPriceAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "linear-swap-api/v1/swap_index", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            return await _baseClient.SendBasicAsync<HTXSwapIndex[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Contract Elements

        /// <inheritdoc />
        public async Task<WebCallResult<HTXContractElements[]>> GetContractElementsAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_query_elements", HTXExchange.RateLimiter.UsdtPublicReference, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXContractElements[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderBook>> GetOrderBookAsync(string contractCode, int? mergeStep = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "type", "step" + (mergeStep ?? 0) },
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, "linear-swap-ex/market/depth", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXOrderBook>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Book Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSwapBookTicker[]>> GetBookTickerAsync(string? contractCode = null, BusinessType? type = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(type));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "linear-swap-ex/market/bbo", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXSwapBookTicker[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<HTXKline[]>> GetKlinesAsync(string contractCode, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            if (startTime == null && endTime == null && limit == null)
                limit = 100; // Limit is required if no time is given

            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) }
            };
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("from", DateTimeConverter.ConvertToSeconds(startTime));
            parameters.AddOptionalParameter("to", DateTimeConverter.ConvertToSeconds(endTime));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "linear-swap-ex/market/history/kline", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Mark Price Klines

        /// <inheritdoc />
        public async Task<WebCallResult<HTXKline[]>> GetMarkPriceKlinesAsync(string contractCode, KlineInterval klineInterval, int limit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract_code", contractCode);
            parameters.AddEnum("period", klineInterval);
            parameters.Add("size", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/index/market/history/linear_swap_mark_price_kline", HTXExchange.RateLimiter.PublicMarket, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTicker>> GetTickerAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, "linear-swap-ex/market/detail/merged", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXTicker>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<HTXListTicker[]>> GetTickersAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/linear-swap-ex/market/detail/batch_merged", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXListTicker[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Last Trade

        /// <inheritdoc />
        public async Task<WebCallResult<HTXLastTrade>> GetLastTradesAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "linear-swap-ex/market/trade", HTXExchange.RateLimiter.PublicMarket, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXLastTradeWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data?.Data?.First()!);
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTrade[]>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "size", limit }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "linear-swap-ex/market/history/trade", HTXExchange.RateLimiter.PublicMarket, 1, false);
            var result = await _baseClient.SendBasicAsync<HTXTradeWrapper[]>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data?.SelectMany(d => d.Data).ToArray()!);
        }

        #endregion

        #region Get Open Interest

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOpenInterestValue>> GetOpenInterestHistoryAsync(InterestPeriod period, Unit unit, string? contractCode = null, string? symbol = null, ContractType? type = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "period", EnumConverter.GetString(period) },
                { "amount_type", EnumConverter.GetString(unit) },
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(type));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_his_open_interest", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXOpenInterestValue>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Premium Index Klines

        /// <inheritdoc />
        public async Task<WebCallResult<HTXKline[]>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/index/market/history/linear_swap_premium_index_kline", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Estimated Funding Rate Klines

        /// <inheritdoc />
        public async Task<WebCallResult<HTXKline[]>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, "index/market/history/linear_swap_estimated_rate_kline", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Basis Data

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBasisData[]>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            parameters.AddOptionalParameter("basis_price_type", basisPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/index/market/history/linear_swap_basis", HTXExchange.RateLimiter.PublicMarket, 1, false);
            return await _baseClient.SendBasicAsync<HTXBasisData[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Trade Status 

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginTradeStatus[]>> GetCrossMarginTradeStatusAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_cross_trade_state", HTXExchange.RateLimiter.UsdtRead, 1, false);
            return await _baseClient.SendBasicAsync<HTXCrossMarginTradeStatus[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Transfer Status 

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginTransferStatus[]>> GetCrossMarginTransferStatusAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_cross_transfer_state", HTXExchange.RateLimiter.UsdtRead, 1, false);
            return await _baseClient.SendBasicAsync<HTXCrossMarginTransferStatus[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
