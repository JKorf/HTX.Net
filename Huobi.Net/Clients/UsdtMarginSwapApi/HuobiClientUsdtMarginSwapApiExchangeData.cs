using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Huobi.Net.Clients.FuturesApi;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    public class HuobiClientUsdtMarginSwapApiExchangeData
    {
        private readonly HuobiClientUsdtMarginSwapApi _baseClient;

        internal HuobiClientUsdtMarginSwapApiExchangeData(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
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

        public async Task<WebCallResult<IEnumerable<HuobiSwapBestOffer>>> GetKlinesAsync(string? contractCode = null, KlineInterval? type = null, int? limit = null, DateTime? from = null, DateTime? to = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(type));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSwapBestOffer>>(_baseClient.GetUrl("linear-swap-ex/market/history/kline"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }
    }
}
