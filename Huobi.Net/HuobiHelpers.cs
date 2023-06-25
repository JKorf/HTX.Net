using Huobi.Net.Clients;
using Huobi.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;
using Huobi.Net.Objects.Options;
using Huobi.Net.Interfaces;
using Huobi.Net.SymbolOrderBooks;

namespace Huobi.Net
{
    /// <summary>
    /// Helpers for Huobi
    /// </summary>
    public static class HuobiHelpers
    {
        /// <summary>
        /// Add the IHuobiClient and IHuobiSocketClient to the sevice collection so they can be injected
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="defaultRestOptionsDelegate">Set default options for the rest client</param>
        /// <param name="defaultSocketOptionsDelegate">Set default options for the socket client</param>
        /// <param name="socketClientLifeTime">The lifetime of the IHuobiSocketClient for the service collection. Defaults to Singleton.</param>
        /// <returns></returns>
        public static IServiceCollection AddHuobi(
            this IServiceCollection services,
            Action<HuobiRestOptions>? defaultRestOptionsDelegate = null,
            Action<HuobiSocketOptions>? defaultSocketOptionsDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            var restOptions = HuobiRestOptions.Default.Copy();

            if (defaultRestOptionsDelegate != null)
            {
                defaultRestOptionsDelegate(restOptions);
                HuobiRestClient.SetDefaultOptions(defaultRestOptionsDelegate);
            }

            if (defaultSocketOptionsDelegate != null)
                HuobiSocketClient.SetDefaultOptions(defaultSocketOptionsDelegate);

            services.AddHttpClient<IHuobiRestClient, HuobiRestClient>(options =>
            {
                options.Timeout = restOptions.RequestTimeout;
            }).ConfigurePrimaryHttpMessageHandler(() => {
                var handler = new HttpClientHandler();
                if (restOptions.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{restOptions.Proxy.Host}:{restOptions.Proxy.Port}"),
                        Credentials = restOptions.Proxy.Password == null ? null : new NetworkCredential(restOptions.Proxy.Login, restOptions.Proxy.Password)
                    };
                }
                return handler;
            });

            services.AddSingleton<IHuobiOrderBookFactory, HuobiOrderBookFactory>();
            services.AddTransient<IHuobiRestClient, HuobiRestClient>();
            if (socketClientLifeTime == null)
                services.AddSingleton<IHuobiSocketClient, HuobiSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(IHuobiSocketClient), typeof(HuobiSocketClient), socketClientLifeTime.Value));
            return services;
        }

        /// <summary>
        /// Validate the string is a valid Huobi symbol.
        /// </summary>
        /// <param name="symbolString">string to validate</param>
        public static string ValidateHuobiSymbol(this string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
                throw new ArgumentException("Symbol is not provided");
            symbolString = symbolString.ToLower(CultureInfo.InvariantCulture);
            if (!Regex.IsMatch(symbolString, "^(([a-z]|[0-9]){4,})$"))
                throw new ArgumentException($"{symbolString} is not a valid Huobi symbol. Should be [QuoteAsset][BaseAsset], e.g. ETHBTC");
            return symbolString;
        }
    }
}
