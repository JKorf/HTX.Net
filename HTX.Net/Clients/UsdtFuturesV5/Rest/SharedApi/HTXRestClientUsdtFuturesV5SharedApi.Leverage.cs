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
        #region Get Leverage

        async Task<IExchangeCallResult<SharedLeverage>> IGetLeverage.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
            => await GetLeverageAsync(request, ct).ConfigureAwait(false);

        public SharedLeverageSettingMode LeverageSettingType => SharedLeverageSettingMode.PerSide;

        public GetLeverageOptions GetLeverageOptions { get; } = new GetLeverageOptions(_exchangeName, true)
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<GetLeverageRequest>.Required(x => x.Symbol)
            ],
            ExchangeParameterRules = [
                ExchangeParameterRule.Optional("MarginMode", "The margin mode", SharedMarginMode.Cross)
            ]
        };
        public async Task<HttpResult<SharedLeverage>> GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);
                        
            var marginMode =
                request.MarginMode != null
                    ? (request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated)
                    : GetProvidedMarginMode(request);
            var result = await _api.Account.GetLeverageAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                marginMode,
                request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Long ? FuturesPositionSide.Long : FuturesPositionSide.Short,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            var setting = result.Data.FirstOrDefault();
            if (setting == null)
                return HttpResult.Fail<SharedLeverage>(Exchange, new ServerError(ErrorType.Unknown, "No leverage setting found for the specified symbol and position side."));

            return HttpResult.Ok(result, new SharedLeverage(setting.LeverageRate));
        }

        #endregion

        #region Set Leverage

        async Task<IExchangeCallResult<SharedLeverage>> ISetLeverage.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
            => await SetLeverageAsync(request, ct).ConfigureAwait(false);

        public SetLeverageOptions SetLeverageOptions { get; } = new SetLeverageOptions(_exchangeName)
        {
            ExchangeParameterRules = [
                ExchangeParameterRule.Optional("MarginMode", "The margin mode", SharedMarginMode.Cross)
            ]
        };
        public async Task<HttpResult<SharedLeverage>> SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = SetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var marginMode =
                request.MarginMode != null
                    ? (request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated)
                    : GetProvidedMarginMode(request) ?? MarginMode.Cross;
            var result = await _api.Account.SetLeverageAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                marginMode,
                (int)request.Leverage,
                positionSide: request.Side == null ? null : request.Side == SharedPositionSide.Long ? FuturesPositionSide.Long : FuturesPositionSide.Short,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(request.Leverage));
        }

        #endregion

        #region Get Leverage Tiers

        async Task<IExchangeCallResult<SharedLeverageTier[]>> IGetLeverageTiers.GetLeverageTiersAsync(GetLeverageTiersRequest request, CancellationToken ct)
            => await GetLeverageTiersAsync(request, ct).ConfigureAwait(false);

        public GetLeverageTiersOptions GetLeverageTiersOptions { get; } = new GetLeverageTiersOptions(_exchangeName, true)
        {
            ExchangeParameterRules = [
                ExchangeParameterRule.Optional("MarginMode", "The margin mode", SharedMarginMode.Cross)
            ]
        };
        public async Task<HttpResult<SharedLeverageTier[]>> GetLeverageTiersAsync(GetLeverageTiersRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageTiersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverageTier[]>(Exchange, validationError);

            var marginMode = GetProvidedMarginMode(request);
            var result = await _api.Account.GetRiskLimitTiersAsync(
                contractCode: request.Symbol!.GetSymbol(FormatSymbol),
                marginMode,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverageTier[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x =>
                new SharedLeverageTier(request.Symbol, x.ContractCode, x.Tier, x.VolumeUnit, x.MinVolume, x.MaxVolume, x.MaintenanceMarginRate, x.MaxLeverage)
            ).ToArray());
        }

        #endregion
    }
}
