
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data
{
    public class PlayerStatsQuery : Query
    {
        private const string GET = "spGetPlayerStats";
        private const string INSERT = "spInsertPlayerStats";
        private const string UPDATE = "spUpdatePlayerStats";

        public PlayerStats Get(int id)
        {
            return GetSingle<PlayerStats>(GET, id);
        }
        public int Add(PlayerStats playerStats)
        {
            return Add<PlayerStats>(INSERT, new { });
        }
        public int Update(PlayerStats playerStats)
        {
            return Update<PlayerStats>(UPDATE, new { });
        }
        public int Delete(int id)
        {
            return Delete(id);
        }

    }


}