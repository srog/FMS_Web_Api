
using Fms_Web_Api.Models;
using System.Collections.Generic;

namespace Fms_Web_Api.Data
{
    public class TeamQuery : Query
    {
        private const string GET_ALL = "spGetAllTeams";
        private const string GET = "spGetTeamById";
        private const string INSERT = "spInsertTeam";
        private const string UPDATE = "spUpdateTeam";

        public IEnumerable<Team> GetAll()
        {
            return GetAll<Team>(GET_ALL);
        }
        public Team Get(int id)
        {
            return GetSingle<Team>(GET, id);
        }
        public int Add(Team team)
        {
            return Add<Team>(INSERT, new { });
        }
        public int Update(Team team)
        {
            return Update<Team>(UPDATE, new { });
        }
        public int Delete(int id)
        {
            return Delete(id);
        }

    }


}