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
        /// <param name="ct"></param>
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
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiWithdrawDeposit>>> GetWithdrawDepositAsync(WithdrawDepositType type, string? asset = null, int? from = null, int? size = null, FilterDirection? direction = null, CancellationToken ct = default);

    }
}
