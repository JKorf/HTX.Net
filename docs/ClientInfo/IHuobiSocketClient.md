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
**[IHuobiSocketClientSpotStreams](SpotApi/IHuobiSocketClientSpotStreams.html) SpotStreams { get; }**  
***
*Usdt margin swap streams*  
**[IHuobiSocketClientUsdtMarginSwapStreams](UsdtMarginSwapApi/IHuobiSocketClientUsdtMarginSwapStreams.html) UsdtMarginSwapStreams { get; }**  
