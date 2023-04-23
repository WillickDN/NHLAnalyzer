using NHLAnalyzer.Data.Entities;

namespace NHLAnalyzer.Management.Services.Interfaces
{
    public interface IPlayerSeasonService
    {
        IQueryable<PlayerSeason> GetPlayerSeasonsByYear(int season);

        IQueryable<PlayerSeason> GetPlayerSeasonsBetweenYears(int startSeason, int endSeason);

        IQueryable<PlayerSeason> SearchPlayerSeasonsByNameAndYear(string playerName, int season);

        IQueryable<PlayerSeason> SearchPlayerSeasonsByNameAndBetweenYears(string playerName, int startSeason, int endSeason);
    }
}
