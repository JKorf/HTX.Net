using CryptoExchange.Net.Objects;
using CryptoExchange.Net;
using Huobi.Net.Clients.FuturesApi;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Linq;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    public class HuobiClientUsdtMarginSwapApiAccount
    {
        private readonly HuobiClientUsdtMarginSwapApi _baseClient;

        internal HuobiClientUsdtMarginSwapApiAccount(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        public async Task<WebCallResult<IEnumerable<HuobiAssetValue>>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("valuation_asset", asset);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiAssetValue>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_balance_valuation"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginAccountInfo>>> GetIsolatedMarginAccountInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginAccountInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_account_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiCrossMarginAccountInfo>>> GetCrossMarginAccountInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossMarginAccountInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_account_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiPosition>>> GetIsolatedMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiPosition>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiPosition>>> GetCrossMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiPosition>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginAssetsAndPositions>>> GetIsolatedMarginAssetAndPositionsAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginAssetsAndPositions>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_account_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiCrossMarginAssetsAndPositions>> GetCrossMarginAssetAndPositionsAsync(string marginAccount, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "margin_account", marginAccount }
            };
            return await _baseClient.SendHuobiRequest<HuobiCrossMarginAssetsAndPositions>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_account_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiSubAccountResult>> SetSubAccountsTradingPermissionsAsync(IEnumerable<string> subAccountUids, bool enabled, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "sub_uid", string.Join(",", subAccountUids) },
                { "sub_auth", enabled ? "1": "0" }
            };
            return await _baseClient.SendHuobiRequest<HuobiSubAccountResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_sub_auth"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>> GetIsolatedMarginSubAccountsAssetsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_sub_account_list"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>> GetCrossMarginSubAccountsAssetsAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_sub_account_list"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiFinancialRecordsPage>> GetFinancialRecordsAsync(string marginAccount, string? contractCode = null, IEnumerable<FinancialRecordType>? types = null, DateTime? createDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "margin_account", marginAccount }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("type", types == null ? null : string.Join(",", types.Select(EnumConverter.GetString)));
            parameters.AddOptionalParameter("create_date", DateTimeConverter.ConvertToMilliseconds(createDate));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiFinancialRecordsPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_financial_record"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
    }
}
