namespace Darty.Web.Hubs
{
    using Darty.Core;
    using Microsoft.AspNetCore.SignalR.Client;
    using System;
    using System.Threading.Tasks;

    public static class GameHubBuidler
    {
        public static HubConnection Build(string hubUrl, string gameId, Func<Task> newGame, Func<Task> dartThrow)
        {
            var hub = new HubConnectionBuilder()
               .WithUrl(hubUrl, (options) =>
               {
                   options.Headers.Add("x-ms-signalr-userid", gameId);
               })
               .Build();

            hub.On(Targets.NewGame, () => newGame());

            hub.On(Targets.DartThrow, () => dartThrow());
            return hub;
        }
    }
}
