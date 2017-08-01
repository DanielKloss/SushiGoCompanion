using SQLite.Net.Attributes;
using System.Collections.ObjectModel;

namespace SushiGoCompanion.Data.Models
{
    public class Player : Entity
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

        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        private bool _selectedToPlay;
        public bool selectedToPlay
        {
            get { return _selectedToPlay; }
            set
            {
                _selectedToPlay = value;
                OnPropertyChanged(nameof(selectedToPlay));
            }
        }

        private ObservableCollection<Achievement> _achievements;
        [Ignore]
        public ObservableCollection<Achievement> achievements
        {
            get { return _achievements; }
            set
            {
                _achievements = value;
                OnPropertyChanged(nameof(achievements));
            }
        }

        private int _roundScore;
        [Ignore]
        public int roundScore
        {
            get { return _roundScore; }
            set
            {
                _roundScore = value;
                OnPropertyChanged(nameof(roundScore));
            }
        }

        private int _totalScore;
        [Ignore]
        public int totalScore
        {
            get { return _totalScore; }
            set
            {
                _totalScore = value;
                OnPropertyChanged(nameof(totalScore));
            }
        }

        private ObservableCollection<SushiType> _sushiScores;
        [Ignore]
        public ObservableCollection<SushiType> sushiScores
        {
            get { return _sushiScores; }
            set
            {
                _sushiScores = value;
                OnPropertyChanged(nameof(sushiScores));
            }
        }


        private bool _isWinner;
        [Ignore]
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
        [Ignore]
        public bool isLoser
        {
            get { return _isLoser; }
            set
            {
                _isLoser = value;
                OnPropertyChanged(nameof(isLoser));
            }
        }

        public Player()
        {
            achievements = new ObservableCollection<Achievement>();
            sushiScores = new ObservableCollection<SushiType>();
            totalScore = 0;
        }
    }
}
