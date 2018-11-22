using Huobi.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects
{
    public class HuobiAccount
    {
        public long Id { get; set; }
        [JsonConverter(typeof(AccountStateConverter))]
        public HuobiAccountState State { get; set; }
        [JsonConverter(typeof(AccountTypeConverter))]
        public HuobiAccountType Type { get; set; }
    }
}
