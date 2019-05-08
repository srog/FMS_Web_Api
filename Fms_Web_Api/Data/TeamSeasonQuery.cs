using Fms_Web_Api.Models;
using System.Collections.Generic;

namespace Fms_Web_Api.Data
{
    public class TeamSeasonQuery : Query
    {
        private const string GET_ALL = "spGetTeamSeasons";
        private const string GET = "spGetTeamSeasonById";
        private const string INSERT = "spInsertTeamSeason";
        private const string RECALCULATE = "spRecalculateTeamSeason";
        

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
                    {"teamId", teamSeason.TeamId}
                });

        }
        public int Recalculate(int id)
        {
            return Update(RECALCULATE, new { id });
        }

        // create for a new game
        public void CreateTeamSeasons(IEnumerable<Team> teamList, int seasonId, int gameDetailsId)
        {
            var index = 0;
            foreach (var team in teamList)
            {
                index++;
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
        public int CreateTeamSeasons(int gameDetailsId, int oldSeasonId, int newSeasonId)
        {
            var currentTeamSeasons = GetByGameAndSeason(gameDetailsId, oldSeasonId);

            foreach (var teamSeason in currentTeamSeasons)
            {
                //Recalculate(teamSeason.Id); ??
                var newDivision = teamSeason.DivisionId;
                if ((teamSeason.Position < 3)
                    && (teamSeason.DivisionId > 1))
                {
                    // Add news item for promotion
                    newDivision--;
                }
                if ((teamSeason.Position > 10)
                    && (teamSeason.DivisionId < 4))
                {
                    // add news item for relegation
                    newDivision++;
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
                        Points = 0
                    };
                Add(newTeamSeason);
            }

            return 0;
        }


    }
}