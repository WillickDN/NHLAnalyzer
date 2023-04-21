using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using NHLAnalyzer.Data.Models;

namespace NHLAnalyzer.Data.Seeding
{
    /// <summary>
    /// Read CSV files in the proper format to generate Player Stat Models
    /// </summary>
    /// <remarks>
    /// The CSV files in the folder need to be named {season}_Player_Stats.csv
    /// The files are expected to have a consistent format based on Rotowire stats and a little bit of manual intervention
    /// </remarks>
    public static class PlayerStatsCsvReader
    {
        #region Constants

        private const string PLAYER_STATS = "Player_Stats";
        private const string CSV_EXTENSION = ".csv";

        #endregion

        #region Public Methods        


        /// <summary>
        /// Returns the path of all CSV files in the given directory that match the expected naming convention
        /// </summary>
        /// <remarks>
        /// CSVs should be named {Season}_Player_Stats.csv
        /// </remarks>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static IEnumerable<FileInfo> GetPlayerStatCsvPaths(string directory)
        {
            var validCsvFileInfos = new List<FileInfo>();
            if (Directory.Exists(directory))
            {
                // Get all CSV files in the directory
                var allCsvPaths = Directory.EnumerateFiles(directory, $"*.csv", SearchOption.TopDirectoryOnly);
                foreach (var csvFilePath in allCsvPaths)
                {
                    var fileInfo = new FileInfo(csvFilePath);
                    // Only grab CSVs named correctly
                    if (IsFileValidPlayerStatFormat(fileInfo.Name))
                    {
                        validCsvFileInfos.Add(fileInfo);
                    }
                }
            }

            return validCsvFileInfos;
        }

        /// <summary>
        /// Returns all player stats from the given season file
        /// </summary>
        /// <param name="fileInformationModel"></param>
        /// <returns></returns>
        public static List<PlayerStatModel> GetPlayerStatModelsFromFile(FileInfo fileInfo)
        {
            var result = new List<PlayerStatModel>();
            try
            {
                var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);

                using (var reader = new StreamReader(fileInfo.FullName))
                using (var csv = new CsvReader(reader, csvConfiguration))
                {
                    result = csv.GetRecords<PlayerStatModel>().ToList();
                    foreach (var line in result)
                    {
                        line.Season = GetSeasonFromFileName(fileInfo.Name);
                    }
                }
            }
            catch (HeaderValidationException headerException)
            {
                throw headerException;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(PlayerStatsCsvReader)} see following message: {Environment.NewLine}{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Returns the season year for the file based on the name of the file
        /// </summary>
        /// <remarks>
        /// Assumes the name begins with the 4 digits of the year
        /// </remarks>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int GetSeasonFromFileName(string fileName)
        {
            var season = -1;
            if (int.TryParse(fileName.AsSpan(0, 4), out var parsedSeason))
            {
                season = parsedSeason;
            }

            return season;
        }

        #endregion

        #region Private Methods        

        /// <summary>
        /// Determine if the files name is the correct format for the seeder to use.
        /// </summary>
        /// <remarks>
        /// Expects the file name to begin with the year (4 digits)
        /// Expects the file to contain the string Player_Stats
        /// </remarks>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool IsFileValidPlayerStatFormat(string fileName)
        {
            return fileName.Contains(PLAYER_STATS) && int.TryParse(fileName.AsSpan(0, 4), out var _);
        }

        #endregion
    }
}
