using CsvHelper.Configuration.Attributes;

namespace NHLAnalyzer.Data.Models
{
    /// <summary>
    /// Each item represents a row from the Rotowire Player Season CSV files   
    /// </summary>
    /// <remarks>
    /// /// Found at: https://www.rotowire.com/hockey/stats.php
    /// </remarks>
    public class PlayerStatModel
    {
        [Name("Player Name")]
        public string PlayerName { get; set; }

        [Name("Team")]
        public string Team { get; set; }

        [Name("Pos")]
        public string Position { get; set; }

        [Name("Games")]
        public int GamesPlayed { get; set; }

        [Name("G")]
        public int Goals { get; set; }

        [Name("A")]
        public int Assists { get; set; }

        [Name("+/-")]
        public int PlusMinus { get; set; }

        [Name("PIM")]
        public int Pims { get; set; }

        [Name("SOG")]
        public int ShotsOnGoal { get; set; }

        [Name("GWG")]
        public int GameWinningGoals { get; set; }

        [Name("PPG")]
        public int PowerPlayGoals { get; set; }

        [Name("PPA")]
        public int PowerPlayAssists { get; set; }

        [Name("SHG")]
        public int ShorthandedGoals { get; set; }

        [Name("SHA")]
        public int ShorthandedAssists { get; set; }

        [Name("Hits")]
        public int Hits { get; set; }

        [Name("BS")]
        public int Blocks { get; set; }

        [Ignore]
        public int Season { get; set; }
    }
}
