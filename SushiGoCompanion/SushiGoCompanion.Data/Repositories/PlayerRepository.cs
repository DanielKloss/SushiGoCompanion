﻿using System.Collections.Generic;
using System.Linq;
using SushiGoCompanion.Data.Models;
using System;

namespace SushiGoCompanion.Data.Repositories
{
    public class PlayerRepository : Repository
    {
        public IEnumerable<Player> GetAllPlayers()
        {
            return connection.Table<Player>();
        }

        public bool PlayerExists(string newPlayerName)
        {
            return !(connection.Table<Player>().SingleOrDefault(p => p.name == newPlayerName) == null);
        }

        public void AddPlayer(Player playerToAdd)
        {
            connection.Insert(playerToAdd);
        }

        public void DeletePlayerById(int id)
        {
            connection.Table<PlayerGame>().Delete(p => p.playerId == id);
            connection.Table<Player>().Delete(p => p.id == id);
        }

        public void EditPlayer(Player newPlayer)
        {
            connection.Update(newPlayer, typeof(Player));
        }

        public int GetNumberOfGamesById(int id)
        {
            return connection.Table<PlayerGame>().Where(pg => pg.playerId == id).Count();
        }

        public int GetNumberOfWinsById(int id)
        {
            return connection.Table<PlayerGame>().Where(pg => pg.playerId == id && pg.isWinner).Count();
        }

        public int GetNumberOfLosesById(int id)
        {
            return connection.Table<PlayerGame>().Where(pg => pg.playerId == id && pg.isLoser).Count();
        }

        public int GetBestScoreById(int id)
        {
            var scores = from playergame in connection.Table<PlayerGame>()
                         where playergame.playerId == id
                         orderby playergame.totalScore descending
                         select playergame;

            if (scores.FirstOrDefault() == null)
            {
                return 0;
            }
            else
            {
                return scores.FirstOrDefault().totalScore;
            }
        }

        public int GetWorstScoreById(int id)
        {
            var scores = from playergame in connection.Table<PlayerGame>()
                         where playergame.playerId == id
                         orderby playergame.totalScore ascending
                         select playergame;

            if (scores.FirstOrDefault() == null)
            {
                return 0;
            }
            else
            {
                return scores.FirstOrDefault().totalScore;
            }
        }

        public double GetAverageScoreById(int id)
        {
            var allScores = connection.Table<PlayerGame>().Where(pg => pg.playerId == id).Select(p => p.totalScore);

            if (allScores.Count() == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(Convert.ToDouble(allScores.Sum()) / Convert.ToDouble(allScores.Count()), 0);
            }
        }
    }
}
