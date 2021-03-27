namespace Darty.Core.Tests
{
    using Darty.Core.Exceptions;
    using Darty.Core.Models;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public class CricketGameModelTests
    {
        [TestMethod]
        public void CanDetectWinner()
        {
            var game = new CricketGameModel("jack", "kevin");
            game.DartThrow("jack", 20, 1);
            game.DartThrow("jack", 19, 2);
            game.DartThrow("jack", 19, 1);
            new List<int> { 25, 20, 19, 18, 17, 16, 15 }.ForEach(valueToHit => game.DartThrow("jack", valueToHit, 3));
            game.HasWinner.Should().BeTrue();
            game.Player1.IsWinner.Should().BeTrue();
            game.Player1.Score.Should().Be(77);
        }

        [TestMethod]
        public void ThrowDartAfterWinnerThrows()
        {
            var game = new CricketGameModel("jack", "kevin");
            new List<int> { 25, 20, 19, 18, 17, 16, 15 }.ForEach(valueToHit => game.DartThrow("jack", valueToHit, 3));
            game.DartThrow("jack", 20, 1);
            Action test = () => game.DartThrow("jack", 20, 1);
            test.Should().Throw<GameOverException>();
        }

        [TestMethod]
        public void InvalidPlayerThrows()
        {
            var game = new CricketGameModel("jack", "kevin");
            Action test = () => game.DartThrow("abc", 20, 1);
            test.Should().Throw<InvalidPlayerException>().Where(x => x.Message.Contains("abc"));
        }

        [TestMethod]
        public void InvalidValueThrows()
        {
            var game = new CricketGameModel("jack", "kevin");
            Action test = () => game.DartThrow("jack", 12, 1);
            test.Should().Throw<InvalidDartValueException>().Where(x => x.Message.Contains("12"));
        }

        [TestMethod]
        public void InvalidMultiplierThrows()
        {
            var game = new CricketGameModel("jack", "kevin");
            Action test = () => game.DartThrow("jack", 20, 5);
            test.Should().Throw<InvalidDartMultiplierException>().Where(x => x.Message.Contains("5"));
        }
    }
}
