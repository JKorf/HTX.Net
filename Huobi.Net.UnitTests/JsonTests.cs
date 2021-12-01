using Huobi.Net;
using Huobi.Net.Interfaces;
using Huobi.Net.Interfaces.Clients.Rest.Spot;
using Huobi.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Objects;

namespace Huobi.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IHuobiClient> _comparer = new JsonToObjectComparer<IHuobiClient>((json) => TestHelpers.CreateResponseClient(json, new HuobiClientOptions()
        { 
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "123"), 
            OutputOriginalData = true,
            SpotApiOptions = new CryptoExchange.Net.Objects.RestApiClientOptions
            {
                RateLimiters = new List<IRateLimiter>()
            }
        }));

        [Test]
        public async Task ValidateAccountCalls()
        {   
            await _comparer.ProcessSubject("DataResponses/Account", c => c.SpotApi.Account,
                useNestedObjectPropertyForCompare: new Dictionary<string, string> 
                {
                },
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                    { "GetBalancesAsync", "list" }
                },
                useNestedJsonPropertyForAllCompare: new List<string> { "data" }
                );
        }

        [Test]
        public async Task ValidateTradingCalls()
        {
            await _comparer.ProcessSubject("DataResponses/Trading", c => c.SpotApi.Trading,
                useNestedObjectPropertyForCompare: new Dictionary<string, string>
                {
                },
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                },
                useNestedJsonPropertyForAllCompare: new List<string> { "data" }
                );
        }

        [Test]
        public async Task ValidateExchangeDataDataCalls()
        {
            await _comparer.ProcessSubject("DataResponses/ExchangeData", c => c.SpotApi.ExchangeData,
                useNestedObjectPropertyForCompare: new Dictionary<string, string>
                {
                    { "GetTickersAsync", "Ticks" },
                },
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                },
                useNestedJsonPropertyForAllCompare: new List<string> { "data" }
                );
        }

        [Test]
        public async Task ValidateExchangeDataTickCalls()
        {
            await _comparer.ProcessSubject("TickResponses", c => c.SpotApi.ExchangeData,
                parametersToSetNull: new [] { "limit" },
                useNestedObjectPropertyForCompare: new Dictionary<string, string>
                {
                },
                useNestedJsonPropertyForAllCompare: new List<string> { "tick" }
                );
        }

    }
}
