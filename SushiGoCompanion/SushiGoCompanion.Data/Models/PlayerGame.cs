﻿using SQLite.Net.Attributes;

namespace SushiGoCompanion.Data.Models
{
    public class PlayerGame : Entity
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(id));
            }
        }

        private int _playerId;
        public int playerId
        {
            get { return _playerId; }
            set
            {
                _playerId = value;
                OnPropertyChanged(nameof(playerId));
            }
        }

        private int _gameId;
        public int gameId
        {
            get { return _gameId; }
            set
            {
                _gameId = value;
                OnPropertyChanged(nameof(gameId));
            }
        }


        private int _totalScore;
        public int totalScore
        {
            get { return _totalScore; }
            set
            {
                _totalScore = value;
                OnPropertyChanged(nameof(totalScore));
            }
        }

        private bool _isWinner;
        public bool isWinner
        {
            get { return _isWinner; }
            set
            {
                _isWinner = value;
                OnPropertyChanged(nameof(isWinner));
            }
        }

        private bool _isLoser;
        public bool isLoser
        {
            get { return _isLoser; }
            set
            {
                _isLoser = value;
                OnPropertyChanged(nameof(isLoser));
            }
        }

        private int _makiScore;
        public int makiScore
        {
            get { return _makiScore; }
            set
            {
                _makiScore = value;
                OnPropertyChanged(nameof(makiScore));
            }
        }

        private int _sashimiScore;
        public int sashimiScore
        {
            get { return _sashimiScore; }
            set
            {
                _sashimiScore = value;
                OnPropertyChanged(nameof(sashimiScore));
            }
        }


        private int _nigiriScore;
        public int nigiriScore
        {
            get { return _nigiriScore; }
            set
            {
                _nigiriScore = value;
                OnPropertyChanged(nameof(nigiriScore));
            }
        }

        private int _tempuraScore;
        public int tempuraScore
        {
            get { return _tempuraScore; }
            set
            {
                _tempuraScore = value;
                OnPropertyChanged(nameof(tempuraScore));
            }
        }

        private int _puddingScore;
        public int puddingScore
        {
            get { return _puddingScore; }
            set
            {
                _puddingScore = value;
                OnPropertyChanged(nameof(puddingScore));
            }
        }

        private int _dumplingScore;
        public int dumplingScore
        {
            get { return _dumplingScore; }
            set
            {
                _dumplingScore = value;
                OnPropertyChanged(nameof(dumplingScore));
            }
        }
    }
}
