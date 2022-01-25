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
    public class HuobiClientFuturesCoinApiExchangeData : IHuobiClientFuturesCoinApiExchangeData
    {
        private const string ServerTimeEndpoint = "timestamp";
        private const string CommonSymbolsEndpoint = "contract_contract_info";

        private readonly HuobiClientFuturesCoinApi _baseClient;

        internal HuobiClientFuturesCoinApiExchangeData(HuobiClientFuturesCoinApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiFuturesSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiFuturesSymbol>>(_baseClient.GetUrl(CommonSymbolsEndpoint, "1"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiTimestampRequest<DateTime>(_baseClient.GetUrl(ServerTimeEndpoint, "1"), HttpMethod.Get, ct)
                .ConfigureAwait(false);
            if (!result)
                return result.AsError<DateTime>(result.Error!);
            var time = result.Data.Item2;
            return result.As(time);
        }
    }
}
