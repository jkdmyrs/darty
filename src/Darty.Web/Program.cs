namespace Darty.Web
{
    using Darty.Core.ApiClient;
    using Darty.Web.Constants;
    using Darty.Web.Settings;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            // string baseUrl = "https://dmyrs.com/darty/dev/api";
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // fetch client config during startup
            HttpClient localHttp = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            ClientConfig clientConfig =
                await localHttp.GetFromJsonAsync<ClientConfig>("config/client_config.json").ConfigureAwait(false);

            // register HTTP clients with factory
            builder.Services.AddHttpClient(HttpClients.DartyAPI, c => c.BaseAddress = new Uri(clientConfig.ApiBaseUrl));
            builder.Services.AddHttpClient(HttpClients.Local, c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            // custom registrations
            builder.Services.AddSingleton<ClientConfig>(clientConfig);
            builder.Services.AddScoped(sp => {
                HttpClient client = sp.GetService<IHttpClientFactory>().CreateClient(HttpClients.DartyAPI);
                return new DartyApiClient(client);
            });

            await builder.Build().RunAsync();
        }
    }
}
