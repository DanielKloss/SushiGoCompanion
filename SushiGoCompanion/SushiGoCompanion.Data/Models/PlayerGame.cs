using SQLite.Net.Attributes;

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

        private int _desertScore;
        public int desertScore
        {
            get { return _desertScore; }
            set
            {
                _desertScore = value;
                OnPropertyChanged(nameof(desertScore));
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
