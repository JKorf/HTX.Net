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
        #region Get Balances

        async Task<ICallResult<SharedBalance[]>> IGetBalances.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
            => await GetBalancesAsync(request, ct).ConfigureAwait(false);

        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Spot)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(long), "Account id of the user", 123123123L)
            }
        };

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            var accountId = ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "AccountId");
            var result = await _api.Account.GetBalancesAsync(accountId, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedBalance[]>(result);

            var resp = new List<SharedBalance>();
            foreach(var balance in result.Data)
            {
                var asset = resp.SingleOrDefault(x => x.Asset == balance.Asset.ToUpperInvariant());
                if (asset == null)
                {
                    asset = new SharedBalance(
                        SupportedTradingModes, 
                        balance.Asset.ToUpperInvariant(),
                        0,
                        0);
                    resp.Add(asset);
                }

                if (balance.Type == Enums.BalanceType.Trade)
                {
                    asset.Available = balance.Balance;
                    asset.Total += balance.Balance;
                }
                else if (balance.Type == Enums.BalanceType.Frozen)
                {
                    asset.Total += balance.Balance;
                }
            }

            return HttpResult.Ok(result, resp.ToArray());
        }

        #endregion
    }
}
