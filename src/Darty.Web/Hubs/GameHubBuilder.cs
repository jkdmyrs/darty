namespace Darty.Web.Hubs
{
    using Darty.Core;
    using Microsoft.AspNetCore.SignalR.Client;
    using System;
    using System.Threading.Tasks;

    public static class GameHubBuilder
    {
        public static HubConnection Build(string gameId, Func<Task> newGame, Func<Task> dartThrow, Func<Task> reconnected, string hubBaseUrl)
        {
            var hubUrl = hubBaseUrl.EndsWith("") ? "api" : "/api";
            HubConnection hub = new HubConnectionBuilder()
                   .WithUrl($"{hubBaseUrl}{hubUrl}", (options) =>
                   {
                       options.Headers.Add("x-ms-signalr-userid", gameId);
                   })
                   .WithAutomaticReconnect()
                   .Build();
            hub.On(Targets.NewGame, newGame);
            hub.On(Targets.DartThrow, dartThrow);
            hub.Reconnected += (_) => reconnected();
            return hub;
        }
    }
}
