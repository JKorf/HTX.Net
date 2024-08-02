using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using HTX.Net.Clients;
using HTX.Net.Objects.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/Spot/Account", "https://api.huobi.pro", IsAuthenticated, stjCompare: true, nestedPropertyForCompare: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountsAsync(), "GetAccounts");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetBalancesAsync(1), "GetBalances", "data.list", ignoreProperties: new List<string> { "debt", "available" });
            await tester.ValidateAsync(client => client.SpotApi.Account.GetPlatformValuationAsync(), "GetPlatformValuation");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAssetValuationAsync(Enums.AccountType.Spot), "GetAssetValuation");
            await tester.ValidateAsync(client => client.SpotApi.Account.InternalTransferAsync(1, Enums.AccountType.Spot, 2, 3, Enums.AccountType.Spot, 4, "ETH", 1), "InternalTransfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountHistoryAsync(1), "GetAccountHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountLedgerAsync(1), "GetAccountLedger");
            await tester.ValidateAsync(client => client.SpotApi.Account.TransferAsync(Enums.TransferAccount.Spot, Enums.TransferAccount.Futures, "ETH", 1, "ETH"), "Transfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetPointBalanceAsync(), "GetPointBalance");
            await tester.ValidateAsync(client => client.SpotApi.Account.TransferPointsAsync("1", "2", "3", 1), "TransferPoints");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUserDeductionInfoAsync(), "GetUserDeductionInfo");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDeductAssetsAsync(), "GetDeductAssets");
            await tester.ValidateAsync(client => client.SpotApi.Account.SetDeductionSwitchAsync(Enums.DeductionSwitchType.AssetDeduction), "SetDeductionSwitch");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositAddressesAsync("ETH"), "GetDepositAddresses");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalQuotasAsync("ETH"), "GetWithdrawalQuotas");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalAddressesAsync("ETH"), "GetWithdrawalAddresses");
            await tester.ValidateAsync(client => client.SpotApi.Account.WithdrawAsync("123", "ETH", 1, 1), "Withdraw");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalByClientOrderIdAsync("123"), "GetWithdrawalByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.Account.CancelWithdrawalAsync(1), "CancelWithdrawal");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawDepositAsync(Enums.WithdrawDepositType.Withdraw), "GetWithdrawDeposit");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetTradingFeesAsync(new[] { "ETHUSDT" }), "GetTradingFees");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetApiKeyInfoAsync(1), "GetApiKeyInfo");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUserIdAsync(), "GetUserId");
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/Spot/ExchangeData", "https://api.huobi.pro", IsAuthenticated, stjCompare: true, nestedPropertyForCompare: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSymbolStatusAsync(), "GetSymbolStatus");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSymbolsAsync(), "GetSymbols", ignoreProperties: new List<string> { "p" });
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetsAsync(), "GetAssets");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSymbolConfigAsync(), "GetSymbolConfig");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetsAndNetworksAsync("ETH"), "GetAssetsAndNetworks");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT"), "GetTicker", nestedJsonProperty: "tick");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickersAsync(), "GetTickers");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", 0), "GetOrderBook", nestedJsonProperty: "tick");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetLastTradeAsync("ETHUSDT"), "GetLastTrade", nestedJsonProperty: "tick");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSymbolDetails24HAsync("ETHUSDT"), "GetSymbolDetails24H", nestedJsonProperty: "tick");
        }

        [Test]
        public async Task ValidateSpotMarginCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/Spot/Margin", "https://api.huobi.pro", IsAuthenticated, stjCompare: true, nestedPropertyForCompare: "data");
        }

        [Test]
        public async Task ValidateSpotSubAccountCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/Spot/SubAccount", "https://api.huobi.pro", IsAuthenticated, stjCompare: true, nestedPropertyForCompare: "data");
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/Spot/Trading", "https://api.huobi.pro", IsAuthenticated, stjCompare: true, nestedPropertyForCompare: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOrderAsync(1, "ETHUSDT", Enums.OrderSide.Buy, Enums.OrderType.IOC, 1), "PlaceOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceMultipleOrderAsync(new [] { new HTXOrderRequest { } }), "PlaceMultipleOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceMarginOrderAsync(1, "ETHUSDT", Enums.OrderSide.Buy, Enums.OrderType.IOC, Enums.MarginPurpose.AutomaticLoan, Enums.SourceType.C2CMargin), "PlaceMarginOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderAsync(1), "CancelOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderByClientOrderIdAsync("1"), "CancelOrderByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenOrdersAsync(), "GetOpenOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrdersByCriteriaAsync(), "CancelOrdersByCriteria");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrdersAsync(new[] { 1L } ), "CancelOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderAsync(1), "GetOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderByClientOrderIdAsync("1"), "GetOrderByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderTradesAsync(1), "GetOrderTrades");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetClosedOrdersAsync("ETHUSDT"), "GetClosedOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetHistoricalOrdersAsync("ETHUSDT"), "GetHistoricalOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT"), "GetUserTrades");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceConditionalOrderAsync(1, "ETHUSDT", Enums.OrderSide.Buy, Enums.ConditionalOrderType.Market, 1), "PlaceConditionalOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelConditionalOrdersAsync(new[] { "1" }), "CancelConditionalOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenConditionalOrdersAsync(), "GetOpenConditionalOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetClosedConditionalOrdersAsync("ETHUSDT", Enums.ConditionalOrderStatus.Created), "GetClosedConditionalOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetConditionalOrderAsync("1"), "GetConditionalOrder");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestUrl.Contains("Signature");
        }
    }
}
