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
                makiScore = player.sushiScores.SingleOrDefault(s => s.name == "Maki").numberOfCards,
                dumplingScore = player.sushiScores.SingleOrDefault(s => s.name == "Dumpling").numberOfCards,
                tempuraScore = player.sushiScores.SingleOrDefault(s => s.name == "Tempura").numberOfCards,
                sashimiScore = player.sushiScores.SingleOrDefault(s => s.name == "Sashimi").numberOfCards,
                nigiriScore = player.sushiScores.SingleOrDefault(s => s.name == "Nigiri").numberOfCards,
                desertScore = player.sushiScores.SingleOrDefault(s => s.name == "Dessert").numberOfCards
            });
        }
    }
}
