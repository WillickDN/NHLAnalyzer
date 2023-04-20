using NHLAnalyzer.Data.Entities;
using NHLAnalyzer.Data.Enums;

namespace NHLAnalyzer.Web.ViewModels
{
    public class PlayerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Position Position { get; set; }

        public int GamesPlayed { get; set; }

        public int Goals { get; set; }

        public int Assists { get; set; }

        public int PlusMinus { get; set; }

        public int Pims { get; set; }

        public int ShotsOnGoal { get; set; }

        public int GameWinningGoals { get; set; }

        public int PowerPlayGoals { get; set; }

        public int PowerPlayAssists { get; set; }

        public int ShorthandedGoals { get; set; }

        public int ShorthandedAssists { get; set; }

        public int Hits { get; set; }

        public int Blocks { get; set; }

        public string Team { get; set; }

        public double Rating { get; set; }

        public PlayerViewModel(PlayerSeason playerSeason)
        {

        }
    }
}
