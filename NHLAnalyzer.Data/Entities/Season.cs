namespace NHLAnalyzer.Data.Entities
{
    public class Season : BaseEntity
    {
        public int SeasonYear { get; set; }

        // TODO: Add additional information for each season like 
        // Regular Season Champion
        // Stanley Cup Champion
        // Etc.

        public ICollection<PlayerSeason> PlayerSeasons { get; set; }
    }
}
