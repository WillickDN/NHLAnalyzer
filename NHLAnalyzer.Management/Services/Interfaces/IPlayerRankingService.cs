namespace NHLAnalyzer.Management.Services.Interfaces
{
    public interface IPlayerRankingService
    {
        double GetColumnRanking(int maxStat, int minStat, int playerStat);

        double GetPlayerRanking();
    }
}
