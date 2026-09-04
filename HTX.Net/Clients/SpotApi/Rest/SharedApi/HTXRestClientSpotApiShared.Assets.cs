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
        #region Get Asset

        async Task<ICallResult<SharedAsset>> IGetAsset.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
            => await GetAssetAsync(request, ct).ConfigureAwait(false);

        public GetAssetOptions GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, false);
        public async Task<HttpResult<SharedAsset>> GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var assets = await _api.ExchangeData.GetAssetsAndNetworksAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset>(assets);

            var asset = assets.Data.SingleOrDefault();
            if (asset == null)
                return HttpResult.Fail<SharedAsset>(assets, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, "Asset not found")));

            return HttpResult.Ok(assets, new SharedAsset(asset.Asset.ToUpperInvariant())
            {
                Networks = asset.Networks.Select(x => new SharedAssetNetwork(x.Network)
                {
                    FullName = x.DisplayName,
                    MinConfirmations = x.NumOfConfirmations,
                    DepositEnabled = x.DepositStatus == NetworkStatus.Allowed,
                    MinWithdrawQuantity = x.MinWithdrawQuantity,
                    MaxWithdrawQuantity = x.MaxWithdrawQuantity,
                    WithdrawEnabled = x.WithdrawStatus == NetworkStatus.Allowed,
                    WithdrawFee = x.FixedWithdrawFee
                }).ToArray()
            });
        }

        #endregion

        #region Get All Assets

        async Task<ICallResult<SharedAsset[]>> IGetAllAssets.GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => await GetAllAssetsAsync(request, ct).ConfigureAwait(false);

        Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => GetAllAssetsAsync(request, ct);
        GetAllAssetsOptions IAssetsRestClient.GetAssetsOptions => GetAllAssetsOptions;

        public GetAllAssetsOptions GetAllAssetsOptions { get; } = new GetAllAssetsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedAsset[]>> GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = GetAllAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var assets = await _api.ExchangeData.GetAssetsAndNetworksAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset[]>(assets);

            return HttpResult.Ok(assets, assets.Data.Select(x => new SharedAsset(x.Asset.ToUpperInvariant())
            {
                Networks = x.Networks.Select(x => new SharedAssetNetwork(x.Network)
                {
                    FullName = x.DisplayName,
                    MinConfirmations = x.NumOfConfirmations,
                    DepositEnabled = x.DepositStatus == NetworkStatus.Allowed,
                    MinWithdrawQuantity = x.MinWithdrawQuantity,
                    MaxWithdrawQuantity = x.MaxWithdrawQuantity,
                    WithdrawEnabled = x.WithdrawStatus == NetworkStatus.Allowed,
                    WithdrawFee = x.FixedWithdrawFee
                }).ToArray()
            }).ToArray());
        }

        #endregion
    }
}
