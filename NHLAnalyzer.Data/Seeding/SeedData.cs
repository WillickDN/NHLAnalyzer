namespace NHLAnalyzer.Data.Seeding
{
    public class SeedData
    {
        #region Constants

        private const string PLAYER_STATS_FOLDER = @"..\NHLAnalyzer.Data\Assets\PlayerStats";

        #endregion

        public static void Initialize(string webDirecotry, ApplicationDbContext ctx)
        {
            var csvFiles = PlayerStatsCsvReader.GetPlayerStatCsvPaths(Path.Combine(webDirecotry, PLAYER_STATS_FOLDER));
            var playerStatsService = new SeedStatsService(ctx);
            playerStatsService.InsertPlayerStatsFromCsvFiles(csvFiles);
        }
    }
}