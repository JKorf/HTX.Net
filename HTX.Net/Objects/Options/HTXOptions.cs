using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Options
{
    /// <summary>
    /// HTX options
    /// </summary>
    public class HTXOptions : LibraryOptions<HTXRestOptions, HTXSocketOptions, ApiCredentials, HTXEnvironment>
    {
    }
}
