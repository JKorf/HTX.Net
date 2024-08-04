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
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/Spot/SubAccount", "https://api.huobi.pro", IsAuthenticated, stjCompare: true, nestedPropertyForCompare: "data");
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

        [Test]
        public async Task ValidateUsdtMarginSwapAccountCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/UsdtMarginSwap/Account", "https://api.hbdm.com", IsAuthenticated, stjCompare: true, nestedPropertyForCompare: "data");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetAssetValuationAsync(), "GetAssetValuation");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetIsolatedMarginAccountInfoAsync(), "GetIsolatedMarginAccountInfo");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetCrossMarginAccountInfoAsync(), "GetCrossMarginAccountInfo");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetIsolatedMarginPositionsAsync(), "GetIsolatedMarginPositions");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetCrossMarginPositionsAsync(), "GetCrossMarginPositions");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetIsolatedMarginAssetsAndPositionsAsync("ETH-USDT"), "GetIsolatedMarginAssetsAndPositions");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetCrossMarginAssetsAndPositionsAsync("ETH-USDT"), "GetCrossMarginAssetsAndPositions");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetFinancialRecordsAsync("ETH-USDT"), "GetFinancialRecords");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetIsolatedMarginAvailableLeverageAsync("ETH-USDT"), "GetIsolatedMarginAvailableLeverage");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetCrossMarginAvailableLeverageAsync("ETH-USDT"), "GetCrossMarginAvailableLeverage");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetOrderLimitsAsync(Enums.OrderPriceType.PostOnly), "GetOrderLimits");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetTradingFeesAsync(), "GetTradingFees");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetIsolatedMarginTransferLimitsAsync(), "GetIsolatedMarginTransferLimits");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetCrossMarginTransferLimitsAsync(), "GetCrossMarginTransferLimits");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetIsolatedMarginPositionLimitAsync("ETH-USDT"), "GetIsolatedMarginPositionLimit");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetCrossMarginPositionLimitsAsync("ETH-USDT"), "GetCrossMarginPositionLimits");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetIsolatedMarginLeveragePositionLimitsAsync("ETH-USDT"), "GetIsolatedMarginLeveragePositionLimits");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetCrossMarginLeveragePositionLimitsAsync(Enums.BusinessType.Futures), "GetCrossMarginLeveragePositionLimits");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.TransferMarginAccountsAsync("ETH", "123", "123", 1), "TransferMarginAccounts");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetTradingStatusAsync(), "GetTradingStatus");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.SetIsolatedMarginPositionModeAsync("ETH-USDT", Enums.PositionMode.SingleSide), "SetIsolatedMarginPositionMode");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.SetCrossMarginPositionModeAsync("ETH-USDT", Enums.PositionMode.SingleSide), "SetCrossMarginPositionMode");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetIsolatedMarginPositionModeAsync("ETH-USDT"), "GetIsolatedMarginPositionMode");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetCrossMarginPositionModeAsync("ETH-USDT"), "GetCrossMarginPositionMode");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetIsolatedMarginSettlementRecordsAsync("ETH-USDT"), "GetIsolatedMarginSettlementRecords");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.Account.GetCrossMarginSettlementRecordsAsync("ETH-USDT"), "GetCrossMarginSettlementRecords");
        }

        [Test]
        public async Task ValidateUsdtMarginSwapExchangeDataCalls()
        {
            var client = new HTXRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<HTXRestClient>(client, "Endpoints/UsdtMarginSwap/ExchangeData", "https://api.hbdm.com", IsAuthenticated, stjCompare: true, nestedPropertyForCompare: "data");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetFundingRateAsync("ETH-USDT"), "GetFundingRate");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetFundingRatesAsync(), "GetFundingRates");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetHistoricalFundingRatesAsync("ETH-USDT"), "GetHistoricalFundingRates");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetLiquidationOrdersAsync("ETH-USDT", Enums.LiquidationTradeType.FullyFilledLiquidationOrders), "GetLiquidationOrders");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetHistoricalSettlementRecordsAsync("ETH-USDT"), "GetHistoricalSettlementRecords");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetTopTraderAccountSentimentAsync("ETH-USDT", Enums.Period.OneDay), "GetTopTraderAccountSentiment");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetTopTraderPositionSentimentAsync("ETH-USDT", Enums.Period.OneDay), "GetTopTraderPositionSentiment");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetIsolatedMarginStatusAsync("ETH-USDT"), "GetIsolatedMarginStatus");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetCrossTieredMarginInfoAsync("ETH-USDT"), "GetCrossTieredMarginInfo");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetIsolatedMarginTieredInfoAsync("ETH-USDT"), "GetIsolatedMarginTieredInfo");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetEstimatedSettlementPriceAsync("ETH-USDT"), "GetEstimatedSettlementPrice");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetIsolatedMarginAdjustFactorInfoAsync("ETH-USDT"), "GetIsolatedMarginAdjustFactorInfo");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetCrossMarginAdjustFactorInfoAsync("ETH-USDT"), "GetCrossMarginAdjustFactorInfo");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetInsuranceFundHistoryAsync("ETH-USDT"), "GetInsuranceFundHistory");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetSwapRiskInfoAsync("ETH-USDT"), "GetSwapRiskInfo");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetSwapPriceLimitationAsync("ETH-USDT"), "GetSwapPriceLimitation");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetSwapOpenInterestAsync("ETH-USDT"), "GetSwapOpenInterest");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetContractsAsync("ETH-USDT"), "GetContractInfo");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetSwapIndexPriceAsync("ETH-USDT"), "GetSwapIndexPrice");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetContractElementsAsync("ETH-USDT"), "GetContractElements");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetOrderBookAsync("ETH-USDT"), "GetOrderBook", nestedJsonProperty: "tick", ignoreProperties: new List<string> { "ch", "id", "mrid" });
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetBookTickerAsync("ETH-USDT"), "GetBookTicker", nestedJsonProperty: "ticks");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetMarkPriceKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10), "GetMarkPriceKlines");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetTickerAsync("ETH-USDT"), "GetTicker", nestedJsonProperty: "tick");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetTickersAsync("ETH-USDT"), "GetTickers", nestedJsonProperty: "ticks");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetLastTradesAsync("ETH-USDT"), "GetLastTrades", nestedJsonProperty: "tick.data.0");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetRecentTradesAsync("ETH-USDT", 10), "GetRecentTrades", "data.0.data");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetOpenInterestHistoryAsync(Enums.InterestPeriod.OneDay, Enums.Unit.Cont), "GetOpenInterestHistory");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetPremiumIndexKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10), "GetPremiumIndexKlines");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetEstimatedFundingRateKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10), "GetEstimatedFundingRateKlines");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetBasisDataAsync("ETH-USDT", Enums.KlineInterval.OneDay, 10), "GetBasisData");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetCrossMarginTradeStatusAsync("ETH-USDT"), "GetCrossMarginTradeStatus");
            await tester.ValidateAsync(client => client.UsdtMarginSwapApi.ExchangeData.GetCrossMarginTransferStatusAsync("ETH-USDT"), "GetCrossMarginTransferStatus");
        }
    
        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestUrl.Contains("Signature");
        }
    }
}
