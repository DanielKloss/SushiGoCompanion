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
                makiScore = player.sushiScore.SingleOrDefault(s => s.name == Sushis.Maki.ToString()).score,
                dumplingScore = player.sushiScore.SingleOrDefault(s => s.name == Sushis.Dumpling.ToString()).score,
                tempuraScore = player.sushiScore.SingleOrDefault(s => s.name == Sushis.Tempura.ToString()).score,
                sashimiScore = player.sushiScore.SingleOrDefault(s => s.name == Sushis.Sashimi.ToString()).score,
                nigiriScore = player.sushiScore.SingleOrDefault(s => s.name == Sushis.EggNigiri.ToString()).score + player.sushiScore.SingleOrDefault(s => s.name == Sushis.SalmonNigiri.ToString()).score + player.sushiScore.SingleOrDefault(s => s.name == Sushis.SquidNigiri.ToString()).score,
                puddingScore = player.sushiScore.SingleOrDefault(s => s.name == Sushis.Pudding.ToString()).score
            });
        }
    }
}
