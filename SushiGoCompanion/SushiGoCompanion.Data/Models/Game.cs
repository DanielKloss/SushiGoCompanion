using SQLite.Net.Attributes;
using System;
using System.Collections.ObjectModel;

namespace SushiGoCompanion.Data.Models
{
    public class Game : Entity
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

        private DateTime _dateTime;
        public DateTime dateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(dateTime));
            }
        }

        private int _round;
        [Ignore]
        public int round
        {
            get { return _round; }
            set
            {
                _round = value;
                OnPropertyChanged(nameof(round));
            }
        }

        private ObservableCollection<Player> _players;
        [Ignore]
        public ObservableCollection<Player> players
        {
            get { return _players; }
            set
            {
                _players = value;
                OnPropertyChanged(nameof(players));
            }
        }
    }
}
