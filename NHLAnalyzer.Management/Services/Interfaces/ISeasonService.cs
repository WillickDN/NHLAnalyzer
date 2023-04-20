using NHLAnalyzer.Data.Entities;

namespace NHLAnalyzer.Management.Services.Interfaces
{
    public interface ISeasonService
    {
        IEnumerable<Season> GetAllSeasons();

        IEnumerable<int> GetAllSeasonYears();
    }
}
