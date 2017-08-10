using SushiGoCompanion.Data.Models;
using System.Linq;

namespace SushiGoCompanion.Data.Repositories
{
    public class PlayerGameRepository : Repository
    {
        public void AddPlayerGame(Player player, int GameId)
        {
            connection.Insert(new PlayerGame
            {
                gameId = GameId,
                playerId = player.id,
                isWinner = player.isWinner,
                isLoser = player.isLoser,
                totalScore = player.totalScore,
                makiScore = player.sushiScore.SingleOrDefault(s => s.name == Sushis.Maki.ToString()).score,
                dumplingScore = player.sushiScore.SingleOrDefault(s => s.name == Sushis.Dumpling.ToString()).score,
                tempuraScore = player.sushiScore.SingleOrDefault(s => s.name == Sushis.Tempura.ToString()).score,
                sashimiScore = player.sushiScore.SingleOrDefault(s => s.name == Sushis.Sashimi.ToString()).score,
                nigiriScore = player.sushiScore.SingleOrDefault(s => s.name == "Egg Nigiri").score + player.sushiScore.SingleOrDefault(s => s.name == "Salmon Nigiri").score + player.sushiScore.SingleOrDefault(s => s.name == "Squid Nigiri").score,
                puddingScore = player.sushiScore.SingleOrDefault(s => s.name == Sushis.Pudding.ToString()).score
            });
        }

        public int? GetWorstEverScore()
        {
            var scores = from playergame in connection.Table<PlayerGame>()
                         orderby playergame.totalScore ascending
                         select playergame;

            return scores.FirstOrDefault()?.totalScore;
        }

        public int? GetBestEverScore()
        {
            var scores = from playergame in connection.Table<PlayerGame>()
                         orderby playergame.totalScore descending
                         select playergame;

            return scores.FirstOrDefault()?.totalScore;
        }
    }
}
