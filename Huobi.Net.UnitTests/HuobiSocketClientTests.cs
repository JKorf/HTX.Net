using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CryptoExchange.Net;
using Huobi.Net.Objects;
using Huobi.Net.Objects.SocketObjects;
using Huobi.Net.UnitTests.TestImplementations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Huobi.Net.UnitTests
{
    [TestFixture]
    public class HuobiSocketClientTests
    {
        [Test]
        public void SubscribeV1_Should_SucceedIfSubbedResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            // act
            var subTask = client.SubscribeToMarketDepthUpdatesAsync("test", 1, test => { });
            socket.InvokeMessage($"{{\"subbed\": \"test\", \"id\":\"{BaseClient.LastId}\", \"status\": \"ok\"}}");
            var subResult = subTask.Result;

            // assert
            Assert.IsTrue(subResult.Success);
        }

        [Test]
        public void SubscribeV1_Should_FailIfNoResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket, new HuobiSocketClientOptions()
            {
                SocketResponseTimeout = TimeSpan.FromMilliseconds(10)
            });

            // act
            var subTask = client.SubscribeToMarketDepthUpdatesAsync("test", 1, test => { });
            var subResult = subTask.Result;

            // assert
            Assert.IsFalse(subResult.Success);
        }

        [Test]
        public void SubscribeV1_Should_FailIfErrorResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            // act
            var subTask = client.SubscribeToMarketDepthUpdatesAsync("test", 1, test => { });
            socket.InvokeMessage($"{{\"status\": \"error\", \"id\": \"{BaseClient.LastId}\", \"err-code\": \"Fail\", \"err-msg\": \"failed\"}}");
            var subResult = subTask.Result;

            // assert
            Assert.IsFalse(subResult.Success);
        }

        [Test]
        public void SubscribeToDepthUpdates_Should_TriggerWithDepthUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            HuobiMarketDepth result = null;
            var subTask = client.SubscribeToMarketDepthUpdatesAsync("test", 1, test => result = test);
            socket.InvokeMessage($"{{\"subbed\": \"test\", \"status\": \"ok\", \"id\": \"{BaseClient.LastId}\"}}");
            var subResult = subTask.Result;

            var expected =  new HuobiMarketDepth()
            {
                Asks = new List<HuobiOrderBookEntry>()
                {
                    new HuobiOrderBookEntry() {Quantity = 0.1m, Price = 0.2m}
                },
                Bids = new List<HuobiOrderBookEntry>()
                {
                    new HuobiOrderBookEntry() {Quantity = 0.3m, Price = 0.4m}
                }
            };

            // act
            socket.InvokeMessage(SerializeExpected("market.test.depth.step1", expected));

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected.Asks[0], result.Asks[0]));
            Assert.IsTrue(TestHelpers.AreEqual(expected.Bids[0], result.Bids[0]));
        }

        [Test]
        public void SubscribeToDetailUpdates_Should_TriggerWithDetailUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            HuobiMarketData result = null;
            var subTask = client.SubscribeToMarketDetailUpdatesAsync("test", test => result = test);
            socket.InvokeMessage($"{{\"subbed\": \"test\", \"id\": \"{BaseClient.LastId}\", \"status\": \"ok\"}}");
            var subResult = subTask.Result;

            var expected = new HuobiMarketData()
            {
                Amount = 0.1m,
                Close = 0.2m,
                Low = 0.3m,
                High = 0.4m,
                Volume = 0.5m,
                Open = 0.6m,
                TradeCount = 12
            };

            // act
            socket.InvokeMessage(SerializeExpected("market.test.detail", expected));

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result));
        }

        [Test]
        public void SubscribeToKlineUpdates_Should_TriggerWithKlineUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            HuobiMarketData result = null;
            var subTask = client.SubscribeToMarketKlineUpdatesAsync("test", HuobiPeriod.FiveMinutes, test => result = test);
            socket.InvokeMessage($"{{\"subbed\": \"test\", \"id\": \"{BaseClient.LastId}\", \"status\": \"ok\"}}");
            var subResult = subTask.Result;

            var expected = new HuobiMarketData()
            {
                Amount = 0.1m,
                Close = 0.2m,
                Low = 0.3m,
                High = 0.4m,
                Volume = 0.5m,
                Open = 0.6m,
                TradeCount = 12
            };

            // act
            socket.InvokeMessage(SerializeExpected("market.test.kline.5min", expected));

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result));
        }

        [Test]
        public void SubscribeToTickerUpdates_Should_TriggerWithTickerUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            HuobiMarketTicks result = null;
            var subTask = client.SubscribeToMarketTickerUpdatesAsync(test => result = test);
            socket.InvokeMessage($"{{\"subbed\": \"test\", \"id\": \"{BaseClient.LastId}\", \"status\": \"ok\"}}");
            var subResult = subTask.Result;

            var expected = new List<HuobiMarketTick>
            {
                new HuobiMarketTick()
                {
                    Amount = 0.1m,
                    Close = 0.2m,
                    Low = 0.3m,
                    High = 0.4m,
                    Volume = 0.5m,
                    Open = 0.6m,
                    TradeCount = 12
                }
            };

            // act
            socket.InvokeMessage(SerializeExpected("market.tickers", expected));

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Ticks[0]));
        }

        [Test]
        public void SubscribeToTradeUpdates_Should_TriggerWithTradeUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            HuobiMarketTrade result = null;
            var subTask = client.SubscribeToMarketTradeUpdatesAsync("ethusdt", test => result = test);
            socket.InvokeMessage($"{{\"subbed\": \"test\", \"id\": \"{BaseClient.LastId}\", \"status\": \"ok\"}}");
            var subResult = subTask.Result;

            var expected = 
                new HuobiMarketTrade()
                {
                    Id = 123,
                    Timestamp = new DateTime(2018, 1, 1),
                    Details = new List<HuobiMarketTradeDetails>()
                    {
                        new HuobiMarketTradeDetails()
                        {
                            Id = "123",
                            Amount = 0.1m,
                            Price = 0.2m,
                            Timestamp = new DateTime(2018,2,1),
                            Side = HuobiOrderSide.Buy
                        }
                    }
            };

            // act
            socket.InvokeMessage(SerializeExpected("market.ethusdt.trade.detail", expected));

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result, "Details"));
            Assert.IsTrue(TestHelpers.AreEqual(expected.Details[0], result.Details[0]));
        }

        [Test]
        public void SubscribeToAccountUpdates_Should_TriggerWithAccountUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateAuthenticatedSocketClient(socket);

            HuobiAccountEvent result = null;
            var subTask = client.SubscribeToAccountUpdatesAsync(test => result = test);
            socket.InvokeMessage("{\"op\": \"auth\"}");
            Thread.Sleep(10);
            socket.InvokeMessage($"{{\"op\": \"sub\", \"cid\": \"{BaseClient.LastId}\"}}");
            var subResult = subTask.Result;

            var expected = new HuobiAccountEvent()
            {
                Event = HuobiAccountEventType.Other,
                BalanceChanges = new List<HuobiBalanceChange>()
                {
                    new HuobiBalanceChange()
                    {
                        Type = HuobiBalanceType.Frozen,
                        AccountId = 123,
                        Currency = "eth",
                        Balance = 0.1m
                    }
                }
            };

            // act
            socket.InvokeMessage(SerializeExpectedAuth("accounts", expected));

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result, "BalanceChanges"));
            Assert.IsTrue(TestHelpers.AreEqual(expected.BalanceChanges[0], result.BalanceChanges[0]));
        }

        [Test]
        public void SubscribeToOrderUpdates_Should_TriggerWithOrderUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateAuthenticatedSocketClient(socket);

            HuobiOrderUpdate result = null;
            var subTask = client.SubscribeToOrderUpdatesAsync(test => result = test);
            socket.InvokeMessage("{\"op\": \"auth\"}");
            Thread.Sleep(10);
            socket.InvokeMessage($"{{\"op\": \"sub\", \"cid\": \"{BaseClient.LastId}\"}}");
            var subResult = subTask.Result;

            var expected = new HuobiOrderUpdate()
            {
                Id = 123,
                Amount = 0.1m,
                Type = HuobiOrderType.IOCBuy,
                Price = 0.2m,
                Symbol = "ethusdt",
                State = HuobiOrderState.Canceled,
                Source = "API",
                AccountId = 1543,
                FilledAmount = 0.3m,
                CreatedAt = new DateTime(2018, 1, 1),
                FilledFees = 0.4m,
                FilledCashAmount = 0.5m,
            };

            // act
            socket.InvokeMessage(SerializeExpectedAuth("orders.*", expected));

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result));
        }

        [Test]
        public void QueryingAccountInfo_Should_ReturnAccountInfo()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateAuthenticatedSocketClient(socket);

            var expected = new List<HuobiAccountBalances>()
            {
                new HuobiAccountBalances()
                {
                    Id = 123,
                    State = HuobiAccountState.Locked,
                    Type = HuobiAccountType.Margin,
                    Data = new List<HuobiBalance>()
                    {
                        new HuobiBalance()
                        {
                            Type = HuobiBalanceType.Frozen,
                            Balance = 0.1m,
                            Currency = "eth"
                        }
                    }
                }
            };

            // act
            var subTask = client.QueryAccountsAsync();
            socket.InvokeMessage("{\"op\": \"auth\"}");
            Thread.Sleep(10);
            socket.InvokeMessage(SerializeExpectedQuery(expected));
            var subResult = subTask.Result;

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], subResult.Data[0], "Data"));
            Assert.IsTrue(TestHelpers.AreEqual(expected[0].Data[0], subResult.Data[0].Data[0]));
        }

        [Test]
        public void QueryingOrderDetails_Should_ReturnOrderDetails()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateAuthenticatedSocketClient(socket);

            var expected = new HuobiOrder()
            {
                Id = 123,
                Amount = 0.1m,
                Type = HuobiOrderType.IOCBuy,
                Price = 0.2m,
                Symbol = "ethusdt",
                State = HuobiOrderState.Canceled,
                Source = "API",
                AccountId = 1543,
                FilledAmount = 0.3m,
                CreatedAt = new DateTime(2018, 1, 1),
                FilledFees = 0.4m,
                CanceledAt = new DateTime(2018, 1, 2),
                FilledCashAmount = 0.5m,
                FinishedAt = new DateTime(2018, 1, 3)
            };

            // act
            var subTask = client.QueryOrderDetailsAsync(123);
            socket.InvokeMessage("{\"op\": \"auth\"}");
            Thread.Sleep(10);
            socket.InvokeMessage(SerializeExpectedQuery(expected));
            var subResult = subTask.Result;

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, subResult.Data));
        }

        [Test]
        public void QueryingOrders_Should_ReturnOrders()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateAuthenticatedSocketClient(socket);

            var expected = new List<HuobiOrder>
            {
                new HuobiOrder()
                {
                    Id = 123,
                    Amount = 0.1m,
                    Type = HuobiOrderType.IOCBuy,
                    Price = 0.2m,
                    Symbol = "ethusdt",
                    State = HuobiOrderState.Canceled,
                    Source = "API",
                    AccountId = 1543,
                    FilledAmount = 0.3m,
                    CreatedAt = new DateTime(2018, 1, 1),
                    FilledFees = 0.4m,
                    CanceledAt = new DateTime(2018, 1, 2),
                    FilledCashAmount = 0.5m,
                    FinishedAt = new DateTime(2018, 1, 3)
                }
            };

            // act
            var subTask = client.QueryOrdersAsync(123, "ethusdt", new [] { HuobiOrderState.Canceled });
            socket.InvokeMessage("{\"op\": \"auth\"}");
            Thread.Sleep(10);
            socket.InvokeMessage(SerializeExpectedQuery(expected));
            var subResult = subTask.Result;

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], subResult.Data[0]));
        }

        [Test]
        public void SubscribeV2_Should_SucceedIfSubbedResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateAuthenticatedSocketClient(socket);

            // act
            var subTask = client.SubscribeToAccountUpdatesAsync(test => { });
            socket.InvokeMessage("{\"op\": \"auth\"}");
            Thread.Sleep(10);
            socket.InvokeMessage($"{{\"op\": \"sub\", \"cid\": \"{BaseClient.LastId}\"}}");
            var subResult = subTask.Result;

            // assert
            Assert.IsTrue(subResult.Success);
        }

        [Test]
        public void SubscribeV2_Should_FailIfAuthErrorResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            // act
            var subTask = client.SubscribeToAccountUpdatesAsync(test => { });
            socket.InvokeMessage("{ \"op\": \"auth\", \"status\": \"error\", \"err-code\": 1, \"err-msg\": \"failed\"}");
            var subResult = subTask.Result;

            // assert
            Assert.IsFalse(subResult.Success);
        }

        [Test]
        public void SubscribeV2_Should_FailIfErrorResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            // act
            var subTask = client.SubscribeToAccountUpdatesAsync(test => { });
            socket.InvokeMessage("{\"op\": \"auth\"}");
            Thread.Sleep(10);
            socket.InvokeMessage($"{{\"op\": \"sub\", \"cid\": \"{BaseClient.LastId}\", \"status\": \"error\", \"err-code\": 1, \"err-msg\": \"failed\"}}");
            var subResult = subTask.Result;

            // assert
            Assert.IsFalse(subResult.Success);
        }

        [Test]
        public void SubscribeV2_Should_FailIfNoResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket, new HuobiSocketClientOptions()
            {
                SocketResponseTimeout = TimeSpan.FromMilliseconds(10)
            });

            // act
            var subTask = client.SubscribeToAccountUpdatesAsync(test => { });
            var subResult = subTask.Result;

            // assert
            Assert.IsFalse(subResult.Success);
        }

        public string SerializeExpected<T>(string channel, T data)
        {
            return $"{{\"ch\": \"{channel}\", \"data\": {JsonConvert.SerializeObject(data)}}}";
        }

        public string SerializeExpectedAuth<T>(string topic, T data)
        {
            return $"{{\"op\": \"notify\", \"topic\": \"{topic}\", \"data\": {JsonConvert.SerializeObject(data)}}}";
        }

        public string SerializeExpectedQuery<T>(T data)
        {
            return $"{{\"op\": \"req\", \"cid\": \"{BaseClient.LastId}\", \"data\": {JsonConvert.SerializeObject(data)}}}";
        }
    }
}
