using Huobi.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;

// REST
var restClient = new HuobiRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
Console.WriteLine($"Rest client ticker price for ETH-USDT: {ticker.Data.ClosePrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
// Optional, manually add logging
var logFactory = new LoggerFactory();
logFactory.AddProvider(new TraceLoggerProvider());

var socketClient = new HuobiSocketClient(logFactory);
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ethusdt", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETHUSDT: {update.Data.ClosePrice}");
});

Console.ReadLine();
