using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects
{
    internal class HuobiApiResponseV2<T>
    {
        public int Code { get; set; }
        public string Message { get; set; } = "";
#pragma warning disable 8618
        public T Data { get; set; }
#pragma warning restore 8618
    }
}
