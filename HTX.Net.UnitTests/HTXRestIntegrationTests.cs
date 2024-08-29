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

namespace HTX.Net.UnitTests
{
    [NonParallelizable]
    internal class HTXRestIntegrationTests : RestIntergrationTest<HTXRestClient>
    {
        public override bool Run { get; set; }

        public HTXRestIntegrationTests()
        {
        }

        public override HTXRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new HTXRestClient(null, loggerFactory, opts =>
            {
                opts.OutputOriginalData = true;
                opts.ApiCredentials = Authenticated ? new ApiCredentials(key, sec) : null;
            });
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetKlinesAsync("TSTTST", Enums.KlineInterval.OneDay, default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.Message, Contains.Substring("invalid-parameter"));
        }

        [Test]
        public async Task TestSpotApiAccount()
        {
            await RunAndCheckResult(client => client.SpotApi.Account.GetAccountsAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetPlatformValuationAsync(default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetAssetValuationAsync(Enums.AccountType.Spot, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetPointBalanceAsync(default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetUserDeductionInfoAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetDeductAssetsAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawalQuotasAsync("BTC", default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawalAddressesAsync("ETH", default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawDepositHistoryAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetTradingFeesAsync(new[] { "ETHUSDT" }, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetUserIdAsync(default), true);
        }

        [Test]
        public async Task TestSpotApiExchangeData()
        {
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSystemStatusAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSymbolStatusAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSymbolsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSymbolConfigAsync(default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetsAndNetworksAsync(default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickersAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", 0, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetLastTradeAsync("ETHUSDT", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT", default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSymbolDetails24HAsync("ETHUSDT", default), false);
        }

        [Test]
        public async Task TestSpotApiTrading()
        {
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOpenOrdersAsync(default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetClosedOrdersAsync("ETHUSDT", default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetHistoricalOrdersAsync("ETHUSDT", default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOpenConditionalOrdersAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetClosedConditionalOrdersAsync("ETHUSDT", Enums.ConditionalOrderStatus.Triggered, default, default, default, default, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestUsdtFuturesApiAccount()
        {
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetAssetValuationAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetIsolatedMarginAccountInfoAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetCrossMarginAccountInfoAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetIsolatedMarginPositionsAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetCrossMarginPositionsAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetIsolatedMarginAssetsAndPositionsAsync("ETH-USDT", default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetCrossMarginAssetsAndPositionsAsync("USDT", default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetFinancialRecordsAsync("USDT", default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetIsolatedMarginAvailableLeverageAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetCrossMarginAvailableLeverageAsync(default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetOrderLimitsAsync(Enums.OrderPriceType.BestOffer, "ETH-USDT", default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetTradingFeesAsync(default, default, default, default, default), true);
        }

        [Test]
        public async Task TestUsdtFuturesApiExchangeData()
        {
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetFundingRateAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetFundingRatesAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetHistoricalFundingRatesAsync("ETH-USDT", default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetLiquidationOrdersAsync("ETH-USDT", Enums.LiquidationTradeType.FullyFilledLiquidationOrders, default, default, default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetHistoricalSettlementRecordsAsync("ETH-USDT", default, default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetTopTraderAccountSentimentAsync("ETH-USDT", Enums.Period.OneDay, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetTopTraderPositionSentimentAsync("ETH-USDT", Enums.Period.OneDay, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetIsolatedMarginStatusAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetCrossTieredMarginInfoAsync("ETH-USDT", default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetIsolatedMarginTieredInfoAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetEstimatedSettlementPriceAsync("ETH-USDT", default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetIsolatedMarginAdjustFactorInfoAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetCrossMarginAdjustFactorInfoAsync("ETH-USDT", default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetInsuranceFundHistoryAsync("ETH-USDT", default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetSwapRiskInfoAsync("ETH-USDT", default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetSwapPriceLimitationAsync("ETH-USDT", default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetSwapOpenInterestAsync("ETH-USDT", default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetContractsAsync(default, default, default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetSwapIndexPriceAsync(default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetContractElementsAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetOrderBookAsync("ETH-USDT", default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetBookTickerAsync(default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 100, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetTickerAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetTickersAsync(default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetLastTradesAsync("ETH-USDT", default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetRecentTradesAsync("ETH-USDT", 10, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetOpenInterestHistoryAsync(Enums.InterestPeriod.OneDay, Enums.Unit.Cont, "ETH-USDT", default ,default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetPremiumIndexKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetEstimatedFundingRateKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetBasisDataAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10 , default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetCrossMarginTradeStatusAsync("ETH-USDT", default ,default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetCrossMarginTransferStatusAsync("USDT", default), false);
        }

        [Test]
        public async Task TestUsdtFuturesApiTrading()
        {
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginOpenOrdersAsync("ETH-USDT", default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetCrossMarginOpenOrdersAsync("ETH-USDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginClosedOrdersAsync("ETH-USDT", Enums.MarginTradeType.BuyShort, true, new[] { OrderStatusFilter.Canceled }, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetCrossMarginClosedOrdersAsync("ETH-USDT", Enums.MarginTradeType.BuyShort, true, new[] { OrderStatusFilter.Canceled }, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginUserTradesAsync("ETH-USDT", Enums.MarginTradeType.BuyShort, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetCrossMarginUserTradesAsync("ETH-USDT", Enums.MarginTradeType.BuyShort, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginOpenTriggerOrdersAsync("ETH-USDT", default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetCrossMarginOpenTriggerOrdersAsync("ETH-USDT", default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginTriggerOrderHistoryAsync("ETH-USDT", MarginTradeType.BuyLong, 90, OrderStatusFilter.FullyMatched, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetCrossMarginTriggerOrderHistoryAsync(MarginTradeType.BuyLong, 90, OrderStatusFilter.FullyMatched, "ETH-USDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginOpenTpSlOrdersAsync("ETH-USDT", default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetCrossMarginOpenTpSlOrdersAsync("ETH-USDT", default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginTpSlHistoryAsync("ETH-USDT", new[] { TpSlStatus.Canceled }, 90, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetCrossMarginTpSlHistoryAsync(new[] { TpSlStatus.Canceled }, 90, "ETH-USDT", default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetOpenIsolatedMarginTrailingOrdersAsync("ETH-USDT", MarginTradeType.BuyLong, 90, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetOpenCrossMarginTrailingOrdersAsync("ETH-USDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetClosedIsolatedMarginTrailingOrdersAsync("ETH-USDT", new[] { TpSlStatus.Canceled }, MarginTradeType.BuyLong, 90, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetClosedCrossMarginTrailingOrdersAsync(new[] { TpSlStatus.Canceled }, MarginTradeType.BuyLong, 90, "ETH-USDT", default, default, default, default, default, default), true);
        }
    }
}
