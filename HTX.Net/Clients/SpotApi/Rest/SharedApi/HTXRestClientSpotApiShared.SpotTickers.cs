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
        #region Get All Spot Tickers

        async Task<ICallResult<SharedSpotTicker[]>> IGetAllSpotTickers.GetAllSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
            => await GetAllSpotTickersAsync(request, ct).ConfigureAwait(false);

        Task<HttpResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllSpotTickersAsync(request, ct);
        GetAllSpotTickersOptions ISpotTickerRestClient.GetSpotTickersOptions => GetAllSpotTickersOptions;

        public GetAllSpotTickersOptions GetAllSpotTickersOptions { get; } = new GetAllSpotTickersOptions(_exchangeName);
        public async Task<HttpResult<SharedSpotTicker[]>> GetAllSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllSpotTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(
                ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Ticks.Select(x => new SharedSpotTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.ClosePrice,
                x.HighPrice,
                x.LowPrice,
                new SharedOrderQuantity(x.Volume, x.QuoteVolume), 
                x.OpenPrice == null || x.OpenPrice == 0 ? null : Math.Round(((x.ClosePrice ?? 0) / x.OpenPrice.Value) * 100 - 100, 2))
            {
            }).ToArray());
        }

        #endregion
        #region Get Spot Ticker

        async Task<ICallResult<SharedSpotTicker>> IGetSpotTicker.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
            => await GetSpotTickerAsync(request, ct).ConfigureAwait(false);

        public GetSpotTickerOptions GetSpotTickerOptions { get; } = new GetSpotTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedSpotTicker>> GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetSpotTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetTickerAsync(
                symbol,
                ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker>(result);

            return HttpResult.Ok(result, new SharedSpotTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol),
                symbol,
                result.Data.ClosePrice,
                result.Data.HighPrice,
                result.Data.LowPrice,
                new SharedOrderQuantity(result.Data.Volume, result.Data.QuoteVolume),
                result.Data.OpenPrice == null || result.Data.OpenPrice == 0 ? null : Math.Round((result.Data.ClosePrice ?? 0) / result.Data.OpenPrice.Value * 100 - 100, 2))
            {
            });
        }

        #endregion
    }
}
