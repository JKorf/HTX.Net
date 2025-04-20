using HTX.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using HTX.Net.Objects.Options;
using Microsoft.Extensions.Options;

// REST
var restClient = new HTXRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
Console.WriteLine($"Rest client ticker price for ETH-USDT: {ticker.Data.ClosePrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
// Optional, manually add logging
var logFactory = new LoggerFactory();
logFactory.AddProvider(new TraceLoggerProvider());

var socketClient = new HTXSocketClient(Options.Create(new HTXSocketOptions { }), logFactory);
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ethusdt", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETHUSDT: {update.Data.ClosePrice}");
});

Console.ReadLine();
