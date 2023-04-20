using NHLAnalyzer.Management.Services.Interfaces;

namespace NHLAnalyzer.Management.Services
{
    public class PlayerRankingService : IPlayerRankingService
    {
        #region Member Variables

        #endregion

        #region Constructors

        #endregion

        #region Public Methods

        public double GetColumnRanking(int maxStat, int minStat, int playerStat)
        {
            return 1 - ((maxStat - playerStat) / (maxStat - minStat)) * 90 + 10;
        }

        public double GetPlayerRanking()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        #endregion        
    }
}
