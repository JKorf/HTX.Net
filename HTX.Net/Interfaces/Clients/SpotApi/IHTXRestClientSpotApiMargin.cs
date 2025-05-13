using HTX.Net.Enums;
using HTX.Net.Objects.Models;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// HTX margin endpoints.
    /// </summary>
    public interface IHTXRestClientSpotApiMargin
    {
        /// <summary>
        /// Repay a margin loan
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5037d-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="asset">Asset to repay, for example `ETH`</param>
        /// <param name="quantity">Quantity to repay</param>
        /// <param name="transactionId">Loan transaction ID</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXRepaymentResult[]>> RepayLoanAsync(string accountId, string asset, decimal quantity, string? transactionId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset from spot account to isolated margin account
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4c545-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">Trading symbol, for example `ETHUSDT`</param>
        /// <param name="asset">Asset to transfer, for example `ETH`</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer id</returns>
        Task<WebCallResult<long>> TransferSpotToIsolatedMarginAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset from isolated margin to spot account
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4cb3f-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">Trading symbol, for example `ETHUSDT`</param>
        /// <param name="asset">Asset to transfer, for example `ETH`</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer id</returns>
        Task<WebCallResult<long>> TransferIsolatedMarginToSpotAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get isolated loan interest rate and quotas
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4d178-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbols">Filter on symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXLoanInfo[]>> GetIsolatedLoanInterestRateAndQuotaAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Request a loan on isolated margin
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4d587-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">The quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order id</returns>
        Task<WebCallResult<long>> RequestIsolatedMarginLoanAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Repay an isolated margin loan
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4d7f0-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="orderId">Id to repay</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order id</returns>
        Task<WebCallResult<long>> RepayIsolatedMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin order history
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4d423-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get history for, for example `ETHUSDT`</param>
        /// <param name="states">Filter by states</param>
        /// <param name="startDate">Filter by start date</param>
        /// <param name="endDate">Filter by end date</param>
        /// <param name="from">Start order id for use in combination with direction</param>
        /// <param name="direction">Direction of results in combination with from parameter</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXMarginOrder[]>> GetIsolatedMarginClosedOrdersAsync(
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
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4d015-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXMarginBalances[]>> GetIsolatedMarginBalanceAsync(string symbol, int? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer from spot account to cross margin account
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4c32c-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="asset">The asset to transfer, for example `ETH`</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> TransferSpotToCrossMarginAsync(string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Transfer from cross margin account to spot account
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4c47a-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="asset">The asset to transfer, for example `ETH`</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> TransferCrossMarginToSpotAsync(string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin interest rates and quotas
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4bef5-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXLoanInfoAsset[]>> GetCrossLoanInterestRateAndQuotaAsync(CancellationToken ct = default);

        /// <summary>
        /// Request a loan on cross margin
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4c1ac-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">The quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order id</returns>
        Task<WebCallResult<long>> RequestCrossMarginLoanAsync(string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Repay a isolated margin loan
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4c26f-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="orderId">Id to repay</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> RepayCrossMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin order history
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4c055-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startDate">Filter by start date</param>
        /// <param name="endDate">Filter by end date</param>
        /// <param name="from">Start order id for use in combination with direction</param>
        /// <param name="direction">Direction of results in combination with from parameter</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXMarginOrder[]>> GetCrossMarginClosedOrdersAsync(
            string? asset = null,
            MarginOrderStatus? status = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? from = null,
            FilterDirection? direction = null,
            int? limit = null,
            int? subUserId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get cross margin account balance
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4bca0-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXMarginBalances>> GetCrossMarginBalanceAsync(int? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin limits
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec512ec-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXMaxHolding[]>> GetCrossMarginLimitAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get repayment history
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec50446-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="repayId">Filter by repay id</param>
        /// <param name="accountId">Filter by account id</param>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="startTime">Only show records after this</param>
        /// <param name="endTime">Only show records before this</param>
        /// <param name="sort">Sort direction</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">Search id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXRepayment[]>> GetRepaymentHistoryAsync(long? repayId = null, long? accountId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default);
        
    }
}
