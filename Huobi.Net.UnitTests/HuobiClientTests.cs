using Huobi.Net.Objects;
using Huobi.Net.UnitTests.TestImplementations;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CryptoExchange.Net.Authentication;

namespace Huobi.Net.UnitTests
{
    [TestFixture]
    public class HuobiClientTests
    {
        [TestCase]
        public void GetMarketTickers_Should_RespondWithMarketTickers()
        {
            // arrange
            var expected = new List<HuobiSymbolTick>
            {
                new HuobiSymbolTick
                {
                    Close = 0.1m,
                    Low = 0.2m,
                    Symbol = "BTCETH",
                    Amount = 0.3m,
                    Open = 0.4m,
                    High = 0.5m,
                    Volume = 0.6m,
                    TradeCount = 123
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetTickers();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.Ticks.ToList()[0]));
        }

        [TestCase]
        public void GetMarketTickerMerged_Should_RespondWithMergedTick()
        {
            // arrange
            var expected = new HuobiSymbolTickMerged()
            {
                Close = 0.1m,
                Id = 12345,
                Low = 0.2m,
                Amount = 0.3m,
                Open = 0.4m,
                High = 0.5m,
                Volume = 0.6m,
                TradeCount = 123,
                BestAsk = new HuobiOrderBookEntry() { Quantity = 0.7m, Price = 0.8m },
                BestBid = new HuobiOrderBookEntry() { Quantity = 0.9m, Price = 1.0m },
                Version = 1
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetMergedTickers("BTCETH");

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result.Data));
        }

        [TestCase]
        public void GetMarketKlines_Should_RespondWithKlines()
        {
            // arrange
            var expected = new List<HuobiSymbolData>
            {
                new HuobiSymbolData
                {
                    Close = 0.1m,
                    Low = 0.2m,
                    Amount = 0.3m,
                    Open = 0.4m,
                    High = 0.5m,
                    Volume = 0.6m,
                    TradeCount = 123
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetKlines("BTCETH", HuobiPeriod.FiveMinutes, 10);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.ToList()[0]));
        }

        [TestCase]
        public void GetMarketDepth_Should_RespondWithDepth()
        {
            // arrange
            var expected = new HuobiOrderBook()
            {
                Asks = new List<HuobiOrderBookEntry>()
                {
                    new HuobiOrderBookEntry(){ Quantity = 0.1m, Price = 0.2m}
                },
                Bids = new List<HuobiOrderBookEntry>()
                {
                    new HuobiOrderBookEntry(){ Quantity = 0.3m, Price = 0.4m}
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetOrderBook("BTCETH", 1);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected.Asks.ToList()[0], result.Data.Asks.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(expected.Bids.ToList()[0], result.Data.Bids.ToList()[0]));
        }

        [TestCase]
        public void GetMarketLastTrade_Should_RespondWithLastTrade()
        {
            // arrange
            var expected = new HuobiSymbolTrade()
            {
                Id = 123,
                Timestamp = new DateTime(2018, 1, 1),
                Details = new List<HuobiSymbolTradeDetails>()
                {
                    new HuobiSymbolTradeDetails()
                    {
                        Amount = 0.1m,
                        Id = "12334232",
                        Price = 0.2m,
                        Timestamp = new DateTime(2018, 1, 1),
                        Side = HuobiOrderSide.Buy
                    }
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetLastTrade("BTCETH");

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result.Data, "Details"));
            Assert.IsTrue(TestHelpers.AreEqual(expected.Details.ToList()[0], result.Data.Details.ToList()[0]));
        }

        [TestCase]
        public void GetMarketTradeHistory_Should_RespondWithTradeHistory()
        {
            // arrange
            var expected = new List<HuobiSymbolTrade>
            {
                new HuobiSymbolTrade()
                {
                    Id = 123,
                    Timestamp = new DateTime(2018, 1, 1),
                    Details = new List<HuobiSymbolTradeDetails>()
                    {
                        new HuobiSymbolTradeDetails()
                        {
                            Amount = 0.1m,
                            Id = "12334232",
                            Price = 0.2m,
                            Timestamp = new DateTime(2018, 1, 1),
                            Side = HuobiOrderSide.Buy
                        }
                    }
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetTradeHistory("BTCETH", 1);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.ToList()[0], "Details"));
            Assert.IsTrue(TestHelpers.AreEqual(expected[0].Details.ToList()[0], result.Data.ToList()[0].Details.ToList()[0]));
        }

        [TestCase]
        public void GetMarketDetails24H_Should_RespondWithDetails24H()
        {
            // arrange
            var expected = new HuobiSymbolData
            {
                Close = 0.1m,
                Low = 0.2m,
                Amount = 0.3m,
                Open = 0.4m,
                High = 0.5m,
                Volume = 0.6m,
                TradeCount = 123
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetSymbolDetails24H("BTCETH");

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result.Data, "Details"));
        }

        [TestCase]
        public void GetSymbols_Should_RespondWithSymbolList()
        {
            // arrange
            var expected = new List<HuobiSymbol>
            {
                new HuobiSymbol()
                {
                    Symbol = "BTCETH",
                    AmountPrecision = 8,
                    BaseCurrency = "BTC",
                    PricePrecision = 8,
                    QuoteCurrency = "ETH",
                    SymbolPartition = "INOVATION"
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetSymbols();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.ToList()[0]));
        }

        [TestCase]
        public void GetCurrencies_Should_RespondWithCurrenciesList()
        {
            // arrange
            var expected = new List<string>
            {
                "BTCETH",
                "BTCUSD"
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetCurrencies();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(expected[0] == result.Data.ToList()[0]);
            Assert.IsTrue(expected[1] == result.Data.ToList()[1]);
        }

        [TestCase]
        public void GetAccounts_Should_RespondWithAccountsList()
        {
            // arrange
            var expected = new List<HuobiAccount>
            {
                new HuobiAccount()
                {
                    Id = 123,
                    Type = HuobiAccountType.Margin,
                    State = HuobiAccountState.Working
                }
            };

            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetAccounts();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.ToList()[0]));
        }

        [TestCase]
        public void GetAccountBalances_Should_RespondWithAccountBalanceList()
        {
            // arrange
            var expected = new HuobiAccountBalances()
            {
                Data = new List<HuobiBalance>
                {
                    new HuobiBalance()
                    {
                        Type = HuobiBalanceType.Frozen,
                        Balance = 0.1m,
                        Currency = "ETH"
                    }
                }
            };

            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetBalances(123);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected.Data.ToList()[0], result.Data.ToList()[0]));
        }

        [TestCase]
        public void PlaceOrder_Should_RespondWithPlacedOrderId()
        {
            // arrange
            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(123, true));

            // act
            var result = client.PlaceOrder(123, "BTCETH", HuobiOrderType.LimitBuy, 1, 2);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(123 == result.Data);
        }

        [TestCase]
        public void GetOpenOrders_Should_RespondWithOpenOrders()
        {
            // arrange
            var expected = new List<HuobiOpenOrder>()
            {
                new HuobiOpenOrder()
                {
                    Amount = 0.1m,
                    Type = HuobiOrderType.LimitBuy,
                    Id = 123,
                    Price = 0.2m,
                    Symbol = "BTCETH",
                    State = HuobiOrderState.Submitted,
                    AccountId = 1234,
                    CreatedAt = new DateTime(2018, 1, 1),
                    FinishedAt = new DateTime(2019, 1, 1),
                    Source = "API",
                    FilledAmount = 1.1m,
                    FilledFees = 1.2m,
                    FilledCashAmount = 1.3m
                }
            };

            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetOpenOrders();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.ToList()[0]));
        }

        [TestCase]
        public void CancelOrder_Should_RespondWithCanceledOrderId()
        {
            // arrange
            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(123, true));

            // act
            var result = client.CancelOrder(123);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(123 == result.Data);
        }

        [TestCase]
        public void CancelOrders_Should_RespondWithCancelResults()
        {
            // arrange
            var expected = new HuobiBatchCancelResult()
            {
                Successful = new long[] { 123 },
                Failed = new[]
                {
                    new HuobiFailedCancelResult()
                    {
                        ErrorCode = "123",
                        ErrorMessage = "Fail",
                        OrderId = 1234
                    }
                }
            };

            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.CancelOrders(new long[] { 123, 1234 });

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(expected.Successful.ToList()[0] == result.Data.Successful.ToList()[0]);
            Assert.IsTrue(TestHelpers.AreEqual(expected.Failed.ToList()[0], result.Data.Failed.ToList()[0]));
        }

        [TestCase]
        public void GetOrderInfo_Should_RespondWithOrderInfo()
        {
            // arrange
            var expected = new HuobiOrder()
            {
                Amount = 0.1m,
                Type = HuobiOrderType.LimitBuy,
                Id = 123,
                Price = 0.2m,
                Symbol = "BTCETH",
                State = HuobiOrderState.Submitted,
                AccountId = 1234,
                CreatedAt = new DateTime(2018, 1, 1),
                Source = "API",
                FilledAmount = 1.1m,
                FilledCashAmount = 1.2m,
                FilledFees = 1.3m
            };

            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetOrderInfo(123);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result.Data));
        }

        [TestCase]
        public void GetOrderTrades_Should_RespondWithOrderTrades()
        {
            // arrange
            var expected = new List<HuobiOrderTrade>
            {
                new HuobiOrderTrade()
                {
                    Id = 123,
                    Price = 0.1m,
                    Symbol = "BTCETH",
                    Source = "API",
                    OrderId = 1234,
                    CreatedAt = new DateTime(2018, 1, 1),
                    OrderType = HuobiOrderType.LimitSell,
                    FilledAmount = 0.2m,
                    FilledFees = 0.3m,
                    MatchId = 125
                }
            };

            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetOrderTrades(123);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.ToList()[0]));
        }

        [TestCase]
        public void GetOrders_Should_RespondWithOrders()
        {
            // arrange
            var expected = new List<HuobiOrder>()
            {
                new HuobiOrder()
                {
                    Amount = 0.1m,
                    Type = HuobiOrderType.LimitBuy,
                    Id = 123,
                    Price = 0.2m,
                    Symbol = "BTCETH",
                    State = HuobiOrderState.Submitted,
                    AccountId = 1234,
                    CreatedAt = new DateTime(2018, 1, 1),
                    Source = "API",
                    FilledAmount = 1.1m,
                    FilledCashAmount = 1.2m,
                    FilledFees = 1.3m
                }
            };

            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetOrders(new[] { HuobiOrderState.Submitted }, "BTCETH");

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.ToList()[0]));
        }

        [TestCase]
        public void GetSymbolTrades_Should_RespondWithSymbolTrades()
        {
            // arrange
            var expected = new List<HuobiOrderTrade>()
            {
                new HuobiOrderTrade()
                {
                    Id = 123,
                    Price = 0.1m,
                    Symbol = "BTCETH",
                    Source = "API",
                    OrderId = 1234,
                    CreatedAt = new DateTime(2018, 1, 1),
                    OrderType = HuobiOrderType.LimitSell,
                    FilledAmount = 0.2m,
                    FilledFees = 0.3m,
                    MatchId = 125
                }
            };

            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetSymbolTrades(symbol: "BTCETH", types: new[] { HuobiOrderType.LimitBuy });

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.ToList()[0]));
        }

        [Test]
        public void SigningString_Should_GiveCorrectSignResult()
        {
            // arrange
            var authProvider = new HuobiAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"), false);

            // act
            var parameters = authProvider.AddAuthenticationToParameters("http://api.test.com/somepath/test", HttpMethod.Get, new Dictionary<string, object>()
            {
                { "Timestamp", new DateTime(2018, 1, 1).ToString("yyyy-MM-ddTHH:mm:ss") }
            }, true, CryptoExchange.Net.Objects.PostParameters.InBody, CryptoExchange.Net.Objects.ArrayParametersSerialization.MultipleValues);

            // assert
            Assert.AreEqual(parameters["Signature"], "5/vfYFw3cHwp20QWtv6DzTHDxBpHzNSU6Rv3p7Up/TI=");
        }

        [TestCase]
        public void ReceivingErrorResponse_Should_FailCall()
        {
            // arrange
            var client = TestHelpers.CreateAuthResponseClient("{{\"status\": \"error\", \"err-code\": \"Error!\", \"err-msg\": \"ErrorMessage\"}}");

            // act
            var result = client.GetCurrencies();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Error.ToString().Contains("Error!"));
            Assert.IsTrue(result.Error.ToString().Contains("ErrorMessage"));
        }

        [TestCase]
        public void ReceivingHttpErrorResponse_Should_FailCall()
        {
            // arrange
            var client = TestHelpers.CreateAuthResponseClient("Error message", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = client.GetCurrencies();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Error.ToString().Contains("Error message"));
        }


        public string SerializeExpected<T>(T data, bool tick)
        {
            return $"{{\"status\": \"ok\", {(tick ? "tick" : "data")}: {JsonConvert.SerializeObject(data)}}}";
        }

        [TestCase("BTCUSDT", true)]
        [TestCase("NANOUSDT", true)]
        [TestCase("NANOBTC", true)]
        [TestCase("ETHBTC", true)]
        [TestCase("BEETC", true)]
        [TestCase("BEEC", true)]
        [TestCase("BEC", false)]
        [TestCase("NANOUSDTD", true)]
        [TestCase("BTC-USDT", false)]
        [TestCase("BTC-USD", false)]
        public void CheckValidHuobiSymbol(string symbol, bool isValid)
        {
            if (isValid)
                Assert.DoesNotThrow(() => symbol.ValidateHuobiSymbol());
            else
                Assert.Throws(typeof(ArgumentException), () => symbol.ValidateHuobiSymbol());
        }
    }
}
