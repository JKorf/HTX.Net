using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    public interface IHTXSocketClientSpotApiShared :
        ITickerSocketClient,
        ITickersSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IBalanceSocketClient,
        ISpotOrderSocketClient,
        IUserTradeSocketClient
    {
    }
}
