using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Clients;
using Huobi.Net.Interfaces;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Objects.Options;
using Huobi.Net.SymbolOrderBooks;
using System;
using System.Net;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
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

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddTransient<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<IHuobiOrderBookFactory, HuobiOrderBookFactory>();
            services.AddTransient(x => x.GetRequiredService<IHuobiRestClient>().SpotApi.CommonSpotClient);
            if (socketClientLifeTime == null)
                services.AddSingleton<IHuobiSocketClient, HuobiSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(IHuobiSocketClient), typeof(HuobiSocketClient), socketClientLifeTime.Value));
            return services;
        }
    }
}
