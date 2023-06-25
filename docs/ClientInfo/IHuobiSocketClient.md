---
title: Socket API documentation
has_children: true
---
*[generated documentation]*  
### HuobiSocketClient  
*Client for accessing the Huobi websocket API.*
  
***
*Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.*  
**void SetApiCredentials(ApiCredentials credentials);**  
***
*Spot streams*  
**[IHuobiSocketClientSpotApi](SpotApi/IHuobiSocketClientSpotApi.html) SpotApi { get; }**  
***
*Usdt margin swap streams*  
**[IHuobiSocketClientUsdtMarginSwapApi](UsdtMarginSwapApi/IHuobiSocketClientUsdtMarginSwapApi.html) UsdtMarginSwapApi { get; }**  
