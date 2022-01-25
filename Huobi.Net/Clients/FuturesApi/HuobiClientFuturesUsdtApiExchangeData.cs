using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Huobi.Net.Interfaces.Clients.FuturesApi;
using Huobi.Net.Objects.Models.Futures;

namespace Huobi.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class HuobiClientFuturesUsdtApiExchangeData : IHuobiClientFuturesUsdtApiExchangeData
    {
        private const string Api = "api";
        private const string UsdtApi = "linear-swap-api";
        private const string ServerTimeEndpoint = "timestamp";
        private const string CommonSymbolsEndpoint = "swap_contract_info";

        private readonly HuobiClientFuturesUsdtApi _baseClient;

        internal HuobiClientFuturesUsdtApiExchangeData(HuobiClientFuturesUsdtApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiFuturesSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiFuturesSymbol>>(_baseClient.GetUrl(CommonSymbolsEndpoint, UsdtApi, "1"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiTimestampRequest<DateTime>(_baseClient.GetUrl(ServerTimeEndpoint, Api, "1"), HttpMethod.Get, ct)
                .ConfigureAwait(false);
            if (!result)
                return result.AsError<DateTime>(result.Error!);
            var time = result.Data.Item2;
            return result.As(time);
        }
    }
}
