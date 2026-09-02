using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXRestClientSpotSharedApi
    {
        #region Withdrawal client
        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, pageRequest, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, true, true, true, 50);
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            int limit = request.Limit ?? 50;
            var direction = request.Direction ?? DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetWithdrawDepositHistoryAsync(
                WithdrawDepositType.Withdraw,
                request.Asset,
                from: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                size: pageParams.Limit,
                direction: FilterDirection.Next,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            // Get next token
            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromId(result.Data.Min(x => x.Id) - 1),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedWithdrawal(
                            x.Asset!.ToUpperInvariant(),
                            x.Address!,
                            x.Quantity,
                            x.Status == WithdrawDepositStatus.Confirmed,
                            x.CreateTime,
                            GetWithdrawalStatus(x))
                        {
                            Id = x.Id.ToString(),
                            Network = x.Network,
                            TransactionId = x.TransactionHash,
                            Tag = x.AddressTag,
                            Fee = x.Fee
                        })
                    .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus GetWithdrawalStatus(HTXWithdrawDeposit x)
        {
            if (x.Status == WithdrawDepositStatus.Canceled
                || x.Status == WithdrawDepositStatus.ConfirmError
                || x.Status == WithdrawDepositStatus.Failed
                || x.Status == WithdrawDepositStatus.Reject
                || x.Status == WithdrawDepositStatus.Repealed)
            {
                return SharedTransferStatus.Failed;
            }

            if (x.Status == WithdrawDepositStatus.Safe)
                return SharedTransferStatus.Completed;

            if (x.Status == WithdrawDepositStatus.Confirming
                || x.Status == WithdrawDepositStatus.Orphan
                || x.Status == WithdrawDepositStatus.Pass
                || x.Status == WithdrawDepositStatus.PreTransfer
                || x.Status == WithdrawDepositStatus.Reexamine
                || x.Status == WithdrawDepositStatus.Submitted
                || x.Status == WithdrawDepositStatus.WaitingTinyAmount
                || x.Status == WithdrawDepositStatus.WalletTransfer)
            {
                return SharedTransferStatus.InProgress;
            }

            return SharedTransferStatus.Unknown;
        }

        #endregion

        #region Withdraw client

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription(["WithdrawFee", "fee"], typeof(decimal), "Fee to use for the withdrawal", 0.001m)
            }
        };

        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var fee = request.GetParamValue<decimal?>(Exchange, "withdrawFee", "fee");

            // Get data
            var withdrawal = await _api.Account.WithdrawAsync(
                asset: request.Asset,
                fee: fee!.Value,
                address: request.Address,
                quantity: request.Quantity,
                network: request.Network,
                addressTag: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.ToString()));
        }

        #endregion
    }
}
