using CryptoExchange.Net.SharedApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    public interface IHTXSocketClientSpotApiShared :
        ITickerSocketClient,
        ITickersSocketClient
    {
    }
}
