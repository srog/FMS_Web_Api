using Fms_Web_Api.Models;
using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Data.Queries
{
    public class TeamSeasonQuery : Query, ITeamSeasonQuery
    {
        private const string GET_ALL = "spGetTeamSeasons";
        private const string GET = "spGetTeamSeasonById";
        private const string INSERT = "spInsertTeamSeason";
        private const string UPDATE = "spUpdateTeamSeason";
        private const string RECALCULATE = "spRecalculateTeamSeason";
        private const string RECALCULATE_POSITIONS = "spRecalculateDivisionPositions";

        private INewsQuery _newsQuery { get; }
        private ITeamQuery _teamQuery { get; }
        private IFixtureGenerator _fixtureGenerator { get; }
        public TeamSeasonQuery(INewsQuery newsQuery, ITeamQuery teamQuery, IFixtureGenerator fixtureGenerator)
        {
            _newsQuery = newsQuery;
            _teamQuery = teamQuery;
            _fixtureGenerator = fixtureGenerator;
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

        public IEnumerable<TeamSeason> GetBySeasonAndDivision(int divisionId, int seasonId)
        {
            var param = new { divisionId, seasonId };
            return GetAll<TeamSeason>(GET_ALL, param);
        }

        public IEnumerable<TeamSeason> GetByGameAndSeason(int gameDetailsId, int seasonId)
        {
            var param = new { gameDetailsId, seasonId };
            return GetAll<TeamSeason>(GET_ALL, param);
        }

        public TeamSeason GetCurrentForTeam(int teamId)
        {
            return GetSingleById<TeamSeason>(GET_ALL, "teamId", teamId);
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

        public int Update(TeamSeason teamSeason)
        {
            return Update(UPDATE, new Dictionary<string, object>
                {
                    {"Id", teamSeason.Id},
                    {"position", teamSeason.Position}
                });
        }

        public int Recalculate(int id)
        {
            var result = Update(RECALCULATE, new { id });
            return result;
        }

        public void RecalculateDivisionPositions(int seasonId, int divisionId)
        {
            Update(RECALCULATE_POSITIONS, new { seasonId, divisionId });
        }
    }
}