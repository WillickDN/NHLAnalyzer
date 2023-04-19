namespace NHLAnalyzer.Data.Entities
{
    public class Team : BaseEntity
    {
        public string NameAbbreviation { get; set; }

        public ICollection<PlayerSeason> PlayerSeasons { get; set; }
    }
}
