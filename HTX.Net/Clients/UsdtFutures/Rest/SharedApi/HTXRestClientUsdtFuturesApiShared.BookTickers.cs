using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXRestClientUsdtFuturesSharedApi
    {
        #region Get Book Ticker

        async Task<ICallResult<SharedBookTicker>> IGetBookTicker.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
            => await GetBookTickerAsync(request, ct).ConfigureAwait(false);

        public GetBookTickerOptions GetBookTickerOptions { get; } = new GetBookTickerOptions(_exchangeName, false);
        public async Task<HttpResult<SharedBookTicker>> GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = await _api.ExchangeData.GetBookTickerAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            var bookTicker = resultTicker.Data.Single();
            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol),
                symbol,
                bookTicker.Ask.Price,
                new SharedOrderQuantity(contractQuantity: bookTicker.Ask.Quantity),
                bookTicker.Bid.Price,
                new SharedOrderQuantity(contractQuantity: bookTicker.Bid.Quantity)));
        }

        #endregion
    }
}
