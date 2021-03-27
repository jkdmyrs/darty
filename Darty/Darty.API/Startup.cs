using Darty.Core.Operations.Impl;
using Darty.Core.Operations.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Darty.API.Startup))]
namespace Darty.API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();

            builder.Services.AddSingleton<ICreateGameOperation, CreateGameOperation>();
            builder.Services.AddSingleton<IGetGameByIdOperation, GetGameByIdOperation>();
            builder.Services.AddSingleton<IDartThrowOperation, DartThrowOperation>();
        }
    }
}
