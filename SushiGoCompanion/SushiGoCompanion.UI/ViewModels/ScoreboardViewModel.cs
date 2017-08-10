using MvvmDialogs;
using SQLite.Net;
using SushiGoCompanion.Data.Models;
using SushiGoCompanion.Data.Repositories;
using SushiGoCompanion.UI.Dialogs;
using SushiGoCompanion.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private ICommand _minusScoreCommand;
        public ICommand minusScoreCommand
        {
            get
            {
                if (_minusScoreCommand == null)
                {
                    _minusScoreCommand = new Command<SushiType>(MinusScore);
                }
                return _minusScoreCommand;
            }
            set { _minusScoreCommand = value; }
        }

        private ICommand _plusScoreCommand;
        public ICommand plusScoreCommand
        {
            get
            {
                if (_plusScoreCommand == null)
                {
                    _plusScoreCommand = new Command<SushiType>(PlusScore);
                }
                return _plusScoreCommand;
            }
            set { _plusScoreCommand = value; }
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

        private void PlusScore(SushiType sushi)
        {
            sushi.score++;
            ((Command<SushiType>)plusScoreCommand).RaiseCanExecuteChanged();
        }

        private void MinusScore(SushiType sushi)
        {
            if (sushi.score > 0)
            {
                sushi.score--;
                ((Command<SushiType>)plusScoreCommand).RaiseCanExecuteChanged();
            }
        }

        private void NextRound()
        {
            newRound = true;

            game.round++;
            CalculateScores();

            foreach (Player player in game.players)
            {
                FindNumberOfSushiCards(player, Sushis.Maki).score = 0;
                FindNumberOfSushiCards(player, Sushis.Dumpling).score = 0;
                FindNumberOfSushiCards(player, Sushis.Sashimi).score = 0;
                FindNumberOfSushiCards(player, Sushis.Tempura).score = 0;

                player.sushiCards.SingleOrDefault(s => s.name == "Egg Nigiri").score = 0;
                player.sushiCards.SingleOrDefault(s => s.name == "Salmon Nigiri").score = 0;
                player.sushiCards.SingleOrDefault(s => s.name == "Squid Nigiri").score = 0;

                player.sushiCards.SingleOrDefault(s => s.name == "Egg Nigiri").wasabis = 0;
                player.sushiCards.SingleOrDefault(s => s.name == "Salmon Nigiri").wasabis = 0;
                player.sushiCards.SingleOrDefault(s => s.name == "Squid Nigiri").wasabis = 0;

                if (game.round == 3)
                {
                    player.sushiCards.Add(new SushiType(Sushis.Pudding));
                    player.sushiScore.Add(new SushiType(Sushis.Pudding));
                }
            }

            if (game.round == 4)
            {
                SaveGame();
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

                foreach (Player player in game.players)
                {
                    player.sushiScore.Add(new SushiType(Sushis.Pudding));
                    player.sushiCards.Add(new SushiType(Sushis.Pudding));
                }

                CalculateFinalScores();
            }

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
            IEnumerable<IGrouping<int, Player>> groupedMaki = game.players.GroupBy(p => p.sushiCards.SingleOrDefault(s => s.name == Sushis.Maki.ToString()).score);
            IEnumerable<IGrouping<int, Player>> groupedPudding = game.players.GroupBy(p => p.sushiCards.SingleOrDefault(s => s.name == Sushis.Pudding.ToString()).score);

            foreach (Player player in game.players)
            {
                ScoreTempura(player);
                ScoreSashimi(player);
                ScoreDumplings(player);
                ScoreNigiri(player);
                ScoreMaki(groupedMaki, player);

                //PUDDING
                if (game.round == 4)
                {
                    ScorePudding(groupedPudding, player);
                }
            }

            //FINAL SCORE
            if (game.round == 4)
            {
                CalculateFinalScores();
            }
        }

        private void CalculateFinalScores()
        {
            foreach (Player player in game.players)
            {
                foreach (SushiType sushiScore in player.sushiScore)
                {
                    player.totalScore += sushiScore.score;
                }
            }

            game.players = new ObservableCollection<Player>(game.players.OrderByDescending(p => p.totalScore));

            if (game.players.Where(p => p.totalScore == game.players.First().totalScore).Count() == game.players.Count)
            {
                foreach (Player player in game.players)
                {
                    player.isWinner = true;
                }
            }
            else
            {
                foreach (Player player in game.players.Where(p => p.totalScore == game.players.First().totalScore))
                {
                    player.isWinner = true;
                }

                foreach (Player player in game.players.Where(p => p.totalScore == game.players.Last().totalScore))
                {
                    player.isLoser = true;
                }
            }
        }

        private void ScorePudding(IEnumerable<IGrouping<int, Player>> groupedPudding, Player player)
        {
            if (groupedPudding.First().Count() != game.players.Count() && groupedPudding.First().Contains(player))
            {
                FindSushiScore(player, Sushis.Pudding).score += Convert.ToInt32(Math.Floor(6.0 / groupedPudding.First().Count()));
            }

            if (groupedPudding.First().Count() != game.players.Count() && groupedPudding.Last().Contains(player))
            {
                FindSushiScore(player, Sushis.Pudding).score -= Convert.ToInt32(Math.Floor(6.0 / groupedPudding.First().Count()));
            }
        }

        private void ScoreMaki(IEnumerable<IGrouping<int, Player>> groupedMaki, Player player)
        {
            //MAKI
            if (FindNumberOfSushiCards(player, Sushis.Maki).score > 0 && groupedMaki.First().Contains(player))
            {
                FindSushiScore(player, Sushis.Maki).score += Convert.ToInt32(Math.Floor(6.0 / groupedMaki.First().Count()));
            }

            if (FindNumberOfSushiCards(player, Sushis.Maki).score > 0 && groupedMaki.First().Count() == 1 && groupedMaki.ElementAt(1).Contains(player))
            {
                FindSushiScore(player, Sushis.Maki).score += Convert.ToInt32(Math.Floor(3.0 / groupedMaki.ElementAt(1).Count()));
            }
        }

        private void ScoreNigiri(Player player)
        {
            //NIGIRI
            player.sushiScore.SingleOrDefault(s => s.name == "Egg Nigiri").score += player.sushiCards.SingleOrDefault(s => s.name == "Egg Nigiri").score * 1;
            player.sushiScore.SingleOrDefault(s => s.name == "Salmon Nigiri").score += player.sushiCards.SingleOrDefault(s => s.name == "Salmon Nigiri").score * 2;
            player.sushiScore.SingleOrDefault(s => s.name == "Squid Nigiri").score += player.sushiCards.SingleOrDefault(s => s.name == "Squid Nigiri").score * 3;

            int eggWasabis = player.sushiCards.SingleOrDefault(s => s.name == "Egg Nigiri").wasabis > player.sushiCards.SingleOrDefault(s => s.name == "Egg Nigiri").score ? player.sushiCards.SingleOrDefault(s => s.name == "Egg Nigiri").score : player.sushiCards.SingleOrDefault(s => s.name == "Egg Nigiri").wasabis;
            int salmonWasabis = player.sushiCards.SingleOrDefault(s => s.name == "Salmon Nigiri").wasabis > player.sushiCards.SingleOrDefault(s => s.name == "Salmon Nigiri").score ? player.sushiCards.SingleOrDefault(s => s.name == "Salmon Nigiri").score : player.sushiCards.SingleOrDefault(s => s.name == "Salmon Nigiri").wasabis;
            int squidWasabis = player.sushiCards.SingleOrDefault(s => s.name == "Squid Nigiri").wasabis > player.sushiCards.SingleOrDefault(s => s.name == "Squid Nigiri").score ? player.sushiCards.SingleOrDefault(s => s.name == "Squid Nigiri").score : player.sushiCards.SingleOrDefault(s => s.name == "Squid Nigiri").wasabis;

            player.sushiScore.SingleOrDefault(s => s.name == "Egg Nigiri").score += eggWasabis * 2;
            player.sushiScore.SingleOrDefault(s => s.name == "Salmon Nigiri").score += salmonWasabis * 4;
            player.sushiScore.SingleOrDefault(s => s.name == "Squid Nigiri").score += squidWasabis * 6;
        }

        private void ScoreDumplings(Player player)
        {
            //DUMPLINGS
            int dumplings = FindNumberOfSushiCards(player, Sushis.Dumpling).score;

            if (dumplings > 5)
            {
                FindSushiScore(player, Sushis.Dumpling).score += 15;
            }
            else if (dumplings > 0)
            {
                FindSushiScore(player, Sushis.Dumpling).score += (dumplings * (dumplings + 1)) / 2;
            }
        }

        private void ScoreSashimi(Player player)
        {
            //SASHIMI
            double sashimiSets = Math.Floor(Convert.ToDouble(FindNumberOfSushiCards(player, Sushis.Sashimi).score / 3));
            FindSushiScore(player, Sushis.Sashimi).score += Convert.ToInt32(sashimiSets * 10);
        }

        private void ScoreTempura(Player player)
        {
            //TEMPURA
            double tempuraSets = Math.Floor(Convert.ToDouble(FindNumberOfSushiCards(player, Sushis.Tempura).score / 2));
            FindSushiScore(player, Sushis.Tempura).score += Convert.ToInt32(tempuraSets * 5);
        }

        private SushiType FindNumberOfSushiCards(Player player, Sushis sushi)
        {
            return player.sushiCards.SingleOrDefault(s => s.name == sushi.ToString());
        }

        private SushiType FindSushiScore(Player player, Sushis sushi)
        {
            return player.sushiScore.SingleOrDefault(s => s.name == sushi.ToString());
        }
    }
}
