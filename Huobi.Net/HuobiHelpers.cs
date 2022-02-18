using Huobi.Net.Clients;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Objects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

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
        /// <param name="defaultOptionsCallback">Set default options for the client</param>
        /// <param name="socketClientLifeTime">The lifetime of the IHuobiSocketClient for the service collection. Defaults to Scoped.</param>
        /// <returns></returns>
        public static IServiceCollection AddHuobi(
            this IServiceCollection services, 
            Action<HuobiClientOptions, HuobiSocketClientOptions>? defaultOptionsCallback = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            if (defaultOptionsCallback != null)
            {
                var options = new HuobiClientOptions();
                var socketOptions = new HuobiSocketClientOptions();
                defaultOptionsCallback?.Invoke(options, socketOptions);

                HuobiClient.SetDefaultOptions(options);
                HuobiSocketClient.SetDefaultOptions(socketOptions);
            }

            services.AddTransient<IHuobiClient, HuobiClient>();
            if (socketClientLifeTime == null)
                services.AddScoped<IHuobiSocketClient, HuobiSocketClient>();
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
