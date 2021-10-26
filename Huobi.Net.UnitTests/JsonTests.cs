using Huobi.Net;
using Huobi.Net.Interfaces;
using Huobi.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huobi.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IHuobiClient> _comparer = new JsonToObjectComparer<IHuobiClient>((json) => TestHelpers.CreateResponseClient(json, new HuobiClientOptions()
        { ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "123"), OutputOriginalData = true }));

        [Test]
        public async Task ValidateBaseDataCalls()
        {   
            await _comparer.ProcessSubject("DataResponses", c => c,
                useNestedObjectPropertyForCompare: new Dictionary<string, string> 
                {
                    { "GetTickersAsync", "Ticks" },
                },
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                    { "GetBalancesAsync", "list" }
                },
                useNestedJsonPropertyForAllCompare: new List<string> { "data" }
                );
        }

        [Test]
        public async Task ValidateBaseTickCalls()
        {
            await _comparer.ProcessSubject("TickResponses", c => c,
                parametersToSetNull: new [] { "limit" },
                useNestedObjectPropertyForCompare: new Dictionary<string, string>
                {
                },
                useNestedJsonPropertyForAllCompare: new List<string> { "tick" }
                );
        }

    }
}
