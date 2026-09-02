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
        #region Deposit client

        public GetDepositAddressesOptions GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedDepositAddress[]>> GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await _api.Account.GetDepositAddressesAsync(request.Asset).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, depositAddresses.Data.Where(x => request.Network == null ? true : x.Network == request.Network).Select(x => new SharedDepositAddress(x.Asset.ToUpperInvariant(), x.Address)
            {
                Network = x.Network,
                TagOrMemo = x.AddressTag
            }
            ).ToArray());
        }
        Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetDepositHistoryAsync(request, pageRequest, ct);
        GetDepositHistoryOptions IDepositRestClient.GetDepositsOptions => GetDepositHistoryOptions;

        public GetDepositHistoryOptions GetDepositHistoryOptions { get; } = new GetDepositHistoryOptions(_exchangeName, true, true, true, 50);
        public async Task<HttpResult<SharedDeposit[]>> GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetDepositHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            int limit = request.Limit ?? 50;
            var direction = request.Direction ?? DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetWithdrawDepositHistoryAsync(
                WithdrawDepositType.Deposit,
                request.Asset,
                from: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                size: pageParams.Limit,
                direction: FilterDirection.Next,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedDeposit[]>(result);

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
                        new SharedDeposit(
                            x.Asset!.ToUpperInvariant(),
                            x.Quantity, 
                            x.Status == WithdrawDepositStatus.Safe,
                            x.CreateTime,
                            ParseTransferStatus(x.Status))
                        {
                            Id = x.Id.ToString(),
                            Network = x.Network,
                            TransactionId = x.TransactionHash,
                            Tag = x.AddressTag
                        })
                    .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus ParseTransferStatus(WithdrawDepositStatus status)
        {
            if (status == WithdrawDepositStatus.Safe)
                return SharedTransferStatus.Completed;
            if (status == WithdrawDepositStatus.Repealed
                || status == WithdrawDepositStatus.ConfirmError
                || status == WithdrawDepositStatus.WalletReject
                || status == WithdrawDepositStatus.Reject
                || status == WithdrawDepositStatus.Canceled
                || status == WithdrawDepositStatus.Failed) 
            {
                return SharedTransferStatus.Failed;
            }

            if (status == WithdrawDepositStatus.Confirming
                || status == WithdrawDepositStatus.Verifying
                || status == WithdrawDepositStatus.Submitted
                || status == WithdrawDepositStatus.WaitingTinyAmount
                || status == WithdrawDepositStatus.WalletTransfer)
            {
                return SharedTransferStatus.InProgress;
            }

            return SharedTransferStatus.InProgress;
        }

        #endregion
    }
}
