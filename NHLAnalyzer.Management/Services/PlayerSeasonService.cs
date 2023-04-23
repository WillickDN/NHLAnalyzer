using Microsoft.EntityFrameworkCore;
using NHLAnalyzer.Data;
using NHLAnalyzer.Data.Entities;
using NHLAnalyzer.Management.Services.Interfaces;

namespace NHLAnalyzer.Management.Services
{
    public class PlayerSeasonService : IPlayerSeasonService
    {
        #region Member Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public PlayerSeasonService(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get all stats for players who played in the given season.
        /// </summary>
        /// <param name="season"></param>
        /// <returns></returns>
        public IQueryable<PlayerSeason> GetPlayerSeasonsByYear(int season)
        {
            return _context.PlayerSeasons.Include(x => x.Season)
                .Include(x => x.Player)
                .Where(x => x.Season.SeasonYear >= season)
                .OrderBy(x => x.Player.PlayerName);
        }

        /// <summary>
        /// Get all stats for players who played within the given seasons
        /// </summary>
        /// <param name="startSeason"></param>
        /// <param name="endSeason"></param>
        /// <returns></returns>
        public IQueryable<PlayerSeason> GetPlayerSeasonsBetweenYears(int startSeason, int endSeason)
        {
            return _context.PlayerSeasons.Include(x => x.Season)
                .Include(x => x.Player)
                .Where(x => x.Season.SeasonYear >= startSeason && x.Season.SeasonYear <= endSeason)
                .OrderBy(x => x.Player.PlayerName);
        }

        /// <summary>
        /// Get all stats for players who played in the given season and whose name contains the search string.
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        public IQueryable<PlayerSeason> SearchPlayerSeasonsByNameAndYear(string playerName, int season)
        {
            var allPlayers = GetPlayerSeasonsByYear(season).ToList();
            return allPlayers.Where(x => x.Player.PlayerName.Contains(playerName, StringComparison.OrdinalIgnoreCase)).AsQueryable();
        }

        /// <summary>
        /// Get all stats for players whoe played between the given seasons and whose name contains the search string.
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="startSeason"></param>
        /// <param name="endSeason"></param>
        /// <returns></returns>
        public IQueryable<PlayerSeason> SearchPlayerSeasonsByNameAndBetweenYears(string playerName, int startSeason, int endSeason)
        {
            var allPlayers = GetPlayerSeasonsBetweenYears(startSeason, endSeason).ToList();
            return allPlayers.Where(x => x.Player.PlayerName.Contains(playerName, StringComparison.OrdinalIgnoreCase)).AsQueryable();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}