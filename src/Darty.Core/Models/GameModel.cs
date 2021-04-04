namespace Darty.Core.Models
{
    using Darty.Core.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GameModel
    {
        private static readonly List<int> _requiredValues = new List<int> { 20, 19, 18, 17, 16, 15, 25 };
        private string _player1Name;
        private string _player2Name;

        public GameModel(string player1Name, string player2Name, string gameId)
        {
            _player1Name = player1Name;
            _player2Name = player2Name;
            ThrowHistory = new List<(int Value, int Multiplier, string Player)>();
            Id = gameId;
        }

        public GameModel(string player1Name, string player2Name, IList<(int Value, int Multiplier, string Player)> gameHistory, string gameId)
        {
            _player1Name = player1Name;
            _player2Name = player2Name;
            ThrowHistory = gameHistory;
            Id = gameId;
        }

        public string Id { get; }

        public (string Name, int Score, bool IsWinner) Player1
        {
            get
            {
                return (_player1Name, CalculateScore(_player1Name), IsWinner(_player1Name));
            }
        }

        public (string Name, int Score, bool IsWinner) Player2
        {
            get
            {
                return (_player2Name, CalculateScore(_player2Name), IsWinner(_player2Name));
            }
        }

        public IList<(int Value, int Multiplier, string Player)> ThrowHistory { get; private set; }

        public bool HasWinner { get => Player1.IsWinner || Player2.IsWinner; }

        public void DartThrow(string player, int value, int multiplier)
        {
            if (HasWinner)
            {
                throw new GameOverException(Id.ToString());
            }
            ValidatePlayer(player);
            ValidateDartThrow(value, multiplier);
            ThrowHistory.Add((value, multiplier, player));
        }

        private void ValidatePlayer(string player)
        {
            if (!(_player1Name == player || _player2Name == player))
            {
                throw new InvalidPlayerException(player);
            }
        }

        private void ValidateValue(int value)
        {
            if (!_requiredValues.Contains(value))
            {
                throw new InvalidDartValueException(value.ToString());
            }
        }

        private void ValidateMultiplier(int multiplier)
        {
            if (!(multiplier > 0 && multiplier <= 3))
            {
                throw new InvalidDartMultiplierException(multiplier.ToString());
            }
        }

        private void ValidateDartThrow(int value, int multiplier)
        {
            ValidateValue(value);
            ValidateMultiplier(multiplier);
        }

        private int CalculateScore(string player)
        {
            ValidatePlayer(player);
            int score = 0;
            _requiredValues.ForEach(val => score += ScoreFromValue(val, player));
            return score;
        }

        private int HitsOnValue(int value, string player)
        {
            ValidateValue(value);
            ValidatePlayer(player);
            return ThrowHistory.Where(x => x.Player == player && x.Value == value).Sum(x => x.Multiplier);
        }

        private int HitsRequiredForValue(int value, string player)
        {
            ValidateValue(value);
            ValidatePlayer(player);
            return Math.Max(3 - HitsOnValue(value, player), 0);
        }

        private bool IsValueClosed(int value, string player)
        {
            ValidateValue(value);
            ValidatePlayer(player);
            return HitsRequiredForValue(value, player) == 0;
        }

        private int ScoreFromValue(int value, string player)
        {
            ValidateValue(value);
            ValidatePlayer(player);
            int hits = HitsOnValue(value, player);
            return hits > 3 ? (hits - 3) * value : 0;
        }

        private string OtherPlayer(string player)
        {
            ValidatePlayer(player);
            return _player1Name == player ? _player2Name : _player1Name;
        }

        private bool IsWinner(string player)
        {
            ValidatePlayer(player);
            bool allClosed = _requiredValues.TrueForAll(val => IsValueClosed(val, player));
            bool higherScore = CalculateScore(player) > CalculateScore(OtherPlayer(player));
            return allClosed && higherScore;
        }
    }
}
