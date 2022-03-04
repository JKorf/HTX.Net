using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Huobi.Net.UnitTests
{
    internal class JsonSocketTests
    {
        [Test]
        public async Task ValidateKlineUpdateStreamJson()
        {
            await TestFileToObject<HuobiDataEvent<HuobiKline>>(@"JsonResponses/Socket/KlineUpdate.txt");
        }

        [Test]
        public async Task ValidatePartialBookUpdateStreamJson()
        {
            await TestFileToObject<HuobiDataEvent<HuobiOrderBook>>(@"JsonResponses/Socket/PartialOrderBookUpdate.txt");
        }

        [Test]
        public async Task ValidateTradeUpdateStreamJson()
        {
            await TestFileToObject<HuobiDataEvent<HuobiSymbolTrade>>(@"JsonResponses/Socket/TradeUpdate.txt");
        }

        [Test]
        public async Task ValidateSymbolDetailUpdateStreamJson()
        {
            await TestFileToObject<HuobiDataEvent<HuobiSymbolDetails>>(@"JsonResponses/Socket/SymbolDetailUpdate.txt");
        }

        [Test]
        public async Task ValidateTickerUpdateStreamJson()
        {
            await TestFileToObject<HuobiDataEvent<HuobiSymbolTick>>(@"JsonResponses/Socket/TickerUpdate.txt");
        }

        [Test]
        public async Task ValidateBestOfferUpdateStreamJson()
        {
            await TestFileToObject<HuobiDataEvent<HuobiBestOffer>>(@"JsonResponses/Socket/BestOfferUpdate.txt");
        }

        [Test]
        public async Task ValidateOrderUpdateStreamJson()
        {
            await TestFileToObject<HuobiTriggerFailureOrderUpdate>(@"JsonResponses/Socket/OrderUpdate1.txt");
            await TestFileToObject<HuobiOrderUpdate>(@"JsonResponses/Socket/OrderUpdate2.txt", new List<string> { "orderSide" });
            await TestFileToObject<HuobiSubmittedOrderUpdate>(@"JsonResponses/Socket/OrderUpdate3.txt");
            await TestFileToObject<HuobiMatchedOrderUpdate>(@"JsonResponses/Socket/OrderUpdate4.txt");
            await TestFileToObject<HuobiCanceledOrderUpdate>(@"JsonResponses/Socket/OrderUpdate5.txt");
        }

        [Test]
        public async Task ValidateAccountUpdateStreamJson()
        {
            await TestFileToObject<HuobiAccountUpdate>(@"JsonResponses/Socket/AccountUpdate.txt");
        }

        [Test]
        public async Task ValidateOrderDetailsUpdateStreamJson()
        {
            await TestFileToObject<HuobiTradeUpdate>(@"JsonResponses/Socket/OrderDetailsUpdate1.txt");
        }
        

        private static async Task TestFileToObject<T>(string filePath, List<string> ignoreProperties = null)
        {
            var listener = new EnumValueTraceListener();
            Trace.Listeners.Add(listener);
            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string json;
            try
            {
                var file = File.OpenRead(Path.Combine(path, filePath));
                using var reader = new StreamReader(file);
                json = await reader.ReadToEndAsync();
            }
            catch (FileNotFoundException)
            {
                throw;
            }

            var result = JsonConvert.DeserializeObject<T>(json);
            JsonToObjectComparer<IHuobiSocketClient>.ProcessData("", result, json, ignoreProperties: new Dictionary<string, List<string>>
            {
                { "", ignoreProperties ?? new List<string>() }
            });
            Trace.Listeners.Remove(listener);
        }
    }

    internal class EnumValueTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            if (message.Contains("Cannot map"))
                throw new Exception("Enum value error: " + message);
        }

        public override void WriteLine(string message)
        {
            if (message.Contains("Cannot map"))
                throw new Exception("Enum value error: " + message);
        }
    }
}
