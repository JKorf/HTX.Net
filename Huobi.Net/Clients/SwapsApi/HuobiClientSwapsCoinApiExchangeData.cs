using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Huobi.Net.Interfaces.Clients.SwapsApi;
using Huobi.Net.Objects.Models.Swaps;

namespace Huobi.Net.Clients.SwapsApi
{
    /// <inheritdoc />
    public class HuobiClientSwapsCoinApiExchangeData : IHuobiClientSwapsCoinApiExchangeData
    {
        private const string Api = "api";
        private const string SwapApi = "swap-api";
        private const string ServerTimeEndpoint = "timestamp";
        private const string CommonSymbolsEndpoint = "swap_contract_info";

        private readonly HuobiClientSwapsCoinApi _baseClient;

        internal HuobiClientSwapsCoinApiExchangeData(HuobiClientSwapsCoinApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiSwapsSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSwapsSymbol>>(_baseClient.GetUrl(CommonSymbolsEndpoint, SwapApi, "1"), HttpMethod.Get, ct).ConfigureAwait(false);
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
