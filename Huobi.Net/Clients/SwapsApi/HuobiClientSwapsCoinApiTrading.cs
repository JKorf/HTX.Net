using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Huobi.Net.Converters.Futures;
using Huobi.Net.Enums.Futures;
using Huobi.Net.Interfaces.Clients.SwapsApi;
using Huobi.Net.Objects.Models.Swaps;
using Newtonsoft.Json;

namespace Huobi.Net.Clients.SwapsApi
{
    /// <inheritdoc />
    public class HuobiClientSwapsCoinApiTrading : IHuobiClientSwapsCoinApiTrading
    {
        private const string Api = "swap-api";
        private const string SymbolTradesEndpoint = "swap_matchresults";

        private readonly HuobiClientSwapsCoinApi _baseClient;

        internal HuobiClientSwapsCoinApiTrading(HuobiClientSwapsCoinApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiSwapsOrderTradeResponse>> GetUserTradesAsync(string contractCode, TradeType? tradeType, int daysLookback = 90, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            daysLookback.ValidateIntBetween(nameof(daysLookback), 1, 90);
            limit?.ValidateIntBetween(nameof(limit), 1, 50);

            var tradeTypeConverter = new TradeTypeConverter(false);
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "trade_type", JsonConvert.SerializeObject(TradeType.All, tradeTypeConverter) },
                { "create_date", daysLookback}
            };

            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", limit);

            return await _baseClient.SendHuobiFuturesRequest<HuobiSwapsOrderTradeResponse>(_baseClient.GetUrl(SymbolTradesEndpoint, Api, "1"), HttpMethod.Post, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
    }
}
