using CryptoExchange.Net.Objects;
using CryptoExchange.Net;
using Huobi.Net.Clients.FuturesApi;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    public class HuobiClientUsdtMarginSwapApiAccount
    {
        private readonly HuobiClientUsdtMarginSwapApi _baseClient;

        internal HuobiClientUsdtMarginSwapApiAccount(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        public async Task<WebCallResult<IEnumerable<HuobiAssetValue>>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("valuation_asset", asset);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiAssetValue>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_balance_valuation"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
    }
}
