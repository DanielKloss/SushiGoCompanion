using MvvmDialogs;
using SQLite.Net;
using SushiGoCompanion.Data.Models;
using SushiGoCompanion.Data.Repositories;
using SushiGoCompanion.UI.Dialogs;
using SushiGoCompanion.UI.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SushiGoCompanion.UI.ViewModels
{
    public class ScoreboardViewModel : BaseViewModel
    {
        private GameRepository _gameRepo;
        private PlayerGameRepository _playerGameRepo;
        private IDialogService _dialogService;

        private bool _newRound;
        public bool newRound
        {
            get { return _newRound; }
            set
            {
                _newRound = value;
                OnPropertyChanged(nameof(newRound));
            }
        }

        private Game _game;
        public Game game
        {
            get { return _game; }
            set
            {
                _game = value;
                OnPropertyChanged(nameof(game));
            }
        }

        private ICommand _nextRoundCommand;
        public ICommand nextRoundCommand
        {
            get
            {
                if (_nextRoundCommand == null)
                {
                    _nextRoundCommand = new Command(NextRound);
                }
                return _nextRoundCommand;
            }
            set { _nextRoundCommand = value; }
        }

        private ICommand _finishGameCommand;
        public ICommand finishGameCommand
        {
            get
            {
                if (_finishGameCommand == null)
                {
                    _finishGameCommand = new Command(() => SaveGame());
                }
                return _finishGameCommand;
            }
            set { _finishGameCommand = value; }
        }

        private ICommand _cancelGameCommand;
        public ICommand cancelGameCommand
        {
            get
            {
                if (_cancelGameCommand == null)
                {
                    _cancelGameCommand = new Command(EndGame);
                }
                return _cancelGameCommand;
            }
            set { _cancelGameCommand = value; }
        }

        public ScoreboardViewModel(IEnumerable<Player> Players)
        {
            _dialogService = new DialogService();
            _gameRepo = new GameRepository();
            _playerGameRepo = new PlayerGameRepository();
            game = new Game();

            game.round = 1;
            game.dateTime = DateTime.Now;
            game.players = new ObservableCollection<Player>(Players);
        }

        private void NextRound()
        {
            newRound = true;

            CalculateScores();

            if (game.round == 3)
            {
                game.round++;
                SaveGame();
            }
            else
            {
                game.round++;
            }
        }

        public async void EndGame()
        {
            ContentDialogResult result = await _dialogService.ShowContentDialogAsync(new ConfirmDialogViewModel("End Game", "You are about to quit an unfinished game. Are you sure you want to quit the game without logging the stats?"));

            if (result == ContentDialogResult.Primary)
            {
                ((App)Application.Current).rootFrame.Navigate(typeof(MainMenuView));
            }
        }

        private async void SaveGame()
        {
            if (game.round < 4)
            {
                ContentDialogResult result = await _dialogService.ShowContentDialogAsync(new ConfirmDialogViewModel("End Game", "You are about to quit an unfinished game. Are you sure you want to quit the game and log the stats?"));

                if (result == ContentDialogResult.Secondary)
                {
                    return;
                }
            }

            CalculateScores();

            AchievementService achievementService = new AchievementService(game);

            achievementService.CheckForPersonalScores();
            achievementService.CheckForEverScores();

            try
            {
                _gameRepo.AddGame(game);

                foreach (Player player in game.players)
                {
                    _playerGameRepo.AddPlayerGame(player, game.id);
                }

                achievementService.CheckForFirsts();
                achievementService.CheckForMileStones();

                achievementService.AddAchievements();

                ((App)Application.Current).rootFrame.Navigate(typeof(StandingsView), game);
            }
            catch (SQLiteException)
            {
                await _dialogService.ShowContentDialogAsync(new MessageDialogViewModel("Error", "Something went wrong, please try again"));
            }
        }

        public void CalculateScores()
        {
            IEnumerable<IGrouping<int, Player>> groupedMaki = game.players.GroupBy(p => p.sushiScores.SingleOrDefault(s => s.name == Sushis.Maki.ToString()).numberOfCards);

            foreach (Player player in game.players)
            {
                //TEMPURA
                double tempuraSets = Math.Floor(Convert.ToDouble(player.sushiScores.SingleOrDefault(s => s.name == Sushis.Tempura.ToString()).numberOfCards / 2));
                player.roundScore += Convert.ToInt32(tempuraSets * 5);

                //SASHIMI
                double sashimiSets = Math.Floor(Convert.ToDouble(player.sushiScores.SingleOrDefault(s => s.name == Sushis.Sashimi.ToString()).numberOfCards / 3));
                player.roundScore += Convert.ToInt32(sashimiSets * 5);

                //DUMPLINGS
                int dumplings = player.sushiScores.SingleOrDefault(s => s.name == Sushis.Dumpling.ToString()).numberOfCards;

                if (dumplings > 5)
                {
                    player.roundScore += 15;
                }
                else if (dumplings > 0)
                {
                    player.roundScore += (dumplings * (dumplings + 1)) / 2;
                }

                //NIGIRI
                player.roundScore += player.sushiScores.SingleOrDefault(s => s.name == Sushis.EggNigiri.ToString()).numberOfCards * 1;
                player.roundScore += player.sushiScores.SingleOrDefault(s => s.name == Sushis.SalmonNigiri.ToString()).numberOfCards * 2;
                player.roundScore += player.sushiScores.SingleOrDefault(s => s.name == Sushis.SquidNigiri.ToString()).numberOfCards * 3;

                player.roundScore += player.sushiScores.SingleOrDefault(s => s.name == Sushis.EggNigiri.ToString()).wasabis * 2;
                player.roundScore += player.sushiScores.SingleOrDefault(s => s.name == Sushis.SalmonNigiri.ToString()).wasabis * 4;
                player.roundScore += player.sushiScores.SingleOrDefault(s => s.name == Sushis.SquidNigiri.ToString()).numberOfCards * 6;

                //MAKI
                if (groupedMaki.First().Contains(player))
                {
                    player.roundScore += Convert.ToInt32(Math.Floor(6.0 / groupedMaki.First().Count()));
                }

                if (groupedMaki.ElementAt(1).Contains(player))
                {
                    player.roundScore += Convert.ToInt32(Math.Floor(3.0 / groupedMaki.ElementAt(1).Count()));
                }
            }
        }
    }
}
