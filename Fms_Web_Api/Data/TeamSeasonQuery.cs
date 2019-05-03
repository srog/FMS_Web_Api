using Fms_Web_Api.Models;
using System.Collections.Generic;

namespace Fms_Web_Api.Data
{
    public class TeamSeasonQuery : Query
    {
        private const string GET_ALL_BY_GAME = "spGetTeamSeasonsByGame";
        private const string GET = "spGetTeamSeasonById";
        private const string INSERT = "spInsertTeamSeason";
        private const string RECALCULATE = "spRecalculateTeamSeason";
        

        public IEnumerable<Team> GetByGame(int gameDetailsId)
        {
            return GetAllById<Team>(GET_ALL_BY_GAME, "gameDetailsId", gameDetailsId);
        }
        public Team Get(int id)
        {
            return GetSingle<Team>(GET, id);
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