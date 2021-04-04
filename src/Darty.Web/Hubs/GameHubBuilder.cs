﻿namespace Darty.Web.Hubs
{
    using Darty.Core;
    using Microsoft.AspNetCore.SignalR.Client;
    using System;
    using System.Threading.Tasks;

    public static class GameHubBuilder
    {
        public static HubConnection Build(string gameId, Func<Task> newGame, Func<Task> dartThrow, string hubBaseUrl)
        {
            var hubUrl = hubBaseUrl.EndsWith("") ? "api" : "/api";
            HubConnection hub = new HubConnectionBuilder()
                   .WithUrl($"{hubBaseUrl}{hubUrl}", (options) =>
                   {
                       options.Headers.Add("x-ms-signalr-userid", gameId);
                   })
                   .Build();
            hub.On(Targets.NewGame, newGame);
            hub.On(Targets.DartThrow, dartThrow);
            return hub;
        }
    }
}
