using HTX.Net.Clients.FuturesApi;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtMarginSwapApi;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Models.UsdtMarginSwap;

namespace HTX.Net.Clients.UsdtMarginSwapApi
{
    /// <inheritdoc />
    internal class HTXRestClientUsdtMarginSwapApiExchangeData : IHTXRestClientUsdtMarginSwapApiExchangeData
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientUsdtMarginSwapApi _baseClient;

        internal HTXRestClientUsdtMarginSwapApiExchangeData(HTXRestClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendTimestampRequestAsync(_baseClient.GetUrl("api/v1/timestamp"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXContractInfo>>> GetContractInfoAsync(string? contractCode = null, MarginMode? supportMarginMode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("support_margin_mode", supportMarginMode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));

            return await _baseClient.SendHTXRequest<IEnumerable<HTXContractInfo>>(_baseClient.GetUrl("linear-swap-api/v1/swap_contract_info"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSwapIndex>>> GetSwapIndexPriceAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXSwapIndex>>(_baseClient.GetUrl("linear-swap-api/v1/swap_index"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXPriceLimitation>>> GetSwapPriceLimitationAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXPriceLimitation>>(_baseClient.GetUrl("linear-swap-api/v1/swap_price_limit"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXOpenInterest>>> GetSwapOpenInterestAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXOpenInterest>>(_baseClient.GetUrl("linear-swap-api/v1/swap_open_interest"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderBook>> GetOrderBookAsync(string contractCode, string step, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "type", step },
            };
            return await _baseClient.SendHTXRequest<HTXOrderBook>(_baseClient.GetUrl("linear-swap-ex/market/depth"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSwapBestOffer>>> GetBestOfferAsync(string? contractCode = null, BusinessType? type = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(type));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXSwapBestOffer>>(_baseClient.GetUrl("linear-swap-ex/market/bbo"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXKline>>> GetKlinesAsync(string contractCode, KlineInterval interval, int? limit = null, DateTime? from = null, DateTime? to = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) }
            };
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("from", DateTimeConverter.ConvertToSeconds(from));
            parameters.AddOptionalParameter("to", DateTimeConverter.ConvertToSeconds(to));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXKline>>(_baseClient.GetUrl("linear-swap-ex/market/history/kline"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMarketData>> GetMarketDataAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            return await _baseClient.SendHTXRequest<HTXMarketData>(_baseClient.GetUrl("linear-swap-ex/market/detail/merged"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXMarketData>>> GetMarketDatasAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXMarketData>>(_baseClient.GetUrl("linear-swap-ex/market/detail/batch_merged"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXLastTrade>> GetLastTradesAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var result = await _baseClient.SendHTXRequest<HTXLastTradeWrapper>(_baseClient.GetUrl("linear-swap-ex/market/trade"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As(result.Data?.Data?.First()!);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXTrade>>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "size", limit }
            };
            var result = await _baseClient.SendHTXRequest<IEnumerable<HTXTradeWrapper>>(_baseClient.GetUrl("/linear-swap-ex/market/history/trade"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IEnumerable<HTXTrade>>(result.Data?.SelectMany(d => d.Data)!);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSwapRiskInfo>>> GetSwapRiskInfoAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXSwapRiskInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_risk_info"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHTXRequest<HTXInsuranceInfo>(_baseClient.GetUrl("linear-swap-api/v1/swap_insurance_fund"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSwapAdjustFactorInfo>>> GetIsolatedMarginAdjustFactorInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXSwapAdjustFactorInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_adjustfactor"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossSwapAdjustFactorInfo>>> GetCrossMarginAdjustFactorInfoAsync(string? contractCode = null, string? asset = null, ContractType? type = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", asset);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(type));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXCrossSwapAdjustFactorInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_adjustfactor"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOpenInterestValue>> GetOpenInterestAsync(InterestPeriod period, Unit unit, string? contractCode = null, string? symbol = null, ContractType? type = null, int? limit = null, CancellationToken ct = default)
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
            return await _baseClient.SendHTXRequest<HTXOpenInterestValue>(_baseClient.GetUrl("/linear-swap-api/v1/swap_his_open_interest"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXTieredMarginInfo>>> GetIsolatedMarginTieredInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXTieredMarginInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_ladder_margin"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXTieredCrossMarginInfo>>> GetCrossTieredMarginInfoAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXTieredCrossMarginInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_ladder_margin"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXContractStatus>>> GetIsolatedStatusAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXContractStatus>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_api_state"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossMarginTransferStatus>>> GetCrossMarginTransferStatusAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXCrossMarginTransferStatus>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_transfer_state"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossMarginTradeStatus>>> GetCrossMarginTradeStatusAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXCrossMarginTradeStatus>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_trade_state"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXLiquidationOrderPage>> GetLiquidationOrdersAsync(int createDate, LiquidationTradeType tradeType, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "create_date", createDate },
                { "trade_type", EnumConverter.GetString(tradeType) },
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHTXRequest<HTXLiquidationOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_liquidation_orders"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

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
            return await _baseClient.SendHTXRequest<HTXSettlementPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_settlement_records"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection() {
                { "contract_code", contractCode }
            };
            return await _baseClient.SendHTXRequest<HTXFundingRate>(_baseClient.GetUrl("/linear-swap-api/v1/swap_funding_rate"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXFundingRate>>> GetFundingRatesAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXFundingRate>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_batch_funding_rate"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHTXRequest<HTXFundingRatePage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_historical_funding_rate"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXKline>>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            return await _baseClient.SendHTXRequest<IEnumerable<HTXKline>>(_baseClient.GetUrl("/index/market/history/linear_swap_premium_index_kline"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXKline>>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            return await _baseClient.SendHTXRequest<IEnumerable<HTXKline>>(_baseClient.GetUrl("/index/market/history/linear_swap_estimated_rate_kline"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXBasisData>>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            parameters.AddOptionalParameter("basis_price_type", basisPriceType);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXBasisData>>(_baseClient.GetUrl("/index/market/history/linear_swap_basis"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXEstimatedSettlementPrice>>> GetEstimatedSettlementPriceAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXEstimatedSettlementPrice>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_estimated_settlement_price"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }
    }
}
