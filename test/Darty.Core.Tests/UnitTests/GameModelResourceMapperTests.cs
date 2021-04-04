namespace Darty.Core.Tests.UnitTests
{
    using Darty.Core.Mappers;
    using Darty.Core.Resources.Data;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class GameModelResourceMapperTests
    {
        [TestMethod]
        public void GameModelResourceMapper_CanMapThrowHistory()
        {
            var resource = new GameModelResource
            {
                HasWinner = false,
                Id = "123456",
                Player1 = new PlayerResource
                {
                    IsWinner = false,
                    Name = "Jack",
                    Score = 0
                },
                Player2 = new PlayerResource
                {
                    IsWinner = false,
                    Name = "Kevin",
                    Score = 0
                },
                ThrowHistory = new List<DartThrowResource>
                {
                    new DartThrowResource
                    {
                        Multiplier = 2,
                        Value = 20,
                        Player = "Jack"
                    },
                    new DartThrowResource
                    {
                        Multiplier = 3,
                        Value = 17,
                        Player = "Jack"
                    }
                }
            };
            var model = GameModelResourceMapper.MapToModel(resource);
            model.ThrowHistory.Count.Should().Be(resource.ThrowHistory.Count);
        }
    }
}
