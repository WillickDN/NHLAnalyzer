using Microsoft.EntityFrameworkCore;
using NHLAnalyzer.Data.Entities;
using NHLAnalyzer.Data.Enums;
using NHLAnalyzer.Data.Models;

namespace NHLAnalyzer.Data.Seeding
{
    public class SeedStatsService
    {
        #region Private Members

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public SeedStatsService(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        public int InsertPlayerStatsFromCsvFiles(IEnumerable<FileInfo> seasonFileInfos)
        {
            var playerStatsInserted = 0;
            foreach (FileInfo season in seasonFileInfos)
            {
                playerStatsInserted += InsertSingleSeasonPlayerStats(season);
            }

            return playerStatsInserted;
        }

        public int InsertSingleSeasonPlayerStats(FileInfo seasonFileInformation)
        {
            var playerStatsInserted = 0;
            try
            {
                var seasonPlayerList = PlayerStatsCsvReader.GetPlayerStatModelsFromFile(seasonFileInformation).ToList();

                // Create Season
                var seasonEntity = GetSeason(PlayerStatsCsvReader.GetSeasonFromFileName(seasonFileInformation.Name));

                // Create Teams
                List<string> distinctTeams = seasonPlayerList.Select(x => x.Team).Distinct().ToList();
                var teamEntities = GetTeams(distinctTeams);

                // Create Players
                var players = GetPlayers(seasonPlayerList);

                // Add Player Season
                playerStatsInserted = InsertPlayerSeasonsIfNotExists(seasonEntity, teamEntities, players, seasonPlayerList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(SeedStatsService)} see following message: {Environment.NewLine}{ex.Message}");
            }

            return playerStatsInserted;
        }

        #endregion

        #region Private Methods

        // Create Season   
        private Season GetSeason(int season)
        {
            Season seasonEntity;
            if (!_context.Seasons.Any(x => x.SeasonYear == season))
            {
                seasonEntity = new Season()
                {
                    SeasonYear = season
                };
                _context.Seasons.Add(seasonEntity);

                _context.SaveChanges();
            }
            else
            {
                seasonEntity = _context.Seasons.First(x => x.SeasonYear == season);
            }

            return seasonEntity;
        }

        // Ensure All Teams Exist
        private List<Team> GetTeams(List<string> teams)
        {
            var dbTeams = _context.Teams.ToList();
            var newDbTeams = new List<Team>();

            foreach (var team in teams)
            {
                if (!dbTeams.Any(x => string.Equals(x.NameAbbreviation, team, System.StringComparison.OrdinalIgnoreCase)))
                {
                    var teamEntity = new Team()
                    {
                        NameAbbreviation = team
                    };
                    newDbTeams.Add(teamEntity);
                }
            }
            if (newDbTeams.Any())
            {
                _context.Teams.AddRange(newDbTeams);
                _context.SaveChanges();
            }

            return _context.Teams.ToList();
        }

        // See if the player Exists
        private List<Player> GetPlayers(List<PlayerStatModel> players)
        {
            var dbPlayers = _context.Players.ToList();
            var newDbPlayers = new List<Player>();

            foreach (var player in players)
            {
                if (!dbPlayers.Any(x => string.Equals(x.PlayerName, player.PlayerName, StringComparison.OrdinalIgnoreCase)))
                {
                    var playerEntity = new Player()
                    {
                        PlayerName = player.PlayerName
                    };
                    newDbPlayers.Add(playerEntity);
                }
            }
            if (newDbPlayers.Any())
            {
                _context.Players.AddRange(newDbPlayers);
                _context.SaveChanges();
            }

            return _context.Players.ToList();
        }

        // Add Player Season if it doesn't exist
        private int InsertPlayerSeasonsIfNotExists(Season seasonEntity, List<Team> teams, List<Player> players, List<PlayerStatModel> playerStatModels)
        {
            var dbPlayerSeason = _context.PlayerSeasons.Include(x => x.Season).ToList();
            var newDbPlayerSeasons = new List<PlayerSeason>();
            foreach (var playerStatModel in playerStatModels)
            {
                // Only add a new Player Season if the player and season doesn't already exist in the DB
                if (!dbPlayerSeason.Any(x => x.Player.PlayerName == playerStatModel.PlayerName && x.Season.Id == seasonEntity.Id))
                {
                    var teamEntity = teams.First(x => string.Equals(x.NameAbbreviation, playerStatModel.Team, StringComparison.OrdinalIgnoreCase));
                    var playerEntity = players.First(x => string.Equals(x.PlayerName, playerStatModel.PlayerName, StringComparison.OrdinalIgnoreCase));

                    newDbPlayerSeasons.Add(GetPlayerSeasonFromPlayerStatModel(seasonEntity, teamEntity, playerEntity, playerStatModel));
                }
            }
            if (newDbPlayerSeasons.Any())
            {
                _context.PlayerSeasons.AddRange(newDbPlayerSeasons);
                _context.SaveChanges();
            }

            return newDbPlayerSeasons.Count();
        }

        private static PlayerSeason GetPlayerSeasonFromPlayerStatModel(Season season, Team team, Player player, PlayerStatModel playerStatModel)
        {
            return new PlayerSeason()
            {
                Season = season,
                Team = team,
                Player = player,
                Position = PositionHelper.GetPositionFromString(playerStatModel.Position),
                GamesPlayed = playerStatModel.GamesPlayed,
                Goals = playerStatModel.Goals,
                Assists = playerStatModel.Assists,
                PlusMinus = playerStatModel.PlusMinus,
                Pims = playerStatModel.Pims,
                ShotsOnGoal = playerStatModel.ShotsOnGoal,
                GameWinningGoals = playerStatModel.GameWinningGoals,
                PowerPlayGoals = playerStatModel.PowerPlayGoals,
                PowerPlayAssists = playerStatModel.PowerPlayAssists,
                ShorthandedGoals = playerStatModel.ShorthandedGoals,
                ShorthandedAssists = playerStatModel.ShorthandedAssists,
                Hits = playerStatModel.Hits,
                Blocks = playerStatModel.Blocks
            };
        }

        #endregion
    }
}
