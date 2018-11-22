using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Objects;
using System;

namespace Huobi.Net.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = "0d6c28e1-f0c412fe-51f73fdc-78470";
            var secret = "df6261f2-44524180-4e8c5db1-c0a18";
            var client = new HuobiClient(new HuobiClientOptions()
            {
                LogVerbosity = LogVerbosity.Debug,
                ApiCredentials = new ApiCredentials(key, secret)
            });
            var socketClient = new HuobiSocketClient(new HuobiSocketClientOptions()
            {
                LogVerbosity = LogVerbosity.Debug,
                ApiCredentials = new ApiCredentials(key, secret)
            });


            //var a = client.GetMarketTickers();
            //var b = client.GetMarketTickerMerged("ethusdt");
            //var c = client.GetMarketKlines("ethusdt", HuobiPeriod.FiveMinutes, 100);
            //var d = client.GetMarketDepth("ethusdt", 0);
            //var e = client.GetMarketLastTrade("ethusdt");
            //var f = client.GetMarketTradeHistory("ethusdt", 50);
            //var g = client.GetMarketDetails24h("ethusdt");
            //var h = client.GetSymbols();
            //var i = client.GetCurrencies();
            //var j = client.GetServerTime();
            //var k = client.GetAccounts();
            //var l = client.GetBalances(k.Data.Data[0].Id);
            //var m = client.PlaceOrder(k.Data.Data[0].Id, "ethusdt", HuobiOrderType.LimitSell, 0.1m, 200);
            //var m2 = client.GetOrders("ethusdt", new[] { HuobiOrderState.Submitted });
            //var n = client.GetOpenOrders();
            //var o = client.CancelOrder(m.Data.Data);
            //var p = client.CancelOrders(new[] { m.Data.Data });
            //var q = client.GetOrderInfo(o.Data.Data);
            //var r = client.GetOrderTrades(o.Data.Data);
            //var s = client.GetSymbolTrades("ethusdt");

            //var q = socketClient.QueryMarketDepth("ethusdt", "step0");
            //var q1 = socketClient.QueryMarketKlines("ethusdt", "1min");
            //var q2 = socketClient.QueryMarketTrades("ethusdt");
            //var q3 = socketClient.QueryMarketDetails("ethusdta");
            //var q4 = socketClient.QueryAccounts();
            //var q5 = socketClient.QueryOrders(q4.Data.Data[0].Id, "ethusdt", new[] { HuobiOrderState.Submitted, HuobiOrderState.PartiallyFilled }, new[] { HuobiOrderType.LimitSell }, startTime: DateTime.UtcNow.AddDays(-2), limit: 10);
            //var q6 = socketClient.QueryOrders(q4.Data.Data[0].Id, "ethusdt", new[] { HuobiOrderState.Submitted });
            //var q7 = socketClient.QueryOrderDetails(123);
            //var q8 = socketClient.QueryOrderDetails(q6.Data.Data[0].Id);
            var a = "";
            //ProcessSubResponse(socketClient.SubscribeToMarketKlineUpdates("ethusdt", "1min", data =>
            //{
            //    Console.WriteLine($"KLine {data.Timestamp}: " + data.Data.Close);
            //}), "Kline");

            //ProcessSubResponse(socketClient.SubscribeToMarketDepthUpdates("ethusdt", "step0", data =>
            //{
            //    Console.WriteLine($"Depth {data.Timestamp}: " + data.Data.Asks.Count);
            //}), "Depth");

            //ProcessSubResponse(socketClient.SubscribeToMarketTradeUpdates("ethusdt", data =>
            //{
            //    Console.WriteLine($"Trades {data.Timestamp}: " + data.Data.Data[0].Price);
            //}), "Trades");

            //ProcessSubResponse(socketClient.SubscribeToMarketDetailUpdates("ethusdt", data =>
            //{
            //    Console.WriteLine($"Details {data.Timestamp}: " + data.Data.Close);
            //}), "Market details");

            //ProcessSubResponse(socketClient.SubscribeToMarketTickerUpdates(data =>
            //{
            //    Console.WriteLine($"Tickers {data.Timestamp}: " + data.Data[0].Close);
            //}), "Market ticker");

            ProcessSubResponse(socketClient.SubscribeToAccountUpdates(data =>
            {
                Console.WriteLine($"Account {data.Timestamp}: " + data.Data.Event);
            }), "Account");

            //ProcessSubResponse(socketClient.SubscribeToOrderUpdates("*", data =>
            //{
            //    Console.WriteLine($"Order {data.Timestamp}: " + data.Data.Id);
            //}), "Orders");


            Console.ReadLine();
        }

        private static void ProcessSubResponse(CallResult<SocketSubscription> sub, string id)
        {
            if (sub.Success)
            {
                sub.Data.ConnectionLost += () => Console.WriteLine(id +" Connection lost");
                sub.Data.ConnectionRestored += () => Console.WriteLine(id + " Connection restored");
                Console.WriteLine("Subscribed "+id);
            }
            else
                Console.WriteLine("Failed "+id+": " + sub.Error);
        }
    }
}
