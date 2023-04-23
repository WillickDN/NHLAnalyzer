using NHLAnalyzer.Data.Entities;

namespace NHLAnalyzer.Management.Services.Interfaces
{
    public interface IPlayerSeasonService
    {
        IQueryable<PlayerSeason> GetPlayerSeasonsByYear(int season);

        IQueryable<PlayerSeason> SearchPlayerSeasonsByNameAndYear(string playerName, int season);
    }
}
