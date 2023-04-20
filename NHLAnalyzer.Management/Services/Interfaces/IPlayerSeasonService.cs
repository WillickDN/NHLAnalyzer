using NHLAnalyzer.Data.Entities;

namespace NHLAnalyzer.Management.Services.Interfaces
{
    public interface IPlayerSeasonService
    {
        IQueryable<PlayerSeason> GetPlayerSeasonsByYearAsync(int season);
    }
}
