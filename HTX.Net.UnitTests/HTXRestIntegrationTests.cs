using HTX.Net.Clients;
using HTX.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTX.Net.Enums;
using Microsoft.Extensions.Options;
using HTX.Net.SymbolOrderBooks;
using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net.UnitTests
{
    [NonParallelizable]
    internal class HTXRestIntegrationTests : RestIntegrationTest<HTXRestClient>
    {
        public override bool Run { get; set; } = false;

        public HTXRestIntegrationTests()
        {
        }

        public override HTXRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new HTXRestClient(null, loggerFactory, Options.Create(new Objects.Options.HTXRestOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new HTXCredentials().WithHMAC(key, sec) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetKlinesAsync("TSTTST", Enums.KlineInterval.OneDay, default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.UnknownSymbol));
        }

        [Test]
        public async Task TestSpotApiAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetAccountsAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetPlatformValuationAsync(default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetAssetValuationAsync(Enums.AccountType.Spot, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetPointBalanceAsync(default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetUserDeductionInfoAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetDeductAssetsAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetWithdrawalQuotasAsync("BTC", default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetWithdrawalAddressesAsync("ETH", default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetWithdrawDepositHistoryAsync(default, default, default, default, default, default), true, "data", ignoreProperties: ["address-id"]);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetTradingFeesAsync(new[] { "ETHUSDT" }, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetUserIdAsync(default), true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotApiExchangeData()
        {
            var warnings = new List<Exception>();
            //await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSystemStatusAsync(default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetSymbolStatusAsync(default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetSymbolsAsync(default), false, "data", ignoreProperties: ["p", "p1", "cfn", "sm", "mspl", "mbph"]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetAssetsAsync(default), false, "data", ignoreProperties: ["cfn"]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetSymbolConfigAsync(default, default), false, "data", ignoreProperties: ["mbph", "mspl"]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetAssetsAndNetworksAsync(default, default), false, "data", ignoreProperties: ["withdrawFeeExpandData", "withdrawFeeExpandType"]);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT", default), false, "tick");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTickersAsync(default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", 0, default, default), false, "tick", ignoreProperties: ["ts"]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetLastTradeAsync("ETHUSDT", default), false, "tick", ignoreProperties: ["ts"]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT", default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetSymbolDetails24HAsync("ETHUSDT", default), false, "tick");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotApiTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetOpenOrdersAsync(default, default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetClosedOrdersAsync("ETHUSDT", default, default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetHistoricalOrdersAsync("ETHUSDT", default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetOpenConditionalOrdersAsync(default, default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetClosedConditionalOrdersAsync("ETHUSDT", Enums.ConditionalOrderStatus.Triggered, default, default, default, default, default, default, default, default, default), true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public Task TestUsdtFuturesApiAccount()
        {
            // Has to be updated to V5
            return Task.CompletedTask;
        }

        [Test]
        public async Task TestUsdtFuturesApiExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetFundingRateAsync("ETH-USDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetFundingRatesAsync("ETH-USDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetHistoricalFundingRatesAsync("ETH-USDT", default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetLiquidationOrdersAsync("ETH-USDT", Enums.LiquidationTradeType.FullyFilledLiquidationOrders, default, default, default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetHistoricalSettlementRecordsAsync("ETH-USDT", default, default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetTopTraderAccountSentimentAsync("ETH-USDT", Enums.Period.OneDay, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetTopTraderPositionSentimentAsync("ETH-USDT", Enums.Period.OneDay, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetIsolatedMarginStatusAsync("ETH-USDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetCrossTieredMarginInfoAsync("ETH-USDT", default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetIsolatedMarginTieredInfoAsync("ETH-USDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetEstimatedSettlementPriceAsync("ETH-USDT", default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetIsolatedMarginAdjustFactorInfoAsync("ETH-USDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetCrossMarginAdjustFactorInfoAsync("ETH-USDT", default, default, default, default), false, "data");
            //await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetInsuranceFundHistoryAsync("ETH-USDT", default, default, default), false);
            //await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetSwapRiskInfoAsync("ETH-USDT", default, default), false);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetSwapPriceLimitationAsync("ETH-USDT", default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetSwapOpenInterestAsync("ETH-USDT", default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetContractsAsync(default, default, default, default, default, default), false, "data", ignoreProperties: ["adjust", "price_estimated", "open_type"]);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetSwapIndexPriceAsync(default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetContractElementsAsync("ETH-USDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetOrderBookAsync("ETH-USDT", default, default), false, "tick", ignoreProperties: ["ts", "ch"]);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetBookTickerAsync(default, default, default), false, "ticks");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 100, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetTickerAsync("ETH-USDT", default), false, "tick");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetTickersAsync(default, default, default), false, "ticks");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetLastTradesAsync("ETH-USDT", default, default), false, "tick.data.0", ignoreProperties: ["ts"]);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetRecentTradesAsync("ETH-USDT", 10, default), false);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetOpenInterestHistoryAsync(Enums.InterestPeriod.OneDay, Enums.Unit.Cont, "ETH-USDT", default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetPremiumIndexKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetEstimatedFundingRateKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetBasisDataAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetCrossMarginTradeStatusAsync("ETH-USDT", default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetCrossMarginTransferStatusAsync("USDT", default), false, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public Task TestUsdtFuturesApiTrading()
        {
           // Has to be updated to V5
            return Task.CompletedTask;
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new HTXSpotSymbolOrderBook("ETHUSDT"));
            await TestOrderBook(new HTXUsdtFuturesSymbolOrderBook("ETH-USDT"));
        }

    }
}
