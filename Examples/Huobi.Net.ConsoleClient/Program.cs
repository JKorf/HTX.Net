using System;
using System.Linq;
using System.Threading.Tasks;
using Huobi.Net.Clients;
using Huobi.Net.Objects;

namespace Huobi.Net.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // REST client
            using (var client = new HuobiClient())
            {
                // Public method
                var marketDetails = await client.SpotApi.ExchangeData.GetSymbolDetails24HAsync("ethusdt");
                if (marketDetails.Success) // Check the success flag for error handling
                    Console.WriteLine($"Got market stats, last price: {marketDetails.Data.ClosePrice}");
                else
                    Console.WriteLine($"Failed to get stats, error: {marketDetails.Error}");

                // Private method
                client.SetApiCredentials(new CryptoExchange.Net.Authentication.ApiCredentials("APIKEY", "APISECRET")); // Change to your credentials
                var accounts = await client.SpotApi.Account.GetAccountsAsync();
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
            await socketClient.SpotStreams.SubscribeToKlineUpdatesAsync("ethusdt", Enums.KlineInterval.FiveMinutes, data =>
            {
                Console.WriteLine("Received kline update. Last price: " + data.Data.ClosePrice);
            });

            Console.ReadLine();
        }
    }
}
