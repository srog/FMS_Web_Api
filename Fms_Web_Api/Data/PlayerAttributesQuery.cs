
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data
{
    public class PlayerAttributesQuery : Query
    {
        private const string GET = "spGetPlayerAttributes";
        private const string INSERT = "spInsertPlayerAttributes";
        private const string UPDATE = "spUpdatePlayerAttributes";

        public PlayerAttributes Get(int id)
        {
            return GetSingle<PlayerAttributes>(GET, id);
        }
        public int Add(PlayerAttributes playerAttributes)
        {
            return Add<PlayerAttributes>(INSERT, new {  });
        }
        public int Update(PlayerAttributes playerAttributes)
        {
            return Update<PlayerAttributes>(UPDATE, new { });
        }
        public int Delete(int id)
        {
            return Delete(id);
        }

    }


}