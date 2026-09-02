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
        #region Position Mode client

        public SharedPositionModeSelection PositionModeSettingType => SharedPositionModeSelection.PerAccount;
        public GetPositionModeOptions GetPositionModeOptions { get; } = new GetPositionModeOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "Margin mode to get position mode for", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedPositionModeResult>> GetPositionModeAsync(GetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = GetPositionModeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionModeResult>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.Account.GetCrossMarginPositionModeAsync("USDT", ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPositionModeResult>(result);

                return HttpResult.Ok(result, new SharedPositionModeResult(result.Data.PositionMode == PositionMode.DualSide ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));
            }
            else
            {
                if (request.Symbol == null)
                    return HttpResult.Fail<SharedPositionModeResult>(Exchange, ArgumentError.Missing(nameof(GetPositionModeRequest.Symbol), "Symbol parameter required for isolated mode"));

                var result = await _api.Account.GetIsolatedMarginPositionModeAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPositionModeResult>(result);

                return HttpResult.Ok(result, new SharedPositionModeResult(result.Data.PositionMode == PositionMode.DualSide ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));
            }
        }

        public SetPositionModeOptions SetPositionModeOptions { get; } = new SetPositionModeOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "Margin mode to get position mode for", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedPositionModeResult>> SetPositionModeAsync(SetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = SetPositionModeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionModeResult>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.Account.SetCrossMarginPositionModeAsync("USDT", request.PositionMode == SharedPositionMode.HedgeMode ? PositionMode.DualSide : PositionMode.SingleSide, ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPositionModeResult>(result);

                return HttpResult.Ok(result, new SharedPositionModeResult(request.PositionMode));
            }
            else
            {
                if (request.Symbol == null)
                    return HttpResult.Fail<SharedPositionModeResult>(Exchange, ArgumentError.Missing(nameof(SetPositionModeRequest.Symbol), "Symbol parameter required for isolated mode"));

                var result = await _api.Account.SetIsolatedMarginPositionModeAsync(request.Symbol.GetSymbol(FormatSymbol), request.PositionMode == SharedPositionMode.HedgeMode ? PositionMode.DualSide : PositionMode.SingleSide, ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPositionModeResult>(result);

                return HttpResult.Ok(result, new SharedPositionModeResult(request.PositionMode));
            }
        }
        #endregion
    }
}
