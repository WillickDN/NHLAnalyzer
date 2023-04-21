using NHLAnalyzer.Data.Seeding;

namespace NHLAnalyzer.Tests.DataTests
{
    public class PlayerStatsCsvReaderTests
    {

        private const string TEST_ASSET_DIRECTORY = @"Assets/PlayerStatsTest";
        private const string TEST_NO_SEASON_IN_TITLE_FOLDER = @"NoSeasonInTitle";
        private const string TEST_NO_PLAYER_STATS_IN_TITLE_FOLDER = @"NoPlayerStatsInTitle";
        private const string TEST_VALID_FILE_NAMES_FOLDER = @"ValidFileNames";
        private const string TEST_INCORRECT_INTERNAL_FORMAT_FILE = @"IncorrectInternalFormat/2020_Player_Stats.csv";
        private const string TEST_VALID_INTERNAL_FORMAT_FILE = @"ValidFileNames/2020_Player_Stats.csv";

        #region GetPlayerStatsCsvPaths

        [Fact]
        public void GetPlayerStatsCsvPaths_FolderWithValidCsvNames_ReturnsCsvPath()
        {
            // Arrange
            var testDirectory = Path.Combine(Directory.GetCurrentDirectory(), $"{TEST_ASSET_DIRECTORY}/{TEST_VALID_FILE_NAMES_FOLDER}");
            var csvFilesInDirectoryCount = Directory.GetFiles(testDirectory, "*.csv").Length;
            Assert.True(Directory.Exists(testDirectory));

            // Act
            var resultCsvPaths = PlayerStatsCsvReader.GetPlayerStatCsvPaths(testDirectory);

            // Assert

            // Ensure The directory we are testing actually has some files in there
            Assert.NotEmpty(Directory.GetFiles(testDirectory, "*.csv"));
            // Ensure only the csv files were returned
            Assert.Equal(csvFilesInDirectoryCount, resultCsvPaths.Count());
        }

        [Fact]
        public void GetPlayerStatsCsvPaths_NoPlayerStatsInName_ReturnsEmptyList()
        {
            // Arrange
            var testDirectory = Path.Combine(Directory.GetCurrentDirectory(), $"{TEST_ASSET_DIRECTORY}/{TEST_NO_PLAYER_STATS_IN_TITLE_FOLDER}");
            Assert.True(Directory.Exists(testDirectory));

            // Act
            var resultCsvPaths = PlayerStatsCsvReader.GetPlayerStatCsvPaths(testDirectory);

            // Assert

            // Ensure The directory we are testing actually has some files in there
            Assert.NotEmpty(Directory.GetFiles(testDirectory));
            // Ensure no paths were returned
            Assert.Empty(resultCsvPaths);
        }

        [Fact]
        public void GetPlayerStatsCsvPaths_NoSeasonInName_ReturnsEmptyList()
        {
            // Arrange
            var testDirectory = Path.Combine(Directory.GetCurrentDirectory(), $"{TEST_ASSET_DIRECTORY}/{TEST_NO_SEASON_IN_TITLE_FOLDER}");
            Assert.True(Directory.Exists(testDirectory));

            // Act
            var resultCsvPaths = PlayerStatsCsvReader.GetPlayerStatCsvPaths(testDirectory);

            // Assert

            // Ensure The directory we are testing actually has some files in there
            Assert.NotEmpty(Directory.GetFiles(testDirectory));
            // Ensure no paths were returned
            Assert.Empty(resultCsvPaths);
        }

        #endregion

        #region GetSeasonFromFilename

        [Fact]
        public void GetSeasonFromFileName_NoSeasonInName_ReturnsNegativeOne()
        {
            // Arrange
            var fileInfo = "Player_Stats.csv";

            // Act
            var actualSeasonReturn = PlayerStatsCsvReader.GetSeasonFromFileName(fileInfo);

            // Assert
            Assert.Equal(-1, actualSeasonReturn);
        }

        [Fact]
        public void GetSeasonFromFileName_SeasonNumberTooShort_ReturnsNegativeOne()
        {
            // Arrange

            var fileSeason = 123;
            var fileInfo = $"{fileSeason}_Player_Stats.csv";

            // Act
            var actualSeasonReturn = PlayerStatsCsvReader.GetSeasonFromFileName(fileInfo);

            // Assert
            Assert.Equal(-1, actualSeasonReturn);
        }

        [Fact]
        public void GetSeasonFromFileName_SeasonNumberTooLong_ReturnsFirst4Digits()
        {
            // Arrange
            var fileSeason = 12345;
            var fileInfo = $"{fileSeason}_Player_Stats.csv";

            // Act
            var actualSeasonReturn = PlayerStatsCsvReader.GetSeasonFromFileName(fileInfo);

            // Assert
            Assert.NotEqual(-1, actualSeasonReturn);
            Assert.NotEqual(fileSeason, actualSeasonReturn);
        }

        [Fact]
        public void GetSeasonFromFileName_SeasonNumberFormatIsProper_Returns4DigitSeason()
        {
            // Arrange
            var fileSeason = 1234;
            var fileInfo = $"{fileSeason}_Player_Stats.csv";

            // Act
            var actualSeasonReturn = PlayerStatsCsvReader.GetSeasonFromFileName(fileInfo);

            // Assert
            Assert.NotEqual(-1, actualSeasonReturn);
            Assert.Equal(fileSeason, actualSeasonReturn);
        }

        #endregion

        #region GetPlayerModelsFromFile

        [Fact]
        public void GetPlayerModelsFromFile_ValidFile_ReturnsPlayerModels()
        {
            // Arrange
            var fileInfo = new FileInfo(Path.Combine(TEST_ASSET_DIRECTORY, TEST_VALID_INTERNAL_FORMAT_FILE));

            // Act
            var result = PlayerStatsCsvReader.GetPlayerStatModelsFromFile(fileInfo);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetPlayerModelsFromFile_InvalidValidFile_ReturnsPlayerModels()
        {
            // Arrange
            var fileInfo = new FileInfo(Path.Combine(TEST_ASSET_DIRECTORY, TEST_INCORRECT_INTERNAL_FORMAT_FILE));

            // Act
            // Assert
            Assert.Throws<CsvHelper.HeaderValidationException>(() => PlayerStatsCsvReader.GetPlayerStatModelsFromFile(fileInfo));
        }

        #endregion
    }
}
