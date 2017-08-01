using SushiGoCompanion.Data.Models;
using SushiGoCompanion.Data.Repositories;
using System;

namespace SushiGoCompanion.UI
{
    public class AchievementService
    {
        private Game game;

        public AchievementService(Game Game)
        {
            game = Game;
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

                if (playerRepo.GetNumberOfGamesById(player.id) == 0)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "1st Game", image = "Assets/milestoneGame.png", dateTime = DateTime.Now });
                }
            }
        }

        internal void CheckForMileStones()
        {
            foreach (Player player in game.players)
            {
                PlayerRepository playerRepo = new PlayerRepository();

                int numberOfGames = playerRepo.GetNumberOfGamesById(player.id);

                if (numberOfGames % 10 == 0 && numberOfGames != 0)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = numberOfGames + "th Game", image = "Assets/milestoneGame.png", dateTime = DateTime.Now });
                }
            }
        }

        internal void CheckForEverScores()
        {

        }

        internal void CheckForPersonalScores()
        {

        }
    }
}
