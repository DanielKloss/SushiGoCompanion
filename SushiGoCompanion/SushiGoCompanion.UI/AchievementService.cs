using SushiGoCompanion.Data.Models;
using SushiGoCompanion.Data.Repositories;
using System;

namespace SushiGoCompanion.UI
{
    public class AchievementService
    {
        private Game game;
        private PlayerRepository playerRepo;
        private PlayerGameRepository playerGameRepo;

        public AchievementService(Game Game)
        {
            game = Game;
            playerRepo = new PlayerRepository();
            playerGameRepo = new PlayerGameRepository();
        }

        internal void AddAchievements()
        {
            foreach (Player player in game.players)
            {
                foreach (Achievement achievement in player.achievements)
                {
                    AchievementRepository achievementRepo = new AchievementRepository();

                    achievementRepo.AddAchievement(achievement);
                }
            }
        }

        internal void CheckForFirsts()
        {
            foreach (Player player in game.players)
            {
                PlayerRepository playerRepo = new PlayerRepository();

                if (playerRepo.GetNumberOfGamesById(player.id) == 1)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "1st Game", image = "/Assets/Logos/milestoneGame.png", dateTime = DateTime.Now });
                }

                if (player.isWinner && playerRepo.GetNumberOfWinsById(player.id) == 1)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "1st Win", image = "/Assets/Logos/milestoneWin.png", dateTime = DateTime.Now });
                }

                if (player.isLoser && playerRepo.GetNumberOfLosesById(player.id) == 1)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "1st Loss", image = "/Assets/Logos/milestoneLoss.png", dateTime = DateTime.Now });
                }
            }
        }

        internal void CheckForMileStones()
        {
            foreach (Player player in game.players)
            {
                int numberOfLosses = playerRepo.GetNumberOfLosesById(player.id);

                if (player.isLoser && numberOfLosses % 10 == 0)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = numberOfLosses + "th Loss", image = "/Assets/Logos/milestoneLoss.png", dateTime = DateTime.Now });
                }

                int numberOfWins = playerRepo.GetNumberOfWinsById(player.id);

                if (player.isWinner && numberOfWins % 10 == 0)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = numberOfWins + "th Win", image = "/Assets/Logos/milestoneWin.png", dateTime = DateTime.Now });
                }

                int numberOfGames = playerRepo.GetNumberOfGamesById(player.id);

                if (numberOfGames % 10 == 0)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = numberOfGames + "th Game", image = "/Assets/Logos/milestoneGame.png", dateTime = DateTime.Now });
                }
            }
        }

        internal void CheckForEverScores()
        {
            foreach (Player player in game.players)
            {
                if (player.isLoser && player.totalScore < playerGameRepo.GetWorstEverScore())
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "New Worst Ever Score (" + player.totalScore + ")", image = "/Assets/Logos/globalDown.png", dateTime = DateTime.Now });
                }

                if (player.isWinner && player.totalScore > playerGameRepo.GetBestEverScore())
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "New Best Ever Score (" + player.totalScore + ")", image = "/Assets/Logos/globalUp.png", dateTime = DateTime.Now });
                }
            }
        }

        internal void CheckForPersonalScores()
        {
            foreach (Player player in game.players)
            {
                if (playerRepo.GetNumberOfGamesById(player.id) > 0 && player.totalScore < playerRepo.GetWorstScoreById(player.id))
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "New Personal Worst Score (" + player.totalScore + ")", image = "/Assets/Logos/personalDown.png", dateTime = DateTime.Now });

                }

                if (playerRepo.GetNumberOfGamesById(player.id) > 0 && player.totalScore > playerRepo.GetBestScoreById(player.id))
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "New Personal Best Score (" + player.totalScore + ")", image = "/Assets/Logos/personalUp.png", dateTime = DateTime.Now });

                }
            }
        }
    }
}
