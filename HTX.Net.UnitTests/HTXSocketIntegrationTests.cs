using HTX.Net.Clients;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using HTX.Net.Objects.Models.Socket;

namespace HTX.Net.UnitTests
{
    [NonParallelizable]
    internal class HTXSocketIntegrationTests : SocketIntegrationTest<HTXSocketClient>
    {
        public override bool Run { get; set; } = false;

        public HTXSocketIntegrationTests()
        {
        }

        public override HTXSocketClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new HTXSocketClient(Options.Create(new HTXSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        [Test]
        public async Task TestSubscriptions()
        {
            await RunAndCheckUpdate<HTXAccountUpdate>((client, updateHandler) => client.SpotApi.SubscribeToAccountUpdatesAsync(updateHandler, default, default), false, true);
            await RunAndCheckUpdate<HTXSymbolTick>((client, updateHandler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);

            await RunAndCheckUpdate<HTXUsdtMarginSwapCrossBalanceUpdate>((client, updateHandler) => client.UsdtFuturesApi.SubscribeToCrossMarginBalanceUpdatesAsync(updateHandler, default), false, true);
            await RunAndCheckUpdate<HTXSymbolTickUpdate>((client, updateHandler) => client.UsdtFuturesApi.SubscribeToTickerUpdatesAsync("ETH-USDT", updateHandler, default), true, false);
        } 
    }
}
