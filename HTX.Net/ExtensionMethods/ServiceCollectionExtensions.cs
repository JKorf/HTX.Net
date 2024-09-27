using CryptoExchange.Net.Clients;
using HTX.Net.Clients;
using HTX.Net.Interfaces;
using HTX.Net.Interfaces.Clients;
using HTX.Net.Objects.Options;
using HTX.Net.SymbolOrderBooks;
using System.Net;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the IRestHTXClient and IHTXSocketClient to the sevice collection so they can be injected
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="defaultRestOptionsDelegate">Set default options for the rest client</param>
        /// <param name="defaultSocketOptionsDelegate">Set default options for the socket client</param>
        /// <param name="socketClientLifeTime">The lifetime of the IHTXSocketClient for the service collection. Defaults to Singleton.</param>
        /// <returns></returns>
        public static IServiceCollection AddHTX(
            this IServiceCollection services,
            Action<HTXRestOptions>? defaultRestOptionsDelegate = null,
            Action<HTXSocketOptions>? defaultSocketOptionsDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            var restOptions = HTXRestOptions.Default.Copy();

            if (defaultRestOptionsDelegate != null)
            {
                defaultRestOptionsDelegate(restOptions);
                HTXRestClient.SetDefaultOptions(defaultRestOptionsDelegate);
            }

            if (defaultSocketOptionsDelegate != null)
                HTXSocketClient.SetDefaultOptions(defaultSocketOptionsDelegate);

            services.AddHttpClient<IHTXRestClient, HTXRestClient>(options =>
            {
                options.Timeout = restOptions.RequestTimeout;
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
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
            services.AddTransient<IHTXOrderBookFactory, HTXOrderBookFactory>();

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IHTXRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IHTXSocketClient>().SpotApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IHTXRestClient>().UsdtFuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IHTXSocketClient>().UsdtFuturesApi.SharedClient);

            services.AddTransient(x => x.GetRequiredService<IHTXRestClient>().SpotApi.CommonSpotClient);
            if (socketClientLifeTime == null)
                services.AddSingleton<IHTXSocketClient, HTXSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(IHTXSocketClient), typeof(HTXSocketClient), socketClientLifeTime.Value));
            return services;
        }
    }
}
