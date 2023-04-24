namespace NHLAnalyzer.Web.ViewModels
{
    public class PlayerSeasonViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

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
    }
}
