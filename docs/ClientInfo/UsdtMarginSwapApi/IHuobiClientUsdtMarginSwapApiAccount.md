---
title: IHuobiClientUsdtMarginSwapApiAccount
has_children: false
parent: IHuobiRestClientUsdtMarginSwapApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`HuobiRestClient > UsdtMarginSwapApi > Account`  
*Huobi usdt swap account endpoints*
  

***

## GetAssetValuationAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-asset-valuation](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-asset-valuation)  
<p>

*Get asset values*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetAssetValuationAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiAssetValue>>> GetAssetValuationAsync(string? asset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|The asset|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginAccountInfoAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-user-39-s-account-information](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-user-39-s-account-information)  
<p>

*Get cross margin account info*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetCrossMarginAccountInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiCrossMarginAccountInfo>>> GetCrossMarginAccountInfoAsync(string? marginAccount = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ marginAccount|Optional margin account filter|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginAssetAndPositionsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-assets-and-positions](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-assets-and-positions)  
<p>

*Get cross margin assets and positions*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetCrossMarginAssetAndPositionsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiCrossMarginAssetsAndPositions>> GetCrossMarginAssetAndPositionsAsync(string marginAccount, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|marginAccount|Margin account|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginAvailableLeverageAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-user-s-available-leverage](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-user-s-available-leverage)  
<p>

*Get cross margin available leverage*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetCrossMarginAvailableLeverageAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiCrossMarginLeverageAvailable>>> GetCrossMarginAvailableLeverageAsync(string? contractCode = default, string? symbol = default, ContractType? contractType = default, BusinessType? businessType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract type|
|_[Optional]_ businessType|Business type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginPositionsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-user-39-s-position-information](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-user-39-s-position-information)  
<p>

*Get cross margin positions*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetCrossMarginPositionsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiPosition>>> GetCrossMarginPositionsAsync(string? contractCode = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Filter by contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginSettlementRecordsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-settlement-records-of-users](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-settlement-records-of-users)  
<p>

*Get cross margin settlement records*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetCrossMarginSettlementRecordsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiCrossMarginUserSettlementRecordPage>> GetCrossMarginSettlementRecordsAsync(string marginAccount, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|marginAccount|Margin account|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginSubAccountsAssetsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-assets-information-of-all-sub-accounts-under-the-master-account](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-assets-information-of-all-sub-accounts-under-the-master-account)  
<p>

*Get cross margin sub account assets*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetCrossMarginSubAccountsAssetsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>> GetCrossMarginSubAccountsAssetsAsync(string? marginAccount = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ marginAccount|Margin account|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFinancialRecordsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-account-financial-records](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-account-financial-records)  
<p>

*Get financial records*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetFinancialRecordsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiFinancialRecordsPage>> GetFinancialRecordsAsync(string marginAccount, string? contractCode = default, IEnumerable<FinancialRecordType>? types = default, DateTime? createDate = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|marginAccount|Margin account|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ types|Filter by type|
|_[Optional]_ createDate|Filter by create date|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginAccountInfoAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-user-s-account-information](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-user-s-account-information)  
<p>

*Get isolated margin account info*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetIsolatedMarginAccountInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiIsolatedMarginAccountInfo>>> GetIsolatedMarginAccountInfoAsync(string? contractCode = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Optional contract code filter|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginAssetAndPositionsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-assets-and-positions](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-assets-and-positions)  
<p>

*Get isolated margin assets and positisons*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetIsolatedMarginAssetAndPositionsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiIsolatedMarginAssetsAndPositions>>> GetIsolatedMarginAssetAndPositionsAsync(string contractCode, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginAvailableLeverageAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-user-s-available-leverage](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-user-s-available-leverage)  
<p>

*Get isolated margin available leverage*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetIsolatedMarginAvailableLeverageAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiIsolatedMarginLeverageAvailable>>> GetIsolatedMarginAvailableLeverageAsync(string? contractCode = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginPositionsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-user-s-position-information](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-user-s-position-information)  
<p>

*Get isolated margin position info*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetIsolatedMarginPositionsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiPosition>>> GetIsolatedMarginPositionsAsync(string? contractCode = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginSettlementRecordsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-settlement-records-of-users](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-settlement-records-of-users)  
<p>

*Get isolated margin settlement records*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetIsolatedMarginSettlementRecordsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiIsolatedMarginUserSettlementRecordPage>> GetIsolatedMarginSettlementRecordsAsync(string contractCode, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginSubAccountsAssetsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-assets-information-of-all-sub-accounts-under-the-master-account](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-assets-information-of-all-sub-accounts-under-the-master-account)  
<p>

*Get isolated margin sub account assets*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetIsolatedMarginSubAccountsAssetsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiIsolatedMarginSubAccountAssets>>> GetIsolatedMarginSubAccountsAssetsAsync(string? contractCode = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMasterSubTransferRecordsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-transfer-records-between-master-and-sub-account](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-transfer-records-between-master-and-sub-account)  
<p>

*Get master sub account transfer records*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetMasterSubTransferRecordsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiMasterSubTransfer>> GetMasterSubTransferRecordsAsync(string marginAccount, int daysInHistory, MasterSubTransferType? type = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|marginAccount|Margin account|
|daysInHistory|Days in history|
|_[Optional]_ type|Filter by type|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTradingFeesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-swap-trading-fee](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-swap-trading-fee)  
<p>

*Get trading fees*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.GetTradingFeesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiTradingFee>>> GetTradingFeesAsync(string? contractCode = default, string? symbol = default, ContractType? contractType = default, BusinessType? businessType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract type|
|_[Optional]_ businessType|Business tpye|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ModifyCrossMarginPositionModeAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-switch-position-mode](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-switch-position-mode)  
<p>

*Switch cross margin position mode*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.ModifyCrossMarginPositionModeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiPositionMode>> ModifyCrossMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|marginAccount|Margin account|
|positionMode|Position mode|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ModifyIsolatedMarginPositionModeAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-switch-position-mode](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-switch-position-mode)  
<p>

*Switch isolated margin position mode*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.ModifyIsolatedMarginPositionModeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiPositionMode>> ModifyIsolatedMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|marginAccount|Margin account|
|positionMode|Position mode|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SetSubAccountsTradingPermissionsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-set-a-batch-of-sub-account-trading-permissions](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-set-a-batch-of-sub-account-trading-permissions)  
<p>

*Set sub account trading permissions*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.SetSubAccountsTradingPermissionsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiSubAccountResult>> SetSubAccountsTradingPermissionsAsync(IEnumerable<string> subAccountUids, bool enabled, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountUids|Uids of the subaccounts|
|enabled|Enable trading|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferMarginAccountsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-transfer-between-different-margin-accounts-under-the-same-account](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-transfer-between-different-margin-accounts-under-the-same-account)  
<p>

*Transfer between margin accounts*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.TransferMarginAccountsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiOrderId>> TransferMarginAccountsAsync(string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, long? clientOrderId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset to transfer|
|fromMarginAccount|From account|
|toMarginAccount|To account|
|quantity|Quantity to transfer|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferMasterSubAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-transfer-between-master-and-sub-account](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-transfer-between-master-and-sub-account)  
<p>

*Transfer between master and sub account*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.UsdtMarginSwapApi.Account.TransferMasterSubAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiOrderId>> TransferMasterSubAsync(string subUid, string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, MasterSubTransferType type, long? clientOrderId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subUid|Sub account uid|
|asset|Asset to transfer|
|fromMarginAccount|From account|
|toMarginAccount|To account|
|quantity|Quantity to transfer|
|type|Type|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ ct|Cancellation token|

</p>
