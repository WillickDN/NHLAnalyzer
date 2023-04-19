namespace NHLAnalyzer.Data.Entities
{
    public class Player : BaseEntity
    {
        public string PlayerName { get; set; }

        public ICollection<PlayerSeason> PlayerSeasons { get; set; }
    }
}
