using NHLAnalyzer.Data.Entities;

namespace NHLAnalyzer.Management.Services.Interfaces
{
    public interface IPlayerSeasonService
    {
        Task<List<PlayerSeason>> GetPlayerSeasonsByYearAsync(int season);
    }
}
