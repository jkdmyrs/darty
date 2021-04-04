namespace Darty.Core.Tests.Automations
{
    using Darty.Core.ApiClient;
    using Darty.Core.Resources.Responses;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    [TestCategory(Categories.Automation)]
    public class DartyGameAutomation
    {
        private DartyApiClient _apiClient;

        [TestInitialize]
        public void Init()
        {
            _apiClient = new DartyApiClient(new HttpClient { BaseAddress = new Uri("https://dmyrs.com/darty/dev/") });
        }

        [TestMethod]
        public async Task GameAutomation_OnlyPlayer1ThrowsAndWins()
        {
            string gameId = "RHBSGN";

            await _apiClient.CreateGame(gameId, "Jack", "Kevin").ConfigureAwait(false);
            Thread.Sleep(1500);
            List<int> dartThrows = new List<int> { 20, 19, 18, 17, 16, 15, 25 };
            // throw a single 20
            await _apiClient.DartThrow(gameId, "Jack", 20, 1).ConfigureAwait(false);
            Thread.Sleep(1500);
            foreach (int val in dartThrows)
            {
                // throw triples of each 
                await _apiClient.DartThrow(gameId, "Jack", val, 3).ConfigureAwait(false);
                Thread.Sleep(1500);
            }

            // get the game and verify
            GameModelResponse game = await _apiClient.GetGameById(gameId).ConfigureAwait(false);
            game.Id.Should().Be(gameId);
            game.HasWinner.Should().BeTrue();
            game.Player1.Name.Should().Be("Jack");
            game.Player2.Name.Should().Be("Kevin");
            game.Player1.IsWinner.Should().BeTrue();
            game.Player2.IsWinner.Should().BeFalse();
            game.Player1.Score.Should().Be(20);
            game.Player2.Score.Should().Be(0);
            game.ThrowHistory.Count.Should().Be(8);
        }
    }
}
