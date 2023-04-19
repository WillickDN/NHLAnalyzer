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
    /// The CSV files in the folder need to be names {season}_Player_Stats.csv
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
        /// Creates a lit of file season information models with the path to each player stats file and season year
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<FileSeasonInformation> CreateFileSeasonInformationModels(string directory)
        {
            var fileInformationModels = new List<FileSeasonInformation>();
            if (Directory.Exists(directory))
            {
                // Only return .csv files from the current directory
                var csvFilePaths = Directory.EnumerateFiles(directory, $"*{CSV_EXTENSION}", SearchOption.TopDirectoryOnly);

                foreach (var csvFilePath in csvFilePaths)
                {
                    if (IsFileValidFormat(new FileInfo(csvFilePath).Name, out var fileSeason))
                    {
                        var fileInformation = new FileSeasonInformation()
                        {
                            FilePath = csvFilePath,
                            SeasonYear = fileSeason
                        };
                        fileInformationModels.Add(fileInformation);
                    }
                }
            }

            return fileInformationModels;
        }

        /// <summary>
        /// Returns all player stats from the given season file
        /// </summary>
        /// <param name="fileInformationModel"></param>
        /// <returns></returns>
        public static List<PlayerStatModel> GetPlayerStatModelsFromFile(FileSeasonInformation fileInformationModel)
        {
            var result = new List<PlayerStatModel>();
            try
            {
                // Our model has properties that don't exist in the csv file.
                // Ensure CsvReader ignores these differences.
                var csvCongifuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                };

                using (var reader = new StreamReader(fileInformationModel.FilePath))
                using (var csv = new CsvReader(reader, csvCongifuration))
                {
                    result = csv.GetRecords<PlayerStatModel>().ToList();
                    foreach (var line in result)
                    {
                        line.Season = fileInformationModel.SeasonYear;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(PlayerStatsCsvReader)} see following message: {Environment.NewLine}{ex.Message}");
            }

            return result;
        }

        #endregion

        #region Private Methods        

        /// <summary>
        /// Determine if the files name is the correct format for our applications use.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool IsFileValidFormat(string fileName, out int seasonNumber)
        {
            // Return 0 for season number if incorrect format
            seasonNumber = 0;
            return fileName.Contains(PLAYER_STATS) && int.TryParse(fileName.Substring(0, 4), out seasonNumber);
        }

        #endregion
    }
}
