using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    internal partial class HTXRestClientUsdtFuturesV5SharedApi
    {
        #region Get Funding Info

        async Task<IExchangeCallResult<SharedFundingInfo>> IGetFundingInfo.GetFundingInfoAsync(GetFundingInfoRequest request, CancellationToken ct)
            => await GetFundingInfoAsync(request, ct).ConfigureAwait(false);

        public GetFundingInfoOptions GetFundingInfoOptions { get; } = new GetFundingInfoOptions(_exchangeName, false, false, false, 0, false);

        public async Task<HttpResult<SharedFundingInfo>> GetFundingInfoAsync(GetFundingInfoRequest request, CancellationToken ct)
        {
            var validationError = GetFundingInfoOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingInfo>(Exchange, validationError);

            // Get data
            var result = await _api.ExchangeData.GetFundingRateAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingInfo>(result);

            var rate = result.Data.FirstOrDefault();
            if (rate == null)
                return HttpResult.Fail<SharedFundingInfo>(Exchange, new ServerError(ErrorType.Unknown, "No funding rate data returned"));

            // Return
            return HttpResult.Ok(result, new SharedFundingInfo(
                rate.FundingRate,
                rate.NextFundingTime,
                null));
        }

        #endregion

        #region Get Funding Rate History

        async Task<IExchangeCallResult<SharedFundingRate[]>> IGetFundingRateHistory.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetFundingRateHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetFundingRateHistoryOptions GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(_exchangeName, false, true, true, 100, false);

        public async Task<HttpResult<SharedFundingRate[]>> GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFundingRateHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingRate[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.ExchangeData.GetFundingRateHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingRate[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.FundingTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.FundingTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.FundingTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFundingRate(x.FundingRate, x.FundingTime))
                    .ToArray(), nextPageRequest);
        }

        #endregion
    }
}
