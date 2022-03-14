using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Net.Objects.Models;
using CryptoExchange.Net.Converters;
using Huobi.Net.Interfaces.Clients.SpotApi;

namespace Huobi.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class HuobiClientSpotApiAccount : IHuobiClientSpotApiAccount
    {
        private const string GetUserId = "user/uid";
        private const string GetSubAccountUsers = "sub-user/user-list";
        private const string GetSubUserAccounts = "sub-user/account-list";

        private const string GetAccountsEndpoint = "account/accounts";
        private const string GetAssetValuationEndpoint = "account/asset-valuation";
        private const string TransferAssetValuationEndpoint = "account/transfer";
        private const string GetBalancesEndpoint = "account/accounts/{}/balance";
        private const string GetAccountHistoryEndpoint = "account/history";
        private const string GetLedgerEndpoint = "account/ledger";

        private const string GetSubAccountBalancesEndpoint = "account/accounts/{}";
        private const string TransferWithSubAccountEndpoint = "subuser/transfer";

        private const string QueryDepositAddressEndpoint = "account/deposit/address";
        private const string PlaceWithdrawEndpoint = "dw/withdraw/api/create";
        private const string QueryWithdrawDepositEndpoint = "query/deposit-withdraw";

        private readonly HuobiClientSpotApi _baseClient;

        internal HuobiClientSpotApiAccount(HuobiClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> GetUserIdAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<long>(_baseClient.GetUrl(GetUserId, "2"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUser>>> GetSubAccountUsersAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUser>>(_baseClient.GetUrl(GetSubAccountUsers, "2"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiSubUserAccounts>> GetSubUserAccountsAsync(long subUserId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "subUid", subUserId.ToString(CultureInfo.InvariantCulture)}
            };

            return await _baseClient.SendHuobiRequest<HuobiSubUserAccounts>(_baseClient.GetUrl(GetSubUserAccounts, "2"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiAccount>>> GetAccountsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiAccount>>(_baseClient.GetUrl(GetAccountsEndpoint, "1"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiBalance>>> GetBalancesAsync(long accountId, CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiRequest<HuobiAccountBalances>(_baseClient.GetUrl(GetBalancesEndpoint.FillPathParameters(accountId.ToString(CultureInfo.InvariantCulture)), "1"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
            if (!result)
                return result.AsError<IEnumerable<HuobiBalance>>(result.Error!);

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiAccountValuation>> GetAssetValuationAsync(AccountType accountType, string? valuationCurrency = null, long? subUserId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "accountType", JsonConvert.SerializeObject(accountType, new AccountTypeConverter(false))}
            };
            parameters.AddOptionalParameter("valuationCurrency", valuationCurrency);
            parameters.AddOptionalParameter("subUid", subUserId);

            return await _baseClient.SendHuobiV2Request<HuobiAccountValuation>(_baseClient.GetUrl(GetAssetValuationEndpoint, "2"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiTransactionResult>> TransferAssetAsync(long fromUserId, AccountType fromAccountType, long fromAccountId,
            long toUserId, AccountType toAccountType, long toAccountId, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "from-account-id", fromAccountId.ToString(CultureInfo.InvariantCulture)},
                { "from-user", fromUserId.ToString(CultureInfo.InvariantCulture)},
                { "from-account-type", JsonConvert.SerializeObject(fromAccountType, new AccountTypeConverter(false))},

                { "to-account-id", toAccountId.ToString(CultureInfo.InvariantCulture)},
                { "to-user", toUserId.ToString(CultureInfo.InvariantCulture)},
                { "to-account-type", JsonConvert.SerializeObject(toAccountType, new AccountTypeConverter(false))},

                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };

            return await _baseClient.SendHuobiRequest<HuobiTransactionResult>(_baseClient.GetUrl(TransferAssetValuationEndpoint, "1"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiAccountHistory>>> GetAccountHistoryAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? size = null, CancellationToken ct = default)
        {
            size?.ValidateIntBetween(nameof(size), 1, 500);

            var transactionTypeConverter = new TransactionTypeConverter(false);
            var parameters = new Dictionary<string, object>
            {
                { "account-id", accountId }
            };
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("transact-types", transactionTypes == null ? null : string.Join(",", transactionTypes.Select(s => JsonConvert.SerializeObject(s, transactionTypeConverter))));
            parameters.AddOptionalParameter("start-time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end-time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("sort", sort == null ? null : JsonConvert.SerializeObject(sort, new SortingTypeConverter(false)));
            parameters.AddOptionalParameter("size", size);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiAccountHistory>>(_baseClient.GetUrl(GetAccountHistoryEndpoint, "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiLedgerEntry>>> GetAccountLedgerAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? size = null, long? fromId = null, CancellationToken ct = default)
        {
            size?.ValidateIntBetween(nameof(size), 1, 500);

            var transactionTypeConverter = new TransactionTypeConverter(false);
            var parameters = new Dictionary<string, object>
            {
                { "accountId", accountId }
            };
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("transactTypes", transactionTypes == null ? null : string.Join(",", transactionTypes.Select(s => JsonConvert.SerializeObject(s, transactionTypeConverter))));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("sort", sort == null ? null : JsonConvert.SerializeObject(sort, new SortingTypeConverter(false)));
            parameters.AddOptionalParameter("limit", size);
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiLedgerEntry>>(_baseClient.GetUrl(GetLedgerEndpoint, "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiBalance>>> GetSubAccountBalancesAsync(long subAccountId, CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiAccountBalances>>(_baseClient.GetUrl(GetSubAccountBalancesEndpoint.FillPathParameters(subAccountId.ToString(CultureInfo.InvariantCulture)), "1"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
            if (!result)
                return result.AsError<IEnumerable<HuobiBalance>>(result.Error!);

            return result.As(result.Data.First().Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferWithSubAccountAsync(long subAccountId, string asset, decimal quantity, TransferType transferType, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new Dictionary<string, object>
            {
                { "sub-uid", subAccountId },
                { "currency", asset },
                { "amount", quantity },
                { "type", JsonConvert.SerializeObject(transferType, new TransferTypeConverter(false)) }
            };

            return await _baseClient.SendHuobiRequest<long>(_baseClient.GetUrl(TransferWithSubAccountEndpoint, "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }



        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiDepositAddress>>> GetDepositAddressesAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>() { { "currency", asset } };
            return await _baseClient.SendHuobiV2Request<IEnumerable<HuobiDepositAddress>>(_baseClient.GetUrl(QueryDepositAddressEndpoint, "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> WithdrawAsync(string address, string asset, decimal quantity, decimal fee, string? network = null, string? addressTag = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "address", address },
                { "currency", asset },
                { "amount", quantity },
                { "fee", fee },
            };

            parameters.AddOptionalParameter("chain", network);
            parameters.AddOptionalParameter("addr-tag", addressTag);
            return await _baseClient.SendHuobiRequest<long>(_baseClient.GetUrl(PlaceWithdrawEndpoint, "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiWithdrawDeposit>>> GetWithdrawDepositAsync(WithdrawDepositType type, string? asset = null, int? from = null, int? size = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "type", JsonConvert.SerializeObject(type, new WithdrawDepositTypeConverter(false))  },
            };

            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("from", from?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("direct", direction == null ? null : JsonConvert.SerializeObject(direction, new FilterDirectionConverter(false)));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiWithdrawDeposit>>(_baseClient.GetUrl(QueryWithdrawDepositEndpoint, "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
    }
}
