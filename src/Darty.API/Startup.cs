using Darty.Core.Commands.Impl;
using Darty.Core.Commands.Interfaces;
using Darty.Core.Operations.Impl;
using Darty.Core.Operations.Interfaces;
using Darty.Core.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Darty.API.Startup))]
namespace Darty.API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();

            // operations
            builder.Services.AddSingleton<ICreateGameOperation, CreateGameOperation>();
            builder.Services.AddSingleton<IGetGameByIdOperation, GetGameByIdOperation>();
            builder.Services.AddSingleton<IDartThrowOperation, DartThrowOperation>();
            builder.Services.AddSingleton<ICreateGameIdOperation, CreateGameIdOperation>();

            // commands
            builder.Services.AddSingleton<IFetchGameCommand, FetchGameCommand>();
            builder.Services.AddSingleton<IPersistGameCommand, PersistGameCommand>();

            // settings 
            builder.Services.AddSingleton<BlobStorageSettings>(new BlobStorageSettings
            {
                ConnecitonString = Environment.GetEnvironmentVariable("GameBlobStorageConnectionString"),
                GameContainer = "darty-games"
            });
        }
    }
}
