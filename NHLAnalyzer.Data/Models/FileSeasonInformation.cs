namespace NHLAnalyzer.Data.Models
{
    /// <summary>
    /// When reading information from the files, we need to know the season that the file represents.
    /// </summary>
    public class FileSeasonInformation
    {
        public string FilePath { get; set; }

        public int SeasonYear { get; set; }
    }
}
