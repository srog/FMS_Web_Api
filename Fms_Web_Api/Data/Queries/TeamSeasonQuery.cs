using Fms_Web_Api.Models;
using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;

namespace Fms_Web_Api.Data.Queries
{
    public class TeamSeasonQuery : Query, ITeamSeasonQuery
    {
        private const string GET_ALL = "spGetTeamSeasons";
        private const string GET = "spGetTeamSeasonById";
        private const string INSERT = "spInsertTeamSeason";
        private const string RECALCULATE = "spRecalculateTeamSeason";

        private INewsQuery _newsQuery { get; }
        private ITeamQuery _teamQuery { get; }
        public TeamSeasonQuery(INewsQuery newsQuery, ITeamQuery teamQuery)
        {
            _newsQuery = newsQuery;
            _teamQuery = teamQuery;
        }

        public IEnumerable<TeamSeason> GetByGame(int gameDetailsId)
        {
            return GetAllById<TeamSeason>(GET_ALL, "gameDetailsId", gameDetailsId);
        }

        public IEnumerable<TeamSeason> GetByGameAndDivision(int gameDetailsId, int divisionId)
        {
            var param = new { gameDetailsId, divisionId };
            return GetAll<TeamSeason>(GET_ALL, param);
        }

        public IEnumerable<TeamSeason> GetByGameSeasonAndDivision(int gameDetailsId, int divisionId, int seasonId)
        {
            var param = new { gameDetailsId, divisionId, seasonId };
            return GetAll<TeamSeason>(GET_ALL, param);
        }

        public IEnumerable<TeamSeason> GetByGameAndSeason(int gameDetailsId, int seasonId)
        {
            var param = new { gameDetailsId, seasonId };
            return GetAll<TeamSeason>(GET_ALL, param);
        }


        public TeamSeason Get(int id)
        {
            return GetSingle<TeamSeason>(GET, id);
        }
        public int Add(TeamSeason teamSeason)
        {
            return Add(INSERT, new Dictionary<string, object>
                {
                    {"divisionId", teamSeason.DivisionId},
                    {"gameDetailsId", teamSeason.GameDetailsId},
                    {"seasonId", teamSeason.SeasonId},
                    {"teamId", teamSeason.TeamId},
                    {"position", teamSeason.Position}
                });

        }
        public int Recalculate(int id)
        {
            return Update(RECALCULATE, new { id });
        }

        // create for a new game
        public void CreateForNewGame(IEnumerable<Team> teamList, int seasonId, int gameDetailsId)
        {
            var index = 0;
            foreach (var team in teamList)
            {
                index++;
                if (index > 12)
                    index = 1;

                Add(new TeamSeason
                    {
                        DivisionId = team.DivisionId,
                        SeasonId = seasonId,
                        TeamId = team.Id,
                        GameDetailsId = gameDetailsId,
                        Position = index
                    });
            }
        }

        // Create for a new season
        // Does promotion / relegation
        public int CreateForNewSeason(int gameDetailsId, int oldSeasonId, int newSeasonId)
        {
            var currentTeamSeasons = GetByGameAndSeason(gameDetailsId, oldSeasonId);

            foreach (var teamSeason in currentTeamSeasons)
            {
                var newPosition = teamSeason.Position;

                //Recalculate(teamSeason.Id); ??
                var newDivision = teamSeason.DivisionId;
                if ((teamSeason.Position < 3)
                    && (teamSeason.DivisionId > 1))
                {
                    newDivision--;
                    newPosition = 10 + newPosition;

                    // Add news item for promotion
                    _newsQuery.Add(new News
                    {
                        GameDetailsId = gameDetailsId,
                        TeamId = teamSeason.TeamId,
                        SeasonId = oldSeasonId,
                        Week = 23,
                        DivisionId = teamSeason.DivisionId,
                        NewsText = _teamQuery.Get(teamSeason.TeamId) + " promoted to division " + newDivision
                    });
                }
                if ((teamSeason.Position > 10)
                    && (teamSeason.DivisionId < 4))
                {
                    newDivision++;
                    newPosition = newPosition - 10;
                    // add news item for relegation
                    _newsQuery.Add(new News
                    {
                        GameDetailsId = gameDetailsId,
                        TeamId = teamSeason.TeamId,
                        SeasonId = oldSeasonId,
                        Week = 23,
                        DivisionId = teamSeason.DivisionId,
                        NewsText = _teamQuery.Get(teamSeason.TeamId) + " relegated to division " + newDivision
                    });
                }

                var newTeamSeason = new TeamSeason
                    {
                        DivisionId = newDivision,
                        GameDetailsId = gameDetailsId,
                        SeasonId = newSeasonId,
                        TeamId = teamSeason.TeamId,
                        Played = 0,
                        Won = 0,
                        Drawn = 0,
                        Lost = 0,
                        GoalsFor = 0,
                        GoalsAgainst = 0,
                        Points = 0,
                        Position = newPosition
                    };
                Add(newTeamSeason);
            }

            return 0;
        }
    }
}