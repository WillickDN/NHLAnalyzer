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

        public async Task<List<PlayerSeason>> GetPlayerSeasonsByYearAsync(int season)
        {
            return await _context.PlayerSeasons.Include(x => x.Season).Include(x => x.Player).Where(x => x.Season.SeasonYear == season).ToListAsync();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}