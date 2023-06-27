---
title: Rest API documentation
has_children: true
---
*[generated documentation]*  
### HuobiRestClient  
*Client for accessing the Huobi API.*
  
***
*Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.*  
**void SetApiCredentials(ApiCredentials credentials);**  
***
*Spot endpoints*  
**IHuobiClientSpotApi SpotApi { get; }**  
***
*Usdt margin swap endpoints*  
**IHuobiClientUsdtMarginSwapApi UsdtMarginSwapApi { get; }**  
