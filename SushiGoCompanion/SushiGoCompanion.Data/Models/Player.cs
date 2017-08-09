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

        private ObservableCollection<SushiType> _sushiCards;
        [Ignore]
        public ObservableCollection<SushiType> sushiCards
        {
            get { return _sushiCards; }
            set
            {
                _sushiCards = value;
                OnPropertyChanged(nameof(sushiCards));
            }
        }

        private ObservableCollection<SushiType> _sushiScore;
        [Ignore]
        public ObservableCollection<SushiType> sushiScore
        {
            get { return _sushiScore; }
            set
            {
                _sushiScore = value;
                OnPropertyChanged(nameof(sushiScore));
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

            sushiCards = new ObservableCollection<SushiType>()
            {
                new SushiType(Sushis.Maki),
                new SushiType(Sushis.Dumpling),
                new SushiType(Sushis.Tempura),
                new SushiType(Sushis.Sashimi),
                new SushiType(Sushis.EggNigiri),
                new SushiType(Sushis.SalmonNigiri),
                new SushiType(Sushis.SquidNigiri)
            };

            sushiScore = new ObservableCollection<SushiType>()
            {
                new SushiType(Sushis.Maki),
                new SushiType(Sushis.Dumpling),
                new SushiType(Sushis.Tempura),
                new SushiType(Sushis.Sashimi),
                new SushiType(Sushis.EggNigiri),
                new SushiType(Sushis.SalmonNigiri),
                new SushiType(Sushis.SquidNigiri)
            };

            totalScore = 0;
        }
    }
}
