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
    }
}