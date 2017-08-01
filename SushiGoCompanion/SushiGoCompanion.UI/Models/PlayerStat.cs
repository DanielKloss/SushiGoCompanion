using SushiGoCompanion.Data.Models;
using System.Collections.Generic;

namespace SushiGoCompanion.UI.Models
{
    public class PlayerStat
    {
        public string name { get; set; }
        public int numberOfGames { get; set; }
        public int bestScore { get; set; }
        public int worstScore { get; set; }
        public int numberOfWins { get; set; }
        public int numberOfLoses { get; set; }
        public double winPercentage { get; set; }
        public int averageScore { get; set; }

        public List<Achievement> achievements { get; set; }
    }
}
