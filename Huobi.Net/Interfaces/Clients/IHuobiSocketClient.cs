using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket;

namespace Huobi.Net.Interfaces.Clients.Socket
{
    /// <summary>
    /// Interface for the Huobi socket client
    /// </summary>
    public interface IHuobiSocketClient: ISocketClient
    {
        public IHuobiSocketClientSpotMarket SpotStreams { get; }

    }
}