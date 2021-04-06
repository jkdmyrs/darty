namespace Darty.Web.Tests.UnitTests
{
    using Darty.Core.Resources.Responses;
    using Darty.Tests.Shared;
    using Darty.Web.ViewModels;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    [TestCategory(Categories.Unit)]
    public class PlayerViewModelTests
    {
        [TestMethod]
        public void GetScoreDetailsForValue_ReturnsZeroWhenNoThrowsForValue()
        {
            var player = new PlayerViewModel(new List<DartThrowResponse>());
            player.GetScoreDetailsForValue(20).HitCount.Should().Be(0);
            player.GetScoreDetailsForValue(20).IsClosed.Should().BeFalse();
        }

        [TestMethod]
        public void GetScoreDetailsForValue_SingleDartWithoutMultiplierCounts1()
        {
            var player = new PlayerViewModel(new List<DartThrowResponse> {
                new DartThrowResponse
                {
                    Multiplier = 1,
                    Player = string.Empty,
                    Value = 20
                }
            });
            player.GetScoreDetailsForValue(20).HitCount.Should().Be(1);
            player.GetScoreDetailsForValue(20).IsClosed.Should().BeFalse();
        }

        [TestMethod]
        public void GetScoreDetailsForValue_SingleDartWithMultiplierCountsMultiplier()
        {
            var player = new PlayerViewModel(new List<DartThrowResponse> {
                new DartThrowResponse
                {
                    Multiplier = 2,
                    Player = string.Empty,
                    Value = 20
                }
            });
            player.GetScoreDetailsForValue(20).HitCount.Should().Be(2);
            player.GetScoreDetailsForValue(20).IsClosed.Should().BeFalse();
        }

        [TestMethod]
        public void GetScoreDetailsForValue_3HitsIsClosed()
        {
            var player = new PlayerViewModel(new List<DartThrowResponse> {
                new DartThrowResponse
                {
                    Multiplier = 3,
                    Player = string.Empty,
                    Value = 20
                }
            });
            player.GetScoreDetailsForValue(20).HitCount.Should().Be(3);
            player.GetScoreDetailsForValue(20).IsClosed.Should().BeTrue();
        }
    }
}
