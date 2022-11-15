using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Huobi usdt swap account endpoints
    /// </summary>
    public interface IHuobiClientUsdtMarginSwapApiAccount
    {
        /// <summary>
        /// Get asset values
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-asset-valuation"/></para>
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiAssetValue>>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin account info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-user-s-account-information"/></para>
        /// </summary>
        /// <param name="contractCode">Optional contract code filter</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiIsolatedMarginAccountInfo>>> GetIsolatedMarginAccountInfoAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin account info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-user-39-s-account-information"/></para>
        /// </summary>
        /// <param name="marginAccount">Optional margin account filter</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiCrossMarginAccountInfo>>> GetCrossMarginAccountInfoAsync(string? marginAccount = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin assets and positions
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-assets-and-positions"/></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiCrossMarginAssetsAndPositions>> GetCrossMarginAssetAndPositionsAsync(string marginAccount, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin available leverage
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-user-s-available-leverage" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiCrossMarginLeverageAvailable>>> GetCrossMarginAvailableLeverageAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin positions
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-user-39-s-position-information" /></para>
        /// </summary>
        /// <param name="contractCode">Filter by contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiPosition>>> GetCrossMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin settlement records
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-settlement-records-of-users" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiCrossMarginUserSettlementRecordPage>> GetCrossMarginSettlementRecordsAsync(string marginAccount, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin sub account assets
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-assets-information-of-all-sub-accounts-under-the-master-account" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>> GetCrossMarginSubAccountsAssetsAsync(string? marginAccount = null, CancellationToken ct = default);
        /// <summary>
        /// Get financial records
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-account-financial-records" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="types">Filter by type</param>
        /// <param name="createDate">Filter by create date</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiFinancialRecordsPage>> GetFinancialRecordsAsync(string marginAccount, string? contractCode = null, IEnumerable<FinancialRecordType>? types = null, DateTime? createDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin assets and positisons
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-assets-and-positions" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiIsolatedMarginAssetsAndPositions>>> GetIsolatedMarginAssetAndPositionsAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin available leverage
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-user-s-available-leverage" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiIsolatedMarginLeverageAvailable>>> GetIsolatedMarginAvailableLeverageAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin position info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-user-s-position-information" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiPosition>>> GetIsolatedMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin settlement records
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-settlement-records-of-users" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiIsolatedMarginUserSettlementRecordPage>> GetIsolatedMarginSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin sub account assets
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-assets-information-of-all-sub-accounts-under-the-master-account" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>> GetIsolatedMarginSubAccountsAssetsAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get master sub account transfer records
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-transfer-records-between-master-and-sub-account" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="daysInHistory">Days in history</param>
        /// <param name="type">Filter by type</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiMasterSubTransfer>> GetMasterSubTransferRecordsAsync(string marginAccount, int daysInHistory, MasterSubTransferType? type = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get trading fees
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-swap-trading-fee" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business tpye</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiTradingFee>>> GetTradingFeesAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Switch cross margin position mode
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-switch-position-mode" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="positionMode">Position mode</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiPositionMode>> ModifyCrossMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default);
        /// <summary>
        /// Switch isolated margin position mode
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-switch-position-mode" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="positionMode">Position mode</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiPositionMode>> ModifyIsolatedMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default);
        /// <summary>
        /// Set sub account trading permissions
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-set-a-batch-of-sub-account-trading-permissions" /></para>
        /// </summary>
        /// <param name="subAccountUids">Uids of the subaccounts</param>
        /// <param name="enabled">Enable trading</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSubAccountResult>> SetSubAccountsTradingPermissionsAsync(IEnumerable<string> subAccountUids, bool enabled, CancellationToken ct = default);
        /// <summary>
        /// Transfer between margin accounts
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-transfer-between-different-margin-accounts-under-the-same-account" /></para>
        /// </summary>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="fromMarginAccount">From account</param>
        /// <param name="toMarginAccount">To account</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOrderId>> TransferMarginAccountsAsync(string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Transfer between master and sub account
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-transfer-between-master-and-sub-account" /></para>
        /// </summary>
        /// <param name="subUid">Sub account uid</param>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="fromMarginAccount">From account</param>
        /// <param name="toMarginAccount">To account</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="type">Type</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOrderId>> TransferMasterSubAsync(string subUid, string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, MasterSubTransferType type, long? clientOrderId = null, CancellationToken ct = default);
    }
}