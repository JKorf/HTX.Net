using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Huobi.Net.Clients.FuturesApi;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    public class HuobiClientUsdtMarginSwapApiExchangeData : IHuobiClientUsdtMarginSwapApiExchangeData
    {
        private readonly HuobiClientUsdtMarginSwapApi _baseClient;

        internal HuobiClientUsdtMarginSwapApiExchangeData(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendTimestampRequestAsync(_baseClient.GetUrl("api/v1/timestamp"), HttpMethod.Get, ct, ignoreRatelimit: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiContractInfo>>> GetContractInfoAsync(string? contractCode = null, MarginMode? supportMarginMode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("support_margin_mode", supportMarginMode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiContractInfo>>(_baseClient.GetUrl("linear-swap-api/v1/swap_contract_info"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiSwapIndex>>> GetSwapIndexPriceAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSwapIndex>>(_baseClient.GetUrl("linear-swap-api/v1/swap_index"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiPriceLimitation>>> GetSwapPriceLimitationAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiPriceLimitation>>(_baseClient.GetUrl("linear-swap-api/v1/swap_price_limit"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiOpenInterest>>> GetSwapOpenInterestAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiOpenInterest>>(_baseClient.GetUrl("linear-swap-api/v1/swap_open_interest"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiOrderBook>> GetOrderBookAsync(string contractCode, string step, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "type", step },
            };
            return await _baseClient.SendHuobiRequest<HuobiOrderBook>(_baseClient.GetUrl("linear-swap-ex/market/depth"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiSwapBestOffer>>> GetBestOfferAsync(string? contractCode = null, BusinessType? type = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(type));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSwapBestOffer>>(_baseClient.GetUrl("linear-swap-ex/market/bbo"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string contractCode, KlineInterval interval, int? limit = null, DateTime? from = null, DateTime? to = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) }
            };
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("from", DateTimeConverter.ConvertToSeconds(from));
            parameters.AddOptionalParameter("to", DateTimeConverter.ConvertToSeconds(to));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiKline>>(_baseClient.GetUrl("linear-swap-ex/market/history/kline"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiMarketData>> GetMarketDataAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            return await _baseClient.SendHuobiRequest<HuobiMarketData>(_baseClient.GetUrl("linear-swap-ex/market/detail/merged"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiMarketData>>> GetMarketDatasAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiMarketData>>(_baseClient.GetUrl("linear-swap-ex/market/detail/batch_merged"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiLastTrade>> GetLastTradesAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var result = await _baseClient.SendHuobiRequest<HuobiLastTradeWrapper>(_baseClient.GetUrl("linear-swap-ex/market/trade"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As(result.Data?.Data?.First()!);
        }

        public async Task<WebCallResult<IEnumerable<HuobiTrade>>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "size", limit }
            };
            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiTradeWrapper>>(_baseClient.GetUrl("/linear-swap-ex/market/history/trade"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IEnumerable<HuobiTrade>>(result.Data?.SelectMany(d => d.Data)!);
        }

        public async Task<WebCallResult<IEnumerable<HuobiSwapRiskInfo>>> GetSwapRiskInfoAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSwapRiskInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_risk_info"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiInsuranceInfo>(_baseClient.GetUrl("linear-swap-api/v1/swap_insurance_fund"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiSwapAdjustFactorInfo>>> GetIsolatedAdjustFactorInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSwapAdjustFactorInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_adjustfactor"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiCrossSwapAdjustFactorInfo>>> GetCrossAdjustFactorInfoAsync(string? contractCode = null, string? asset = null, ContractType? type = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", asset);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(type));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossSwapAdjustFactorInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_adjustfactor"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiOpenInterestValue>> GetOpenInterestAsync(InterestPeriod period, Unit unit, string? contractCode = null, string? symbol = null, ContractType? type = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "period", EnumConverter.GetString(period) },
                { "amount_type", EnumConverter.GetString(unit) },
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(type));
            return await _baseClient.SendHuobiRequest<HuobiOpenInterestValue>(_baseClient.GetUrl("/linear-swap-api/v1/swap_his_open_interest"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiTieredMarginInfo>>> GetIsolatedTieredMarginInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiTieredMarginInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_ladder_margin"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiTieredCrossMarginInfo>>> GetCrossTieredMarginInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiTieredCrossMarginInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_ladder_margin"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiContractStatus>>> GetIsolatedStatusAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiContractStatus>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_api_state"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiCrossMarginTransferStatus>>> GetCrossMarginTransferStatusAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossMarginTransferStatus>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_transfer_state"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiCrossMarginTradeStatus>>> GetCrossMarginTradeStatusAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossMarginTradeStatus>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_trade_state"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiLiquidationOrderPage>> GetLiquidationOrdersAsync(int createDate, LiquidationTradeType tradeType, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "create_date", createDate },
                { "trade_type", EnumConverter.GetString(tradeType) },
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiLiquidationOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_liquidation_orders"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiSettlementPage>> GetHistoricalSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("start_time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end_time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiSettlementPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_settlement_records"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>() {
                { "contract_code", contractCode }
            };
            return await _baseClient.SendHuobiRequest<HuobiFundingRate>(_baseClient.GetUrl("/linear-swap-api/v1/swap_funding_rate"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiFundingRate>>> GetFundingRatesAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiFundingRate>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_batch_funding_rate"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiFundingRatePage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_historical_funding_rate"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiKline>>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiKline>>(_baseClient.GetUrl("/index/market/history/linear_swap_premium_index_kline"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiKline>>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiKline>>(_baseClient.GetUrl("/index/market/history/linear_swap_estimated_rate_kline"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiBasisData>>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            parameters.AddOptionalParameter("basis_price_type", basisPriceType);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiBasisData>>(_baseClient.GetUrl("/index/market/history/linear_swap_basis"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiEstimatedSettlementPrice>>> GetEstimatedSettlementPriceAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiEstimatedSettlementPrice>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_estimated_settlement_price"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }
    }
}
