using SushiGoCompanion.Data.Models;

namespace SushiGoCompanion.Data.Repositories
{
    public class GameRepository : Repository
    {
        public void AddGame(Game gameToAdd)
        {
            connection.Insert(gameToAdd);
        }

        public int GetTotalGames()
        {
            return connection.Table<Game>().Count();
        }
    }
}
