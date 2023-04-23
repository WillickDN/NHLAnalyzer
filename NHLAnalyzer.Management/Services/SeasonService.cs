using NHLAnalyzer.Data;
using NHLAnalyzer.Data.Entities;
using NHLAnalyzer.Management.Services.Interfaces;

namespace NHLAnalyzer.Management.Services
{
    public class SeasonService : ISeasonService
    {
        #region Member Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public SeasonService(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        public IEnumerable<Season> GetAllSeasons()
        {
            return _context.Seasons;
        }

        public IEnumerable<int> GetAllSeasonYears()
        {
            return _context.Seasons.Select(x => x.SeasonYear).OrderByDescending(x => x);
        }

        #endregion

        #region Private Methods

        #endregion        
    }
}
