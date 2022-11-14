using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Net.Objects.Models;

namespace Huobi.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Huobi account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IHuobiClientSpotApiAccount
    {
        /// <summary>
        /// Get the user id associated with the apikey/secret
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-uid"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> GetUserIdAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of users associated with the apikey/secret
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-sub-user-39-s-list"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUser>>> GetSubAccountUsersAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of sub-user accounts associated with the sub-user id
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-sub-user-39-s-account-list"/></para>
        /// </summary>
        /// <param name="subUserId">The if of the user to get accounts for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSubUserAccounts>> GetSubUserAccountsAsync(long subUserId, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-all-accounts-of-the-current-user" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiAccount>>> GetAccountsAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of balances for a specific account
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-account-balance-of-a-specific-account" /></para>
        /// </summary>
        /// <param name="accountId">The id of the account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiBalance>>> GetBalancesAsync(long accountId, CancellationToken ct = default);

        /// <summary>
        /// Gets the valuation of all assets
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-total-valuation-of-platform-assets" /></para>
        /// </summary>
        /// <param name="accountType">Type of account to valuate</param>
        /// <param name="valuationCurrency">The currency to get the value in</param>
        /// <param name="subUserId">The id of the sub user</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiAccountValuation>> GetAssetValuationAsync(AccountType accountType, string? valuationCurrency = null, long? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer assets between accounts
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#asset-transfer" /></para>
        /// </summary>
        /// <param name="fromUserId">From user id</param>
        /// <param name="fromAccountType">From account type</param>
        /// <param name="fromAccountId">From account id</param>
        /// <param name="toUserId">To user id</param>
        /// <param name="toAccountType">To account type</param>
        /// <param name="toAccountId">To account id</param>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Amount to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiTransactionResult>> TransferAssetAsync(long fromUserId, AccountType fromAccountType, long fromAccountId,
            long toUserId, AccountType toAccountType, long toAccountId, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of balance changes of specified user's account
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-account-history" /></para>
        /// </summary>
        /// <param name="accountId">The id of the account to get the balances for</param>
        /// <param name="asset">Asset name</param>
        /// <param name="transactionTypes">Blance change types</param>
        /// <param name="startTime">Far point of time of the query window. The maximum size of the query window is 1 hour. The query window can be shifted within 30 days</param>
        /// <param name="endTime">Near point of time of the query window. The maximum size of the query window is 1 hour. The query window can be shifted within 30 days</param>
        /// <param name="sort">Sorting order (Ascending by default)</param>
        /// <param name="size">Maximum number of items in each response (from 1 to 500, default is 100)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiAccountHistory>>> GetAccountHistoryAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? size = null, CancellationToken ct = default);

        /// <summary>
        /// This endpoint returns the balance changes of specified user's account.
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-account-ledger" /></para>
        /// </summary>
        /// <param name="accountId">The id of the account to get the ledger for</param>
        /// <param name="asset">Asset name</param>
        /// <param name="transactionTypes">Blanace change types</param>
        /// <param name="startTime">Far point of time of the query window. The maximum size of the query window is 10 days. The query window can be shifted within 30 days</param>
        /// <param name="endTime">Near point of time of the query window. The maximum size of the query window is 10 days. The query window can be shifted within 30 days</param>
        /// <param name="sort">Sorting order (Ascending by default)</param>
        /// <param name="size">Maximum number of items in each response (from 1 to 500, default is 100)</param>
        /// <param name="fromId">Only get orders with ID before or after this. Used together with the direction parameter</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiLedgerEntry>>> GetAccountLedgerAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? size = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of balances for a specific sub account
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-account-balance-of-a-sub-user" /></para>
        /// </summary>
        /// <param name="subAccountId">The id of the sub account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiBalance>>> GetSubAccountBalancesAsync(long subAccountId, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset between parent and sub account
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-between-parent-and-sub-account" /></para>
        /// </summary>
        /// <param name="subAccountId">The target sub account id to transfer to or from</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">The quantity of asset to transfer</param>
        /// <param name="transferType">The type of transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Unique transfer id</returns>
        Task<WebCallResult<long>> TransferWithSubAccountAsync(long subAccountId, string asset, decimal quantity, TransferType transferType, CancellationToken ct = default);

        /// <summary>
        /// Parent user and sub user could query deposit address of corresponding chain, for a specific crypto currency (except IOTA).
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#query-deposit-address" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiDepositAddress>>> GetDepositAddressesAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Parent user creates a withdraw request from spot account to an external address (exists in your withdraw address list), which doesn't require two-factor-authentication.
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#create-a-withdraw-request" /></para>
        /// </summary>
        /// <param name="address">The desination address of this withdraw</param>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">The quantity of asset to withdraw</param>
        /// <param name="fee">The fee to pay with this withdraw</param>
        /// <param name="network">Set as "usdt" to withdraw USDT to OMNI, set as "trc20usdt" to withdraw USDT to TRX</param>
        /// <param name="addressTag">A tag specified for this address</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> WithdrawAsync(string address, string asset, decimal quantity, decimal fee, string? network = null, string? addressTag = null, CancellationToken ct = default);

        /// <summary>
        /// Parent user and sub user searche for all existed withdraws and deposits and return their latest status.
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-for-existed-withdraws-and-deposits" /></para>
        /// </summary>
        /// <param name="type">Define transfer type to search</param>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="from">The transfer id to begin search</param>
        /// <param name="size">The number of items to return</param>
        /// <param name="direction">the order of response</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiWithdrawDeposit>>> GetWithdrawDepositAsync(WithdrawDepositType type, string? asset = null, int? from = null, int? size = null, FilterDirection? direction = null, CancellationToken ct = default);

        /// <summary>
        /// Repay a margin loan
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-cross-isolated" /></para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="asset">Asset to repay</param>
        /// <param name="quantity">Quantity to repay</param>
        /// <param name="transactionId">Loan transaction ID</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiRepaymentResult>>> RepayMarginLoanAsync(string accountId, string asset, decimal quantity, string? transactionId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset from spot account to isolated margin account
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-spot-trading-account-to-isolated-margin-account-isolated" /></para>
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer id</returns>
        Task<WebCallResult<long>> TransferSpotToIsolatedMarginAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset from isolated margin to spot account
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-isolated-margin-account-to-spot-trading-account-isolated" /></para>
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer id</returns>
        Task<WebCallResult<long>> TransferIsolatedMarginToSpotAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get isolated loan interest rate and quotas
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-loan-interest-rate-and-quota-isolated" /></para>
        /// </summary>
        /// <param name="symbols">Filter on symbols</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiLoanInfo>>> GetIsolatedLoanInterestRateAndQuotaAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Request a loan on isolated margin
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#request-a-margin-loan-isolated" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="asset">The asset</param>
        /// <param name="quantity">The quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order id</returns>
        Task<WebCallResult<long>> RequestIsolatedMarginLoanAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Repay a isolated margin loan
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-isolated" /></para>
        /// </summary>
        /// <param name="orderId">Id to repay</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order id</returns>
        Task<WebCallResult<long>> RepayIsolatedMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin orders history
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-past-margin-orders-isolated" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get history for</param>
        /// <param name="states">Filter by states</param>
        /// <param name="startDate">Filter by start date</param>
        /// <param name="endDate">Filter by end date</param>
        /// <param name="from">Start order id for use in combination with direction</param>
        /// <param name="direction">Direction of results in combination with from parameter</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiMarginOrder>>> GetIsolatedMarginClosedOrdersAsync(
            string symbol,
            IEnumerable<MarginOrderStatus>? states = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? from = null,
            FilterDirection? direction = null,
            int? limit = null,
            int? subUserId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin account balance
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-balance-of-the-margin-loan-account-isolated" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiMarginBalances>>> GetIsolatedMarginBalanceAsync(string symbol, int? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer from spot account to cross margin account
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-spot-trading-account-to-cross-margin-account-cross" /></para>
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> TransferSpotToCrossMarginAsync(string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Transfer from cross margin account to spot account
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-cross-margin-account-to-spot-trading-account-cross" /></para>
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> TransferCrossMarginToSpotAsync(string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin interest rates and quotas
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-loan-interest-rate-and-quota-cross" /></para>
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiLoanInfoAsset>>> GetCrossLoanInterestRateAndQuotaAsync(CancellationToken ct = default);

        /// <summary>
        /// Request a loan on cross margin
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#request-a-margin-loan-cross" /></para>
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="quantity">The quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order id</returns>
        Task<WebCallResult<long>> RequestCrossMarginLoanAsync(string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Repay a isolated margin loan
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-cross" /></para>
        /// </summary>
        /// <param name="orderId">Id to repay</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order id</returns>
        Task<WebCallResult<object>> RepayCrossMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin order history
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-past-margin-orders-cross" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="state">Filter by state</param>
        /// <param name="startDate">Filter by start date</param>
        /// <param name="endDate">Filter by end date</param>
        /// <param name="from">Start order id for use in combination with direction</param>
        /// <param name="direction">Direction of results in combination with from parameter</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiMarginOrder>>> GetCrossMarginClosedOrdersAsync(
            string? asset = null,
            MarginOrderStatus? state = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? from = null,
            FilterDirection? direction = null,
            int? limit = null,
            int? subUserId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get cross margin account balance
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-balance-of-the-margin-loan-account-cross" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiMarginBalances>> GetCrossMarginBalanceAsync(int? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Get repayment history
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#repayment-record-reference" /></para>
        /// </summary>
        /// <param name="repayId">Filter by repay id</param>
        /// <param name="accountId">Filter by account id</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Only show records after this</param>
        /// <param name="endTime">Only show records before this</param>
        /// <param name="sort">Sort direction</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">Search id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiRepayment>>> GetRepaymentHistoryAsync(long? repayId = null, long? accountId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default);
        
        /// <summary>
        /// Get Current Fee Rate Applied to The User
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-current-fee-rate-applied-to-the-user" /></para>
        /// </summary>
        /// <param name="symbols">Filter on symbols</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiFeeRate>>> GetCurrentFeeRatesAsync(IEnumerable<string> symbols,
            CancellationToken ct = default);
    }
}
