using System;
using System.Linq;
using Huobi.Net.Objects;

namespace Huobi.Net.ConsoleClient
{
    class Program
    {
        static async void Main(string[] args)
        {
            // REST client
            using (var client = new HuobiClient())
            {
                // Public method
                var marketDetails = await client.GetSymbolDetails24HAsync("ethusdt");
                if (marketDetails.Success) // Check the success flag for error handling
                    Console.WriteLine($"Got market stats, last price: {marketDetails.Data.Close}");
                else
                    Console.WriteLine($"Failed to get stats, error: {marketDetails.Error}");

                // Private method
                client.SetApiCredentials("APIKEY", "APISECRET"); // Change to your credentials
                var accounts = await client.GetAccountsAsync();
                if (accounts.Success) // Check the success flag for error handling
                    Console.WriteLine($"Got account list, account id #1: {accounts.Data.First().Id}");
                else
                    Console.WriteLine($"Failed to get account list, error: {accounts.Error}");
            }

            Console.WriteLine("");
            Console.WriteLine("Press enter to continue to the socket client..");
            Console.ReadLine();

            // Socket client
            var socketClient = new HuobiSocketClient();
            await socketClient.SubscribeToKlineUpdatesAsync("ethusdt", HuobiPeriod.FiveMinutes, data =>
            {
                Console.WriteLine("Received kline update. Last price: " + data.Data.Close);
            });

            Console.ReadLine();
        }
    }
}
