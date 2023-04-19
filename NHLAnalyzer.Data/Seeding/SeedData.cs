namespace NHLAnalyzer.Data.Seeding
{
    public class SeedData
    {
        #region Constants

        private const string PLAYER_STATS_FOLDER = @"..\NHLAnalyzer.Data\Assets\PlayerStats";

        #endregion

        public static void Initialize(string webDirecotry, ApplicationDbContext ctx)
        {
            var directory = Path.Combine(webDirecotry, PLAYER_STATS_FOLDER);

            var fileSeasons = PlayerStatsCsvReader.CreateFileSeasonInformationModels(directory);

            var playerStatsService = new PlayerStatsService(ctx);

            foreach (var season in fileSeasons)
            {
                playerStatsService.InsertSingleCsvSeason(season);
            }
        }
    }
}