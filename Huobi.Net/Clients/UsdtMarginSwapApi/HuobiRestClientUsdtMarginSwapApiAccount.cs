using CryptoExchange.Net.Objects;
using CryptoExchange.Net;
using Huobi.Net.Clients.FuturesApi;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Linq;
using System.Globalization;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    /// <inheritdoc />
    public class HuobiRestClientUsdtMarginSwapApiAccount : IHuobiRestClientUsdtMarginSwapApiAccount
    {
        private readonly HuobiRestClientUsdtMarginSwapApi _baseClient;

        internal HuobiRestClientUsdtMarginSwapApiAccount(HuobiRestClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiAssetValue>>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("valuation_asset", asset);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiAssetValue>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_balance_valuation"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginAccountInfo>>> GetIsolatedMarginAccountInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginAccountInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_account_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiCrossMarginAccountInfo>>> GetCrossMarginAccountInfoAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossMarginAccountInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_account_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiPosition>>> GetIsolatedMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiPosition>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiPosition>>> GetCrossMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiPosition>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginAssetsAndPositions>>> GetIsolatedMarginAssetAndPositionsAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginAssetsAndPositions>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_account_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiCrossMarginAssetsAndPositions>> GetCrossMarginAssetAndPositionsAsync(string marginAccount, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "margin_account", marginAccount }
            };
            return await _baseClient.SendHuobiRequest<HuobiCrossMarginAssetsAndPositions>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_account_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiSubAccountResult>> SetSubAccountsTradingPermissionsAsync(IEnumerable<string> subAccountUids, bool enabled, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "sub_uid", string.Join(",", subAccountUids) },
                { "sub_auth", enabled ? "1": "0" }
            };
            return await _baseClient.SendHuobiRequest<HuobiSubAccountResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_sub_auth"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>> GetIsolatedMarginSubAccountsAssetsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_sub_account_list"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>> GetCrossMarginSubAccountsAssetsAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_sub_account_list"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiIsolatedMarginUserSettlementRecordPage>> GetIsolatedMarginSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("start_time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end_time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiIsolatedMarginUserSettlementRecordPage>(_baseClient.GetUrl("linear-swap-api/v1/swap_user_settlement_records"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiCrossMarginUserSettlementRecordPage>> GetCrossMarginSettlementRecordsAsync(string marginAccount, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "margin_account", marginAccount }
            };
            parameters.AddOptionalParameter("start_time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end_time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiCrossMarginUserSettlementRecordPage>(_baseClient.GetUrl("linear-swap-api/v1/swap_cross_user_settlement_records"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginLeverageAvailable>>> GetIsolatedMarginAvailableLeverageAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginLeverageAvailable>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_available_level_rate"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiCrossMarginLeverageAvailable>>> GetCrossMarginAvailableLeverageAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossMarginLeverageAvailable>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_available_level_rate"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiTradingFee>>> GetTradingFeesAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiTradingFee>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_fee"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiOrderId>> TransferMasterSubAsync(string subUid, string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, MasterSubTransferType type, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "sub_uid", subUid },
                { "asset", asset },
                { "from_margin_account", fromMarginAccount },
                { "to_margin_account", toMarginAccount },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
                { "type", type == MasterSubTransferType.SubToMaster ? "sub_to_master": "master_to_sub" },
            };
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHuobiRequest<HuobiOrderId>(_baseClient.GetUrl("/linear-swap-api/v1/swap_master_sub_transfer"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiMasterSubTransfer>> GetMasterSubTransferRecordsAsync(string marginAccount, int daysInHistory, MasterSubTransferType? type = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "margin_account", marginAccount },
                { "create_date", daysInHistory }
            };
            parameters.AddOptionalParameter("transfer_type", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiMasterSubTransfer>(_baseClient.GetUrl("/linear-swap-api/v1/swap_master_sub_transfer_record"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiOrderId>> TransferMarginAccountsAsync(string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "asset", asset },
                { "from_margin_account", fromMarginAccount },
                { "to_margin_account", toMarginAccount },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHuobiRequest<HuobiOrderId>(_baseClient.GetUrl("/linear-swap-api/v1/swap_transfer_inner"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiPositionMode>> ModifyIsolatedMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "margin_account", marginAccount },
                { "position_mode", EnumConverter.GetString(positionMode) },
            };
            return await _baseClient.SendHuobiRequest<HuobiPositionMode>(_baseClient.GetUrl("/linear-swap-api/v1/swap_switch_position_mode"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiPositionMode>> ModifyCrossMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "margin_account", marginAccount },
                { "position_mode", EnumConverter.GetString(positionMode) },
            };
            return await _baseClient.SendHuobiRequest<HuobiPositionMode>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_switch_position_mode"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
    }
}
