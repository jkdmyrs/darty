namespace Darty.Web
{
    using Darty.Core.ApiClient;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            string baseUrl = "https://dmyrs.com/darty/api";

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(new AppSettings { SignalRHubBaseUrl = $"{baseUrl}" });

            builder.Services.AddScoped(sp => new HttpClient());
            builder.Services.AddScoped(sp => new DartyApiClient(baseUrl, sp.GetService<HttpClient>()));

            await builder.Build().RunAsync();
        }
    }
}
