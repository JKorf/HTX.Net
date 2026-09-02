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
        #region Transfer client

        public TransferOptions TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Spot,
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryInverseFutures
            ])
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "The settle asset for futures transfer", "usdt")
            }
        };
        public async Task<HttpResult<SharedId>> TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var fromType = GetTransferType(request.FromAccountType);
            var toType = GetTransferType(request.ToAccountType);
            if (fromType == null || toType == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));
            if(request.FromSymbol != null && request.ToSymbol != null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From Symbol", "Both fromSymbol and toSymbol cannot be set at the same time"));

            // Get data
            var transfer = await _api.Account.TransferAsync(
                fromType.Value,
                toType.Value,
                request.Asset,
                request.Quantity,
                request.ToSymbol ?? request.FromSymbol ?? "USDT",
                ct: ct).ConfigureAwait(false);
            if (!transfer.Success)
                return HttpResult.Fail<SharedId>(transfer);

            return HttpResult.Ok(transfer, new SharedId(transfer.Data.ToString()));
        }

        private TransferAccount? GetTransferType(SharedAccountType type)
        {
            if (type == SharedAccountType.Spot) return TransferAccount.Spot;
            if (type == SharedAccountType.DeliveryLinearFutures || type == SharedAccountType.PerpetualLinearFutures) return TransferAccount.LinearSwap;
            if (type == SharedAccountType.PerpetualInverseFutures || type == SharedAccountType.DeliveryInverseFutures) return TransferAccount.Swap;
            return null;
        }

        #endregion
    }
}
