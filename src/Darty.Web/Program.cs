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
            // string baseUrl = "https://dmyrs.com/darty/dev/api";
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient());
            builder.Services.AddScoped(sp => new DartyApiClient(sp.GetService<HttpClient>()));

            await builder.Build().RunAsync();
        }
    }
}
