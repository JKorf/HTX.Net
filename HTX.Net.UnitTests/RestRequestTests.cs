using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using HTX.Net.Clients;
using HTX.Net.Objects.Models;
using NUnit.Framework;
using System;
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
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/Spot/Account", "https://api.huobi.pro", IsAuthenticated, nestedPropertyForCompare: "data");
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
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawDepositHistoryAsync(Enums.WithdrawDepositType.Withdraw), "GetWithdrawDeposit");
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
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/Spot/ExchangeData", "https://api.huobi.pro", IsAuthenticated, nestedPropertyForCompare: "data");
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
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/Spot/Margin", "https://api.huobi.pro", IsAuthenticated, nestedPropertyForCompare: "data");
            await tester.ValidateAsync(client => client.SpotApi.Margin.RepayLoanAsync("123", "ETH", 1), "RepayLoan");
            await tester.ValidateAsync(client => client.SpotApi.Margin.TransferSpotToIsolatedMarginAsync("123", "ETH", 1), "TransferSpotToIsolatedMargin");
            await tester.ValidateAsync(client => client.SpotApi.Margin.TransferIsolatedMarginToSpotAsync("123", "ETH", 1), "TransferIsolatedMarginToSpot");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetIsolatedLoanInterestRateAndQuotaAsync(), "GetIsolatedLoanInterestRateAndQuota");
            await tester.ValidateAsync(client => client.SpotApi.Margin.RequestIsolatedMarginLoanAsync("ETHUSDT", "ETH", 1), "RequestIsolatedMarginLoan");
            await tester.ValidateAsync(client => client.SpotApi.Margin.RepayIsolatedMarginLoanAsync("123", 1), "RepayIsolatedMarginLoan");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetIsolatedMarginClosedOrdersAsync("123"), "GetIsolatedMarginClosedOrders");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetIsolatedMarginBalanceAsync("ETHUSDT"), "GetIsolatedMarginBalance");
            await tester.ValidateAsync(client => client.SpotApi.Margin.TransferSpotToCrossMarginAsync("ETH", 1), "TransferSpotToCrossMargin");
            await tester.ValidateAsync(client => client.SpotApi.Margin.TransferCrossMarginToSpotAsync("ETH", 1), "TransferCrossMarginToSpot");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetCrossLoanInterestRateAndQuotaAsync(), "GetCrossLoanInterestRateAndQuota");
            await tester.ValidateAsync(client => client.SpotApi.Margin.RequestCrossMarginLoanAsync("ETH", 1), "RequestCrossMarginLoan");
            await tester.ValidateAsync(client => client.SpotApi.Margin.RepayCrossMarginLoanAsync("123", 1), "RepayCrossMarginLoan");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetCrossMarginClosedOrdersAsync(), "GetCrossMarginClosedOrders");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetCrossMarginBalanceAsync(), "GetCrossMarginBalance");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetCrossMarginLimitAsync(), "GetCrossMarginLimit");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetRepaymentHistoryAsync(), "GetRepaymentHistory");
        }

        [Test]
        public async Task ValidateSpotSubAccountCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/Spot/SubAccount", "https://api.huobi.pro", IsAuthenticated, nestedPropertyForCompare: "data");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.SetDeductModeAsync(new[] { "1" }, Enums.DeductMode.Sub), "SetDeductMode");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.CreateSubAccountsAsync(new[] { new HTXSubAccountRequest { UserName = "123" } }), "CreateSubAccounts");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetSubUserListAsync(), "GetSubUserList");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.SetLockAsync(1, Enums.LockAction.Normal), "SetLock");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetSubUserAsync(1), "GetSubUser");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.SetTradableMarketAsync(new[] { "1" }, Enums.SubAccountMarketType.IsolatedMargin, true), "SetTradableMarket");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.SetAssetTransferPermissionsAsync(new[] { "1" }, true), "SetAssetTransferPermissions");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetSubUserAccountsAsync(1), "GetSubUserAccounts");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.CreateApiKeyAsync("1", 1, "123", new[] { "" }, new[] { "" }), "CreateApiKey");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.EditApiKeyAsync(1, "123", "123", new[] {""}, new[] {""}), "EditApiKey");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.DeleteApiKeyAsync(1, "123"), "DeleteApiKey");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.TransferWithSubAccountAsync(1, "ETH", 1, Enums.TransferType.PointFromSubAccount), "TransferWithSubAccount");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetDepositAddressAsync(1, "ETH"), "GetDepositAddress");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetDepositHistoryAsync(1, "ETH"), "GetDepositHistory");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetAggregateBalancesAsync(), "GetAggregateBalances");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetBalancesAsync(1), "GetBalances", ignoreProperties: new List<string> { "debt", "available" });
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/Spot/Trading", "https://api.huobi.pro", IsAuthenticated, nestedPropertyForCompare: "data");
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

        [Test]
        public async Task ValidateUsdtMarginSwapAccountCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/UsdtMarginSwap/Account", "https://api.hbdm.com", IsAuthenticated, nestedPropertyForCompare: "data");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetAssetValuationAsync(), "GetAssetValuation");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetIsolatedMarginAccountInfoAsync(), "GetIsolatedMarginAccountInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetCrossMarginAccountInfoAsync(), "GetCrossMarginAccountInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetIsolatedMarginPositionsAsync(), "GetIsolatedMarginPositions");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetCrossMarginPositionsAsync(), "GetCrossMarginPositions");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetIsolatedMarginAssetsAndPositionsAsync("ETH-USDT"), "GetIsolatedMarginAssetsAndPositions");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetCrossMarginAssetsAndPositionsAsync("ETH-USDT"), "GetCrossMarginAssetsAndPositions");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetFinancialRecordsAsync("ETH-USDT"), "GetFinancialRecords");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetIsolatedMarginAvailableLeverageAsync("ETH-USDT"), "GetIsolatedMarginAvailableLeverage");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetCrossMarginAvailableLeverageAsync("ETH-USDT"), "GetCrossMarginAvailableLeverage");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetOrderLimitsAsync(Enums.OrderPriceType.PostOnly), "GetOrderLimits");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetTradingFeesAsync(), "GetTradingFees");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetIsolatedMarginTransferLimitsAsync(), "GetIsolatedMarginTransferLimits");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetCrossMarginTransferLimitsAsync(), "GetCrossMarginTransferLimits");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetIsolatedMarginPositionLimitAsync("ETH-USDT"), "GetIsolatedMarginPositionLimit");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetCrossMarginPositionLimitsAsync("ETH-USDT"), "GetCrossMarginPositionLimits");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetIsolatedMarginLeveragePositionLimitsAsync("ETH-USDT"), "GetIsolatedMarginLeveragePositionLimits");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetCrossMarginLeveragePositionLimitsAsync(Enums.BusinessType.Futures), "GetCrossMarginLeveragePositionLimits");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.TransferMarginAccountsAsync("ETH", "123", "123", 1), "TransferMarginAccounts");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetTradingStatusAsync(), "GetTradingStatus");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.SetIsolatedMarginPositionModeAsync("ETH-USDT", Enums.PositionMode.SingleSide), "SetIsolatedMarginPositionMode");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.SetCrossMarginPositionModeAsync("ETH-USDT", Enums.PositionMode.SingleSide), "SetCrossMarginPositionMode");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetIsolatedMarginPositionModeAsync("ETH-USDT"), "GetIsolatedMarginPositionMode");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetCrossMarginPositionModeAsync("ETH-USDT"), "GetCrossMarginPositionMode");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetIsolatedMarginSettlementRecordsAsync("ETH-USDT"), "GetIsolatedMarginSettlementRecords");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetCrossMarginSettlementRecordsAsync("ETH-USDT"), "GetCrossMarginSettlementRecords");
        }

        [Test]
        public async Task ValidateUsdtMarginSwapExchangeDataCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/UsdtMarginSwap/ExchangeData", "https://api.hbdm.com", IsAuthenticated, nestedPropertyForCompare: "data");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetFundingRateAsync("ETH-USDT"), "GetFundingRate");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetFundingRatesAsync(), "GetFundingRates");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetHistoricalFundingRatesAsync("ETH-USDT"), "GetHistoricalFundingRates");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetLiquidationOrdersAsync("ETH-USDT", Enums.LiquidationTradeType.FullyFilledLiquidationOrders), "GetLiquidationOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetHistoricalSettlementRecordsAsync("ETH-USDT"), "GetHistoricalSettlementRecords");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetTopTraderAccountSentimentAsync("ETH-USDT", Enums.Period.OneDay), "GetTopTraderAccountSentiment");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetTopTraderPositionSentimentAsync("ETH-USDT", Enums.Period.OneDay), "GetTopTraderPositionSentiment");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetIsolatedMarginStatusAsync("ETH-USDT"), "GetIsolatedMarginStatus");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetCrossTieredMarginInfoAsync("ETH-USDT"), "GetCrossTieredMarginInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetIsolatedMarginTieredInfoAsync("ETH-USDT"), "GetIsolatedMarginTieredInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetEstimatedSettlementPriceAsync("ETH-USDT"), "GetEstimatedSettlementPrice");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetIsolatedMarginAdjustFactorInfoAsync("ETH-USDT"), "GetIsolatedMarginAdjustFactorInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetCrossMarginAdjustFactorInfoAsync("ETH-USDT"), "GetCrossMarginAdjustFactorInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetInsuranceFundHistoryAsync("ETH-USDT"), "GetInsuranceFundHistory");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetSwapRiskInfoAsync("ETH-USDT"), "GetSwapRiskInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetSwapPriceLimitationAsync("ETH-USDT"), "GetSwapPriceLimitation");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetSwapOpenInterestAsync("ETH-USDT"), "GetSwapOpenInterest");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetContractsAsync("ETH-USDT"), "GetContractInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetSwapIndexPriceAsync("ETH-USDT"), "GetSwapIndexPrice");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetContractElementsAsync("ETH-USDT"), "GetContractElements");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetOrderBookAsync("ETH-USDT"), "GetOrderBook", nestedJsonProperty: "tick", ignoreProperties: new List<string> { "ch", "id", "mrid" });
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetBookTickerAsync("ETH-USDT"), "GetBookTicker", nestedJsonProperty: "ticks");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10), "GetMarkPriceKlines");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetTickerAsync("ETH-USDT"), "GetTicker", nestedJsonProperty: "tick");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetTickersAsync("ETH-USDT"), "GetTickers", nestedJsonProperty: "ticks");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetLastTradesAsync("ETH-USDT"), "GetLastTrades", nestedJsonProperty: "tick.data.0");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetRecentTradesAsync("ETH-USDT", 10), "GetRecentTrades", "data.0.data");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetOpenInterestHistoryAsync(Enums.InterestPeriod.OneDay, Enums.Unit.Cont), "GetOpenInterestHistory");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetPremiumIndexKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10), "GetPremiumIndexKlines");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetEstimatedFundingRateKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10), "GetEstimatedFundingRateKlines");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetBasisDataAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10), "GetBasisData");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetCrossMarginTradeStatusAsync("ETH-USDT"), "GetCrossMarginTradeStatus");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetCrossMarginTransferStatusAsync("ETH-USDT"), "GetCrossMarginTransferStatus");
        }

        [Test]
        public async Task ValidateUsdtMarginSwapSubAccountCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/UsdtMarginSwap/SubAccount", "https://api.hbdm.com", IsAuthenticated, nestedPropertyForCompare: "data");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.SubAccount.SetTradingPermissionsAsync(new[] { "1" }, true), "SetTradingPermissions");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.SubAccount.GetTradePermissionsAsync(new[] { "1" }), "GetTradePermissions");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.SubAccount.GetIsolatedMarginAssetsAsync(), "GetIsolatedMarginAssets");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.SubAccount.GetCrossMarginAssetsAsync(), "GetCrossMarginAssets");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.SubAccount.GetIsolatedMarginAssetInfoAsync(), "GetIsolatedMarginAssetInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.SubAccount.GetCrossMarginAssetInfoAsync(), "GetCrossMarginAssetInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.SubAccount.GetIsolatedMarginPositionsAsync(1), "GetIsolatedMarginPositions");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.SubAccount.GetCrossMarginPositionsAsync(1), "GetCrossMarginPositions");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.SubAccount.TransferMasterSubAsync("1", "ETH", "1", "2", 1, Enums.MasterSubTransferType.MasterToSub), "TransferMasterSub");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.SubAccount.GetMasterSubTransferRecordsAsync("1", 90), "GetMasterSubTransferRecords");
        }

        [Test]
        public async Task ValidateUsdtMarginSwapTradingCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/UsdtMarginSwap/Trading", "https://api.hbdm.com", IsAuthenticated, nestedPropertyForCompare: "data");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelOrdersAfterAsync(true, TimeSpan.FromSeconds(10)), "CancelOrdersAfter");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.PlaceIsolatedMarginOrderAsync("ETH-USDT", 1, Enums.OrderSide.Buy, 1, Enums.OrderPriceType.Limit), "PlaceIsolatedMarginOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.PlaceCrossMarginOrderAsync(1, Enums.OrderSide.Sell, 1, Enums.OrderPriceType.Limit), "PlaceCrossMarginOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelIsolatedMarginOrderAsync("ETH-USDT", 1), "CancelIsolatedMarginOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelCrossMarginOrderAsync(1), "CancelCrossMarginOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelIsolatedMarginOrdersAsync("ETH-USDT", new[] { 1L }, new[] { 1L }), "CancelIsolatedMarginOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelCrossMarginOrdersAsync(new[] { 1L }, new[] { 1L }), "CancelCrossMarginOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelAllIsolatedMarginOrdersAsync("ETH-USDT"), "CancelAllIsolatedMarginOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelAllCrossMarginOrdersAsync("ETH-USDT"), "CancelAllCrossMarginOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.SetIsolatedMarginLeverageAsync("ETH-USDT", 1), "SetIsolatedMarginLeverage");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.SetCrossMarginLeverageAsync(1), "SetCrossMarginLeverage");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginOrderAsync("ETH-USDT"), "GetIsolatedMarginOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetCrossMarginOrderAsync("ETH-USDT"), "GetCrossMarginOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginOrdersAsync("ETH-USDT", new[] { 1L }, new[] { 1L }), "GetIsolatedMarginOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetCrossMarginOrdersAsync(new[] { 1L }, new[] { 1L }), "GetCrossMarginOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginOrderDetailsAsync("ETH-USDT", 1), "GetIsolatedMarginOrderDetails", ignoreProperties: new List<string> { "price" });
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetCrossMarginOrderDetailsAsync("ETH-USDT", 1), "GetCrossMarginOrderDetails", ignoreProperties: new List<string> { "price" });
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginOpenOrdersAsync("ETH-USDT", 1), "GetIsolatedMarginOpenOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetCrossMarginOpenOrdersAsync("ETH-USDT"), "GetCrossMarginOpenOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginClosedOrdersAsync("ETH-USDT", Enums.MarginTradeType.LiquidateShort, true, new[] { Enums.OrderStatusFilter.Canceled }), "GetIsolatedMarginClosedOrders", ignoreProperties: new List<string> { "query_id" });
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetCrossMarginClosedOrdersAsync("ETH-USDT", Enums.MarginTradeType.LiquidateShort, true, new[] { Enums.OrderStatusFilter.Canceled }), "GetCrossMarginClosedOrders", ignoreProperties: new List<string> { "query_id" });
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginUserTradesAsync("ETH-USDT", Enums.MarginTradeType.LiquidateShort), "GetIsolatedMarginUserTrades");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetCrossMarginUserTradesAsync("ETH-USDT", Enums.MarginTradeType.LiquidateShort), "GetCrossMarginUserTrades");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CloseIsolatedMarginPositionAsync("ETH-USDT", Enums.OrderSide.Sell), "CloseIsolatedMarginPosition");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CloseCrossMarginPositionAsync(Enums.OrderSide.Sell), "CloseCrossMarginPosition");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.PlaceIsolatedMarginTriggerOrderAsync("ETH-USDT", Enums.TriggerType.LesserThanOrEqual, 1, 1, Enums.OrderSide.Buy), "PlaceIsolatedMarginTriggerOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.PlaceCrossMarginTriggerOrderAsync(Enums.TriggerType.LesserThanOrEqual, 1, 1, Enums.OrderSide.Buy), "PlaceCrossMarginTriggerOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelIsolatedMarginTriggerOrderAsync("ETH-USDT", "1"), "CancelIsolatedMarginTriggerOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelCrossMarginTriggerOrderAsync("1"), "CancelCrossMarginTriggerOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelAllIsolatedMarginTriggerOrdersAsync("ETH-USDT"), "CancelAllIsolatedMarginTriggerOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelAllCrossMarginTriggerOrdersAsync("ETH-USDT"), "CancelAllCrossMarginTriggerOrdersAsync");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginOpenTriggerOrdersAsync("ETH-USDT"), "GetIsolatedMarginOpenTriggerOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetCrossMarginOpenTriggerOrdersAsync("ETH-USDT"), "GetCrossMarginOpenTriggerOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginTriggerOrderHistoryAsync("ETH-USDT", Enums.MarginTradeType.BuyShort, 90, Enums.OrderStatusFilter.FullyMatched), "GetIsolatedMarginTriggerOrderHistory");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetCrossMarginTriggerOrderHistoryAsync(Enums.MarginTradeType.BuyShort, 90, Enums.OrderStatusFilter.FullyMatched), "GetCrossMarginTriggerOrderHistory");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.SetIsolatedMarginTpSlAsync("ETH-USDT", Enums.OrderSide.Sell, 1), "SetIsolatedMarginTpSl");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.SetCrossMarginTpSlAsync(Enums.OrderSide.Sell, 1), "SetCrossMarginTpSl");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelIsolatedMarginTpSlAsync("ETH-USDT", "1"), "CancelIsolatedMarginTpSl");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelCrossMarginTpSlAsync("1"), "CancelCrossMarginTpSl");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelAllIsolatedMarginTpSlAsync("ETH-USDT"), "CancelAllIsolatedMarginTpSl");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelAllCrossMarginTpSlAsync("ETH-USDT"), "CancelAllCrossMarginTpSl");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginOpenTpSlOrdersAsync("ETH-USDT"), "GetIsolatedMarginOpenTpSlOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetCrossMarginOpenTpSlOrdersAsync("ETH-USDT"), "GetCrossMarginOpenTpSlOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginTpSlHistoryAsync("ETH-USDT", new[] { Enums.TpSlStatus.Canceled }, 90), "GetIsolatedMarginTpSlHistory");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetCrossMarginTpSlHistoryAsync(new[] { Enums.TpSlStatus.Canceled }, 90), "GetCrossMarginTpSlHistory");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetIsolatedMarginPositionOpenTpSlInfoAsync("ETH-UST", 1), "GetIsolatedMarginPositionOpenTpSlInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetCrossMarginPositionOpenTpSlInfoAsync(1), "GetCrossMarginPositionOpenTpSlInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.PlaceIsolatedMarginTrailingOrderAsync("ETH-USDT", false, Enums.OrderSide.Sell, Enums.Offset.Open, 1, 1, 1, 1, Enums.OrderPriceType.Limit), "PlaceIsolatedMarginTrailingOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.PlaceCrossMarginTrailingOrderAsync(Enums.OrderSide.Sell, Enums.Offset.Open, 1, 1, 1, 1, Enums.OrderPriceType.Limit), "PlaceCrossMarginTrailingOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelIsolatedMarginTrailingOrderAsync("ETH-USDT", "1"), "CancelIsolatedMarginTrailingOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelCrossMarginTrailingOrderAsync("1"), "CancelCrossMarginTrailingOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelAllIsolatedMarginTrailingOrdersAsync("ETH-USDT"), "CancelAllIsolatedMarginTrailingOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelAllCrossMarginTrailingOrdersAsync("ETH-USDT"), "CancelAllCrossMarginTrailingOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetOpenIsolatedMarginTrailingOrdersAsync("ETH-USDT"), "GetOpenIsolatedMarginTrailingOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetOpenCrossMarginTrailingOrdersAsync("ETH-USDT"), "GetOpenCrossMarginTrailingOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetClosedIsolatedMarginTrailingOrdersAsync("ETH-USDT", new[] { Enums.TpSlStatus.Canceled }, Enums.MarginTradeType.BuyShort, 90), "GetClosedIsolatedMarginTrailingOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetClosedCrossMarginTrailingOrdersAsync(new[] { Enums.TpSlStatus.Canceled }, Enums.MarginTradeType.BuyShort, 90), "GetClosedCrossMarginTrailingOrders");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestUrl.Contains("Signature");
        }
    }
}
