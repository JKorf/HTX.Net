using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
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
            var k = client.GetAccounts();


            Console.ReadLine();
        }
    }
}
